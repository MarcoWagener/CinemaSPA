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

        [AllowAnonymous]
        [Route("{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalSolistens = new int();

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Solisten> solistenList = null;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    solistenList = _solistenRepository
                                        .GetAll()
                                        .OrderBy(s => s.ID)
                                        .Where(s => s.SerialNumber.ToLower().Contains(filter.ToLower().Trim()))
                                        .ToList();
                }
                else
                {
                    solistenList = _solistenRepository.GetAll().ToList();
                }

                totalSolistens = solistenList.Count();

                solistenList = solistenList.Skip(currentPage * currentPageSize)
                                    .Take(currentPageSize)
                                    .ToList();

                IEnumerable<SolistenModel> solistenModel = Mapper.Map<IEnumerable<Solisten>, IEnumerable<SolistenModel>>(solistenList);

                PagnationSet<SolistenModel> pagedSet = new PagnationSet<SolistenModel>()
                {
                    Page = currentPage,
                    TotalCount = totalSolistens,
                    TotalPages = (int)Math.Ceiling((decimal)totalSolistens / currentPageSize),
                    Items = solistenModel
                };

                response = request.CreateResponse<PagnationSet<SolistenModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [AllowAnonymous]
        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var solisten = _solistenRepository.GetSingle(id);

                var solsitenModel = Mapper.Map<Solisten, SolistenModel>(solisten);

                response = request.CreateResponse<SolistenModel>(HttpStatusCode.OK, solsitenModel);

                return response;
            });
        }
    }
}
