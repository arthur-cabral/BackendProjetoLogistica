using Application.DTO.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Company
{
    public class CompanyDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public bool Active { get; set; }
        public ICollection<SaleDTO> Sales { get; set; }
    }
}
