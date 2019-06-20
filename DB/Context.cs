using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DB.Model;

namespace DB
{
    public class Context: DbContext
    {
        public Context(string connectionString) : base(connectionString)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
