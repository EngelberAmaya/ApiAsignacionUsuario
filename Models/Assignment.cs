using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace apiUsuarioCrud.Models
{
    public class Assignment
    {
        //public long Id { get; set; }
        //[StringLength(30), Required]

        public long UserID { get; set; }
        public User User { get; set; }

        public long SoftwareID { get; set; }
        public Software Software { get; set; }

        public long HardwareID { get; set; }
        public Hardware Hardware { get; set; }
    }
}
