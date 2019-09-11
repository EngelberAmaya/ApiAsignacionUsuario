using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiUsuarioCrud.Models
{
    public class Software
    {
        public long Id { get; set; }
        [StringLength(30), Required]
        public string SoftwareName { get; set; }
        // para relacionarla con las asignaciones
        public IList<Assignment> Assignment { get; set; }
    }
}
