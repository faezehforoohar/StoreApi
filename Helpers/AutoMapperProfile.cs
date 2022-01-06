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
            CreateMap<PriceList, PriceListModel>();
            CreateMap<PriceListModel, PriceList>();
            CreateMap<PriceListSave, PriceList>();
            CreateMap<PriceListDModel, PriceListD>();
            CreateMap<PriceListDSave, PriceListD>();
            CreateMap<PriceListD, PriceListDModel>();

        }
    }
}