using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sdiaz.SimpleChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdiaz.SimpleChat.DataRepositories.Context
{
    public class SimpleChatDbContext : IdentityDbContext
    {

        public SimpleChatDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
