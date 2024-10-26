using System;

double orderAmount = 50;
Console.WriteLine($"Order amount: {orderAmount} uah");


var deliveryContext = new DeliveryContext();

deliveryContext.SetDeliveryStrategy(new SelfPickupStrategy());
Console.WriteLine($"Self Pickup cost: {deliveryContext.GetDeliveryCost(orderAmount)} uah");


deliveryContext.SetDeliveryStrategy(new ExternalDeliveryStrategy());
Console.WriteLine($"External Delivery cost: {deliveryContext.GetDeliveryCost(orderAmount)} uah");


deliveryContext.SetDeliveryStrategy(new InternalDeliveryStrategy());
Console.WriteLine($"Internal Delivery cost: {deliveryContext.GetDeliveryCost(orderAmount)} uah");



public interface IDeliveryStrategy
{
    double CalculateDeliveryCost(double orderAmount);
}


public class SelfPickupStrategy : IDeliveryStrategy
{
    public double CalculateDeliveryCost(double orderAmount){return 0;}
}

public class ExternalDeliveryStrategy : IDeliveryStrategy
{
    public double CalculateDeliveryCost(double orderAmount)
    {
        if (orderAmount < 1000){return 70;}
        return 70 + orderAmount * 0.1;
    }
}

public class InternalDeliveryStrategy : IDeliveryStrategy
{
    public double CalculateDeliveryCost(double orderAmount)
    {
        return Math.Max(orderAmount*0.1, 30);
    }
}

public class DeliveryContext
{
    private IDeliveryStrategy deliveryStrategy;

    public void SetDeliveryStrategy(IDeliveryStrategy deliveryStrategy)
    {
        this.deliveryStrategy = deliveryStrategy;
    }

    public double GetDeliveryCost(double orderAmount)
    {
        return deliveryStrategy.CalculateDeliveryCost(orderAmount);
    }
}

