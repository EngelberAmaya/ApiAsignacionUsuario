using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;

using System.IO;
using System.Dynamic;

//using System.Data.SqlClient;
using Dapper;
using System.Data.SqlClient;

namespace apiUsuarioCrud.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // para configurar las claves conpuestas 
            modelBuilder.Entity<Assignment>().HasKey(asig => new { asig.HardwareID, asig.SoftwareID, asig.UserID });

            //asignando las propiedades, para las relaciones de las claves compuestas
            modelBuilder.Entity<Assignment>().HasOne(asig => asig.User)
                                             .WithMany(asig => asig.Assignment)
                                             .HasForeignKey(asig => asig.UserID);

            modelBuilder.Entity<Assignment>().HasOne(asig => asig.Software)
                                             .WithMany(asig => asig.Assignment)
                                             .HasForeignKey(asig => asig.SoftwareID);

            modelBuilder.Entity<Assignment>().HasOne(asig => asig.Hardware)
                                             .WithMany(asig => asig.Assignment)
                                             .HasForeignKey(asig => asig.HardwareID);
        }

        public DbSet<User> Users { get; set; }  
        public DbSet<Hardware> Hardware { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

    }


    
    
    


    






}
