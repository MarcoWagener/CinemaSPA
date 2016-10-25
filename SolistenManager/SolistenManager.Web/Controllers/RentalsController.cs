using AutoMapper;
using SolistenManager.Data.Extensions;
using SolistenManager.Data.Infrastructure;
using SolistenManager.Data.Repositories;
using SolistenManager.Entities;
using SolistenManager.Web.Infrastructure.Core;
using SolistenManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SolistenManager.Web.Controllers
{
    [RoutePrefix("api/rentals")]
    public class RentalsController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Rental> _rentalRepository;
        private readonly IEntityBaseRepository<Client> _clientRepository;
        private readonly IEntityBaseRepository<Stock> _stockRepository;
        private readonly IEntityBaseRepository<Solisten> _solistenRepository;

        public RentalsController(IEntityBaseRepository<Rental> rentalRepository,
            IEntityBaseRepository<Client> clientRepository,
            IEntityBaseRepository<Stock> stockRepository,
            IEntityBaseRepository<Solisten> solistenRepository,
            IEntityBaseRepository<Error> _errorsRepository,
            IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _rentalRepository = rentalRepository;
            _clientRepository = clientRepository;
            _stockRepository = stockRepository;
            _solistenRepository = solistenRepository;
        }

        [HttpGet]
        [Route("{id:int}/rentalhistory")]
        public HttpResponseMessage RentalHistory(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<RentalHistoryModel> _rentalHistory = GetSolistenRentalHistory(id);

                response = request.CreateResponse<List<RentalHistoryModel>>(HttpStatusCode.OK, _rentalHistory);

                return response;
            });
        }

        [HttpPost]
        [Route("return/{rentalId:int}")]
        public HttpResponseMessage Return(HttpRequestMessage request, int rentalId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var rental = _rentalRepository.GetSingle(rentalId);

                if (rental == null)
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid rental");
                else
                {
                    rental.Status = "Returned";
                    rental.Stock.IsAvailable = true;
                    rental.ReturnedDate = DateTime.Now;

                    _unitOfWork.Commit();


                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }
        
        [HttpPost]
        [Route("rent/{clientId:int}/{stockId:int}")]
        public HttpResponseMessage Rent(HttpRequestMessage request, int clientId, int stockId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var client = _clientRepository.GetSingle(clientId);
                var stock = _stockRepository.GetSingle(stockId);

                if(client == null || stock == null)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid client or stock time");
                }
                else
                {
                    if (stock.IsAvailable)
                    {
                        Rental rental = new Rental()
                        {
                            ClientId = clientId,
                            StockId = stockId,
                            RentalDate = DateTime.Now,
                            Status = "Borrowed"
                        };

                        _rentalRepository.Add(rental);

                        stock.IsAvailable = false;

                        _unitOfWork.Commit();

                        RentalModel rentalModel = Mapper.Map<Rental, RentalModel>(rental);

                        response = request.CreateResponse<RentalModel>(HttpStatusCode.OK, rentalModel);
                    }
                    else
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Selected stock is not available anymore");
                }

                return response;
            });
        }

        private List<RentalHistoryModel> GetSolistenRentalHistory(int solistenId)
        {
            List<RentalHistoryModel> _rentalHistory = new List<RentalHistoryModel>();
            List<Rental> rentals = new List<Rental>();

            var solisten = _solistenRepository.GetSingle(solistenId);

            foreach(var stock in solisten.Stocks)
            {
                rentals.AddRange(stock.Rentals);
            }

            foreach(var rental in rentals)
            {
                RentalHistoryModel _historyItem = new RentalHistoryModel()
                {
                    ID = rental.ID,
                    StockId = rental.StockId,
                    RentalDate = rental.RentalDate,
                    ReturnedDate = rental.ReturnedDate.HasValue ? rental.ReturnedDate : null,
                    Status = rental.Status,
                    Client = _clientRepository.GetClientFullName(rental.ClientId)
                };

                _rentalHistory.Add(_historyItem);
            }

            _rentalHistory.Sort((r1, r2) => r2.RentalDate.CompareTo(r1.RentalDate));

            return _rentalHistory;
        }
    }
}
