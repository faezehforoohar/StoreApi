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
            CreateMap<PriceList, PriceListModel>()
                 .ForMember(x => x.DateTime, opt => opt.MapFrom(o => o.DateTime.ToPersianDate()));
            CreateMap<PriceListModel, PriceList>();
            CreateMap<PriceListUpdate, PriceList>()
                 .ForMember(x => x.DateTime, opt => opt.MapFrom(o => o.DateTime.ToDateTime()));
            CreateMap<PriceListCreate, PriceList>()
                   .ForMember(x => x.DateTime, opt => opt.MapFrom(o => o.DateTime.ToDateTime()));
            //
            CreateMap<PriceListD, PriceListDModel>();
            CreateMap<PriceListDModel, PriceListD>();
            CreateMap<PriceListDUpdate, PriceListD>();
            CreateMap<PriceListDCreate, PriceListD>();



        }
    }
}