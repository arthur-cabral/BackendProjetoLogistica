using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company
    {
        public Company()
        {
            this.Sales = new HashSet<Sale>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
