using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS0162

// ReSharper disable All

namespace ConfirmationService.Host;

public class SpaceshipStarter
{
    public void SendOrder(Order order)
    {
        if (order is null)
        {
            throw new ArgumentNullException(nameof(order));
        }
    }
}

public class Order
{
}
