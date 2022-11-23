using MediatR;
using MT.OutBoxPatternInMicroServices.Application.Interfaces.Repository;
using MT.OutBoxPatternInMicroServices.Application.Interfaces.UnitOfWork;
using MT.OutBoxPatternInMicroServices.Domain.Entities;
using MT.OutBoxPatternInMicroServices.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderOutBox> _orderOutBoxRepository;
        private readonly IUnitOfWork _unitOfWork;


        public CreateOrderCommandHandler(IGenericRepository<Order> orderRepository, IGenericRepository<OrderOutBox> orderOutBoxRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderOutBoxRepository = orderOutBoxRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            Order order = new() { Description =request.Description};
            await _orderRepository.AddAsync(order);

            OrderOutBox orderOutBox = new()
            {
                OccureOn = DateTime.UtcNow,
                ProcessDate = null,
                Payload = JsonSerializer.Serialize(order),
                Type = nameof(OrderCreatedEvent),
                IdempotentToken = Guid.NewGuid()
            };
            await _orderOutBoxRepository.AddAsync(orderOutBox);
            await _unitOfWork.CommitAsync();
            return new();
        }
    }
}
