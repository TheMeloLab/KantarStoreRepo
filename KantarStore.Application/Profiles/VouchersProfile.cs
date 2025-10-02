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
    public class VouchersProfile : Profile
    {
        public VouchersProfile()
        {
            CreateMap<Voucher, VoucherDto>();
        }
    }
}
