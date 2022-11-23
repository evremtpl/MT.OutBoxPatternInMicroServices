using MT.OutBoxPatternInMicroServices.Shared.Events.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Shared.Events
{
    public class OrderCreatedEvent : IEvent
    {
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Guid IdempotentToken { get; set; }
    }
}
