using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Domain.Entities
{
    public class OrderOutBox
    {
        public OrderOutBox()
        {

        }
        public DateTime OccureOn  { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string @Type  { get; set; }
        public string Payload { get; set; }
        public Guid IdempotentToken { get; set; }
    }
}
