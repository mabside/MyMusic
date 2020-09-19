using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MyMusic.Core.Models;
using MyMusic.Data.Configurations;

namespace MyMusic.Data
{
    public class MyMusicDbContext : IdentityDbContext<User,Role,Guid>
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public MyMusicDbContext(DbContextOptions<MyMusicDbContext> options): base(options)
        {
            // if(options == )

            // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {


            // builder.Entity<User>().ToTable("Users");

            // builder.Entity<IdentityRole<string>>().ToTable("Roles");
            // builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            // builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            // builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            // builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            // builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            
            // builder.Entity<User>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            // builder.Entity<User>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            // builder.Entity<User>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            // builder.Entity<Role>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            // builder.Entity<Role>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            // builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            // builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            // builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            // builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            // builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            // builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            // builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            // builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            // builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            // builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            // builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            // builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));



            
            builder.ApplyConfiguration(new MusicConfiguration());
            builder.ApplyConfiguration(new ArtistConfiguration());

            base.OnModelCreating(builder);

        }
    }
}
