using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataBaseLibrary
{
    class DataContext : DbContext
    {
        public DataContext() : base("DataConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
