using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sale
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public Company Company { get; set; }
    }
}
