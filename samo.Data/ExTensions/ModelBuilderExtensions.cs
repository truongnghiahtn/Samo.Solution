using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using samo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.ExTensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MakeMoney>().HasData(
                new MakeMoney() { Id=1,Name="Ăn uống",Description="chiphi",Img="eating.png",Status=true,},
                new MakeMoney() { Id = 2, Name = "Quần áo", Description = "chiphi", Img = "clothes.png", Status = true, },
                new MakeMoney() { Id = 3, Name = "Hoa quả", Description = "chiphi", Img = "fruit.png", Status = true, },
                new MakeMoney() { Id = 4, Name = "Giao thông", Description = "chiphi", Img = "traffic.png", Status = true, },
                new MakeMoney() { Id = 5, Name = "Nhà ở", Description = "chiphi", Img = "house.png", Status = true, }
            );

            modelBuilder.Entity<Spend>().HasData(
                 new Spend() { Id = 1, Name = "Cho thuê", Description = "thunhap", Img = "lease.png", Status = true, },
                 new Spend() { Id = 2, Name = "Quyên góp", Description = "thunhap", Img = "donation.png", Status = true, },
                 new Spend() { Id = 3, Name = "Bán hàng", Description = "thunhap", Img = "sell.png", Status = true, },
                 new Spend() { Id = 4, Name = "Cổ tức", Description = "thunhap", Img = "devidend.png", Status = true, },
                 new Spend() { Id = 5, Name = "Hoàn tiền", Description = "thunhap", Img = "refund.png", Status = true, },
                 new Spend() { Id = 6, Name = "Tiền lương", Description = "thunhap", Img = "salary.png", Status = true, }
                );

            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "truongnghiahtn@gmail.com",
                NormalizedEmail = "truongnghiahtn@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456789"),
                SecurityStamp = string.Empty,
                FirstName = "Nghia",
                LastName = "Truong",
                LimitMoney=100000,
                AccountBalance=0
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
