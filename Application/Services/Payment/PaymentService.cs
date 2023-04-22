using Application.Dtos;
using Domain.Entities;
using Stripe.Checkout;

namespace Application.Services.Payment
{
    public interface IPaymentService
    {
        Task<string> CreateCheckoutSessionAsync(SubscriptionPlanToReturnDto plan, string returnUrl);
    }

    public class PaymentService : IPaymentService
    {
        public async Task<string> CreateCheckoutSessionAsync(SubscriptionPlanToReturnDto plan, string returnUrl)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)plan.Price * 100,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"{plan.Name} - {plan.Description}",
                            }
                        },
                        Quantity = 1,
                    }
                },
                Mode = "payment",
                SuccessUrl = returnUrl + $"&success=true",
                CancelUrl = returnUrl + "&canceled=true",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }
    }
}
    

