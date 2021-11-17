using Microsoft.EntityFrameworkCore;
using Omega.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.Model
{
    public class InventoryContext: DbContext
    {

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
