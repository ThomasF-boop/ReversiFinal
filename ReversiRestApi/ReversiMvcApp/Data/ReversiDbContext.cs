﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Data
{
    public class ReversiDbContext : DbContext
    {
        public ReversiDbContext (DbContextOptions<ReversiDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReversiMvcApp.Models.Speler> Speler { get; set; } = default!;
    }
}
