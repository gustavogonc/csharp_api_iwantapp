﻿namespace IWantApp.Endpoints.Orders
{
    public record OrderResponse( Guid Id, string ClientEmail, IEnumerable<OrderProduct> Products, string DeliveryAddress);
    public record OrderProduct(Guid id, string Name);
}
