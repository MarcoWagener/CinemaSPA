using AutoMapper;
using SolistenManager.Entities;
using SolistenManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolistenManager.Web.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        //Property not marked as virtual in the automapper assembly. Is this override required?
        //public override string ProfileName
        //{
        //    get { return "DomainToViewModelMappings"; }
        //}

        protected override void Configure()
        {
            Mapper.CreateMap<Solisten, SolistenModel>()
                .ForMember(vm => vm.Description, map => map.MapFrom(m => m.Description))
                .ForMember(vm => vm.Owner, map => map.MapFrom(m => m.Owner))
                .ForMember(vm => vm.SerialNumber, map => map.MapFrom(m => m.SerialNumber))
                .ForMember(vm => vm.IsAvailable, map => map.MapFrom(m => m.Stocks.Any(s => s.IsAvailable)))
                .ForMember(vm => vm.Image, map => map.MapFrom(m => string.IsNullOrEmpty(m.Image) == true ? "unknown.jpg" : m.Image));

            Mapper.CreateMap<Client, ClientModel>();
            Mapper.CreateMap<ClientModel, Client>();
        }
    }
}