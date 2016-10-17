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
    [RoutePrefix("api/stocks")]
    public class StocksController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Stock> _stocksRepository;

        public StocksController(IEntityBaseRepository<Stock> stocksRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _stocksRepository = stocksRepository;
        }

        [Route("solisten/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            IEnumerable<Stock> stocks = null;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                stocks = _stocksRepository.GetAvailableItems(id);

                IEnumerable<StockModel> stocksModel = Mapper.Map<IEnumerable<Stock>, IEnumerable<StockModel>>(stocks);

                response = request.CreateResponse<IEnumerable<StockModel>>(HttpStatusCode.OK, stocksModel);

                return response;
            });
        }
    }
}
