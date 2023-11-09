using Contracts.Events;
using MassTransit;

namespace NFTAuctionService.Consumers;

public class NFTAuctionCreatedFaultConsumer: IConsumer<Fault<NFTAuctionCreated>>
{
    public async Task Consume(ConsumeContext<Fault<NFTAuctionCreated>> context)
    {
        Console.WriteLine("DEBUG: --> Consuming faulty creation");

        var exception = context.Message.Exceptions.First();

        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Name = "FooBar";
            await context.Publish(context.Message.Message);
        }
        else
        {
            Console.WriteLine("DEBUG: Not an argument exception - update error dashboard somewhere");
        }
    }
}
