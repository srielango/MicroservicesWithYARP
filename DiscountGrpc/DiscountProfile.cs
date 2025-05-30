using AutoMapper;
using DiscountGrpc.Protos;

namespace DiscountGrpc
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
