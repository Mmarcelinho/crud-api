using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Data
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options): base(options)
        {}

        public DbSet<Todo> Todo { get; set; }
    }
}

