using Application.Activities;
using Application.Messages;
using MassTransit;

namespace API.Activities
{
    public class ChargePaymentActivity : IExecuteActivity<ChargePaymentArguments>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<ChargePaymentArguments> context)
        {
            Console.WriteLine("Charge");
            await context.Publish(new PaymentAccepted(


                context.Arguments.OrderId,
                context.Arguments.BuyerId,
                context.Arguments.Amount));
            return context.Completed();
        }
    }
}
