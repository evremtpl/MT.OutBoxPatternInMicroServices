using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int  Quantity { get; set; }
        public string Description { get; set; }
    }
}
