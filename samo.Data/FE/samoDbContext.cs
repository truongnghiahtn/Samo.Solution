using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using samo.Data.Configurations;
using samo.Data.Entities;
using samo.Data.ExTensions;
using System;

namespace samo.Data.FE
{
    public class samoDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public samoDbContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
            modelBuilder.ApplyConfiguration(new SpendConfiguration());
            modelBuilder.ApplyConfiguration(new RegisterMakeMoneyConfiguration());
            modelBuilder.ApplyConfiguration(new MakeMoneyConfiguration());  
            modelBuilder.ApplyConfiguration(new RegisterSpendCongiguraton());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());


            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            //data seeding

            modelBuilder.Seed();

        }

        // khởi tạo các bảng của sql

        public DbSet<MakeMoney> MakeMoneys { get; set; }
        public DbSet<Spend> Spends { get; set; }
        public DbSet<RegisterSpend> RegisterSpends { get; set; }
        public DbSet<RegisterMakeMoney> RegisterMakeMoneys { get; set; }
    }
}
