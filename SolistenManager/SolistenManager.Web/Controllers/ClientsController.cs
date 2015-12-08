using AutoMapper;
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
    //[Authorize(Roles="Admin")]
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Client> _clientRepository;

        public ClientsController(IEntityBaseRepository<Client> clientRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Client> clientList = null;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    clientList = _clientRepository
                                    .GetAll()
                                    .OrderBy(c => c.ID)
                                    .Where(c => c.FirstName.ToLower().Contains(filter) ||
                                        c.LastName.ToLower().Contains(filter) ||
                                        c.Email.ToLower().Contains(filter))
                                    .ToList();
                }
                else
                    clientList = _clientRepository
                                    .GetAll()
                                    .ToList();

                clientList = clientList.Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                                .ToList();

                IEnumerable<ClientModel> clientModel = Mapper.Map<IEnumerable<Client>, IEnumerable<ClientModel>>(clientList);

                PagnationSet<ClientModel> pagedSet = new PagnationSet<ClientModel>()
                {
                    Page = currentPage,
                    TotalCount = clientList.Count(),
                    TotalPages = (int)Math.Ceiling((decimal)clientList.Count() / currentPageSize),
                    Items = clientModel
                };

                response = request.CreateResponse<PagnationSet<ClientModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
    }
}
