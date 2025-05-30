using DiscountGrpc.Protos;

namespace BasketAPI
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscountAsync(string productName)
        {
            var request = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(request);
        }
    }
}
