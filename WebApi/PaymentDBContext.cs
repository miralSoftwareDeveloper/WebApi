using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebApi.Models
{
    public class PaymentDBContext : DbContext
    {
        public PaymentDBContext() : base("PaymentDB") 
        {
            Database.SetInitializer<PaymentDBContext>(new CreateDatabaseIfNotExists<PaymentDBContext>());

        }

        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        
    }
}

///http://www.leutbounpaseuth.me/dotnet/asp-net-core-web-api-with-ef-core-database-first/