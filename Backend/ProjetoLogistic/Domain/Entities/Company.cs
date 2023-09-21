using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public bool Active { get; set; }
        public ICollection<Sale> Sales { get; set; }

    }
}
