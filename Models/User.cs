using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiUsuarioCrud.Models
{
    public class User
    {
        public long Id { get; set; }
        [StringLength(30), Required]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime LastSessionDateTime { get; set; }
        // para relacionarla con las asignaciones
        public IList<Assignment> Assignment { get; set; }
    }
}
