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
            CouponModel response = new CouponModel();
            var request = new GetDiscountRequest { ProductName = productName };
            try
            {
                response = await _discountProtoService.GetDiscountAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching discount for {productName}: {ex.Message}");
            }
            return response;
        }
    }
}
