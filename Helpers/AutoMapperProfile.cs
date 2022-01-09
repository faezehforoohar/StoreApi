using AutoMapper;
using StoreApi.Entities;
using StoreApi.Models.PriceList;
using StoreApi.Models.PriceListD;
using StoreApi.Models.Users;

namespace StoreApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            //
            CreateMap<PriceList, PriceListModel>();
            CreateMap<PriceListModel, PriceList>();
            CreateMap<PriceListUpdate, PriceList>();
            CreateMap<PriceListCreate, PriceList>();
            //
            CreateMap<PriceListDModel, PriceListD>();
            CreateMap<PriceListDUpdate, PriceListD>();
            CreateMap<PriceListDCreate, PriceListD>();
            CreateMap<PriceListD, PriceListDModel>();


        }
    }
}