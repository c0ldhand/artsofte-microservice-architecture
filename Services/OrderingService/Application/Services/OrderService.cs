using Application.Clients;
using Application.DTO;
using Domain.Entities.Enum;
using Domain.Entities.Values;
using Infrastructure.Repositories;


namespace Application.Services
{
    public class OrderService
    {
        private readonly IIdentityClient _identityClient;
        public readonly UnitOfWork _unitOfWork;
        public OrderService(UnitOfWork unitOfWork, IIdentityClient identityClient) 
        {
            _unitOfWork = unitOfWork;
            _identityClient = identityClient;
        }

        public async Task<Guid> CreateOrder(Guid buyerId, Guid sellerId,
        List<(Guid productId, int qty, decimal price)> items)
        {
            if (items == null || items.Count == 0) throw new ArgumentException("items");

            var orderItems = new List<OrderItem>();
            foreach (var i in items)
            {
                var product = await _unitOfWork.Product.GetByIdAsync(i.productId);
                if (product == null || !product.IsActive)
                    throw new InvalidOperationException($"Product {i.productId} is not available");

                var unitPrice = product.Price;
                orderItems.Add(new OrderItem(i.productId, i.qty, unitPrice));
            }

            var order = new Order(buyerId, sellerId, orderItems);
            await _unitOfWork.Order.AddAsync(order);
            return order.Id;
        }

        public async Task<List<Order>> GetOrdersByProductAsync(Guid productId)
        {
            return await _unitOfWork.Order.GetByProductAsync(productId);
        }

        public async Task<Order?> GetOrder(Guid id)
        {
            return await _unitOfWork.Order.GetByIdAsync(id);
        }

        public async Task<List<Order>> GetBuyerOrders(Guid buyerId)
        {
            return await _unitOfWork.Order.GetByBuyerAsync(buyerId);
        }

        public async Task<List<Order>> GetSellerOrders(Guid sellerId)
        {
            return await _unitOfWork.Order.GetBySellerAsync(sellerId);
        }

        public async Task UpdateStatus(Guid id, OrderStatus status)
        {
            var order = await _unitOfWork.Order.GetByIdAsync(id);
            if (order == null) throw new InvalidOperationException("Order not found");

            switch (status)
            {
                case OrderStatus.Paid:
                    order.MarkAsPaid();
                    break;
                case OrderStatus.Shipped:
                    order.MarkAsShipped();
                    break;
                case OrderStatus.Delivered:
                    order.MarkAsDelivered();
                    break;
                case OrderStatus.Pending:
                case OrderStatus.Cancelled:
                default: throw new ArgumentException("Invalid status");
            }

            await _unitOfWork.Order.UpdateAsync(order);
        }
        public async Task SaveOrderAsync(Order order)
        {
            await _unitOfWork.Order.UpdateAsync(order);
        }

        public async Task UpdateOrderItemsAsync(Guid orderId, List<UpdateItemDTO> items)
        {
            var order = await _unitOfWork.Order.GetByIdAsync(orderId);
            if (order == null) throw new InvalidOperationException("Order not found");
            if (order.Status == OrderStatus.Paid || order.Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Cannot change items of a paid or delivered order");

            var newItems = new List<OrderItem>();
            foreach (var i in items)
            {
                var product = await _unitOfWork.Product.GetByIdAsync(i.ProductId);
                if (product == null || !product.IsActive)
                    throw new InvalidOperationException($"Product {i.ProductId} is not available");

                newItems.Add(new OrderItem(i.ProductId, i.Quantity, product.Price));
            }

            order.ReplaceItems(newItems);
            await _unitOfWork.Order.UpdateAsync(order);
        }

    }
}
