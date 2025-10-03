using AutoMapper;
using KantarStore.Application.Dtos;
using KantarStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Application.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            //CreateMap<BasketDto, Basket>().ForMember(b => b.Items, opt => opt.MapFrom(b => b.Items));
            //CreateMap<Basket, BasketDto>().ForMember(b => b.Items, opt => opt.MapFrom(b => b.Items));
            //CreateMap<BasketItemDto, BasketItem>();
            //CreateMap<BasketItem, BasketItemDto>();
        }
    }

}
