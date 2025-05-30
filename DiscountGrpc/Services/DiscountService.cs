using AutoMapper;
using DiscountGrpc.Protos;
using Grpc.Core;
using System.Linq;

namespace DiscountGrpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private List<Coupon> _coupons;
        private readonly IMapper _mapper;

        public DiscountService(IMapper mapper)
        {
            _coupons = new List<Coupon>
            {
                new Coupon { Id = 1, ProductName = "Samsung 10", Description = "10 off", Amount = 10 },
                new Coupon { Id = 2, ProductName = "IPhone X", Description = "20 off", Amount = 20 }
            };
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var result = _coupons.FirstOrDefault(x => x.ProductName.Equals(request.ProductName, StringComparison.OrdinalIgnoreCase))
                ?? new Coupon { ProductName = request.ProductName, Description = "No discount available", Amount = 0 };

            return _mapper.Map<CouponModel>(result);
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            _coupons.Add(coupon);

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _coupons.FirstOrDefault(x => x.ProductName.Equals(request.Coupon.ProductName, StringComparison.OrdinalIgnoreCase));
            if(coupon == null)
            {
                return new CouponModel();
            }
            coupon.Description = request.Coupon.Description;
            coupon.Amount = request.Coupon.Amount;

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = _coupons.FirstOrDefault(x => x.ProductName.Equals(request.ProductName, StringComparison.OrdinalIgnoreCase));
            if (coupon != null)
            {
                _coupons.Remove(coupon);
                return new DeleteDiscountResponse { Success = true };
            }
            return new DeleteDiscountResponse { Success = false };
        }
    }
}
