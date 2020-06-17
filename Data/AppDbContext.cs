﻿using BlogAdaia.Models;
using BlogAdaia.Models.Comments;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAdaia.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ): base(options)
        {
           

        }
        public DbSet <Post> Posts { get; set; }
        public DbSet<MainComment> MainComments { get; set; }       
        public DbSet<SubComment> SubComments { get; set; }

    }
}
