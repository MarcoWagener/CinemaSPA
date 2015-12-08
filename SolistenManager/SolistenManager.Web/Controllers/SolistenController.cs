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
    [RoutePrefix("api/solistens")]
    public class SolistenController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Solisten> _solistenRepository;

        public SolistenController(IEntityBaseRepository<Solisten> solistenRepository, 
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _solistenRepository = solistenRepository;
        }

        [AllowAnonymous]
        [Route("all")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var solistenList = _solistenRepository
                                    .GetAll()
                                    .OrderByDescending(s => s.PurchaseDate).Take(6).ToList();

                IEnumerable<SolistenModel> solistenModel = Mapper.Map<IEnumerable<Solisten>, IEnumerable<SolistenModel>>(solistenList);

                response = request.CreateResponse<IEnumerable<SolistenModel>>(HttpStatusCode.OK, solistenModel);

                return response;
            });
        }
    }
}
