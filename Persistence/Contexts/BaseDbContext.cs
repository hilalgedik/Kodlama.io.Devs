using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Github> Githubs { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");

                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");

                a.Property(p => p.Name).HasColumnName("Name");

                a.HasOne(p => p.ProgrammingLanguage);
               
            });


            ProgrammingLanguage[] programmingLanguagesEntitySeeds =
            {
                new(5, "C#"),
                new(6, "Java"),
                //new(4,"JavaScript")
            };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguagesEntitySeeds);


            Technology[] technologiesEntitySeeds = {
                new(1,5, "WPF"),
                new(2, 5, "ASP.NET"),
                new(3,6, "Spring"),
                new(4,6, "JSP"),
                //new(6,4, "Vue"),
                //new(7,4, "React")
                };
            modelBuilder.Entity<Technology>().HasData(technologiesEntitySeeds);

            //add-migration Add-User-Operation-Claim

            modelBuilder.Entity<User>(p =>
            {
                p.ToTable("Users").HasKey(u => u.Id);
                p.Property(u => u.Id).HasColumnName("Id");
                p.Property(u => u.FirstName).HasColumnName("FirstName");
                p.Property(u => u.LastName).HasColumnName("LastName");
                p.Property(u => u.Email).HasColumnName("Email");
                p.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                p.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                p.Property(u => u.Status).HasColumnName("Status");
                p.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
                p.HasMany(c => c.UserOperationClaims);
                p.HasMany(c => c.RefreshTokens);
            });

            //User[] usersEntitySeeds =
            //{
            //    new(1, "Hilal","Gedik","hilal@mail.com",[1],[1],true,AuthenticatorType.Email),
            //    new(2, "Irem","Gulec","irem@mail.com",1,1,true,AuthenticatorType.Email)
            //};
            //modelBuilder.Entity<OperationClaim>().HasData(usersEntitySeeds);



            modelBuilder.Entity<OperationClaim>(p =>
            {
                p.ToTable("OperationClaims").HasKey(o => o.Id);
                p.Property(o => o.Id).HasColumnName("Id");
                p.Property(o => o.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(p =>
            {
                p.ToTable("UserOperationClaims").HasKey(u => u.Id);
                p.Property(u => u.Id).HasColumnName("Id");
                p.Property(u => u.UserId).HasColumnName("UserId");
                p.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                p.HasOne(u => u.User);
                p.HasOne(u => u.OperationClaim);
            });

            OperationClaim[] operationClaimsEntitySeeds =
            {
                new(1, "Admin"),
                new(2, "User")
            };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimsEntitySeeds);


            modelBuilder.Entity<Github>(p =>
            {
                p.ToTable("Githubs").HasKey(o => o.Id);
                p.Property(o => o.Id).HasColumnName("Id");
                p.Property(o => o.UserId).HasColumnName("UserId");
                p.Property(o => o.GithubUrl).HasColumnName("GithubUrl");
            });

            //Github[] githubsEntitySeeds =
            //{
            //    new(1, 1,"https://github.com/hilalgedik"),
            //    new(2, 2,"https://github.com/iremgulec")
            //};
            //modelBuilder.Entity<Github>().HasData(githubsEntitySeeds);

        }
    }
}
