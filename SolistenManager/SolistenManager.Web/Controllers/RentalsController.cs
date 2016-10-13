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

        private List<RentalHistoryModel> GetSolistenRentalHistory(int solistenId)
        {
            throw new NotImplementedException();
        }
    }
}
