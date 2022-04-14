using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using WebProject.Models.IdentityModels;

namespace WebProject.AppDbContextLayer
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public IConfiguration Configuration { get; }

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Product> products { get; set; }

        public DbSet<Commodity> commodities { get; set; }

        public DbSet<Categorize> categorizes { get; set; }

        public DbSet<ProductImage> productImages { get; set; }

        public DbSet<Company> companies { get; set; }
        public DbSet<Lines> lines { get; set; }

        public DbSet<DataImage> dataImages { get; set; }

        //public DbSet<CategorizeProduct> categorizeProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ContextConnectionLC"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var item in builder.Model.GetEntityTypes())
            {
                var tableName = item.GetTableName();

                if (tableName.StartsWith("AspNet"))
                {
                    item.SetTableName(tableName.Substring(6));
                }

            }

            //Khóa Chính
            builder.Entity<Product>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<Commodity>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<Categorize>().Property(x => x.Id).HasDefaultValueSql("NEWID()");          
            builder.Entity<DataImage>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<Lines>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<Company>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            //Cấu hình 1 lúc hai khóa chính
            //builder.Entity<ProductImage>().HasKey(l => new { l.DataImageId, l.ProdtuctId });
            //Mối quan hệ 1 nhiều

            builder.Entity<Product>()
            .HasOne(s => s.Lines)
            .WithMany(g => g.Products)
            .HasForeignKey(s => s.LinesId);

            builder.Entity<DataImage>()
            .HasOne(s => s.Lines)
            .WithMany(g => g.DataImages)
            .HasForeignKey(s => s.LinesId);

            builder.Entity<ProductImage>()
            .HasOne(s => s.Product)
            .WithMany(g => g.ProductImages)
            .HasForeignKey(s => s.ProdtuctId);

            builder.Entity<Categorize>()
            .HasOne(s => s.Commodity)
            .WithMany(g => g.Categorizes)
            .HasForeignKey(s => s.CommotityId);

            builder.Entity<Categorize>()
            .HasOne(s => s.Company)
            .WithMany(g => g.categorizes)
            .HasForeignKey(s => s.CompanyId);

            builder.Entity<Lines>()
            .HasOne(s => s.Categorize)
            .WithMany(g => g.Lines)
            .HasForeignKey(s => s.CategorizeId);



            //Đánh dấu chỉ 
            builder.Entity<Product>().HasIndex(x => new { x.Code }).IsUnique();
            builder.Entity<Commodity>().HasIndex(x => new { x.Code }).IsUnique();
            builder.Entity<DataImage>().HasIndex(x => new { x.UrlImg }).IsUnique();
            builder.Entity<Categorize>().HasIndex(c => new { c.Code }).IsUnique();        
            builder.Entity<Lines>().HasIndex(c => new { c.Code }).IsUnique();
            builder.Entity<Company>().HasIndex(c => new { c.Code }).IsUnique();


            //many to manay
            builder.Entity<ProductImage>().HasKey(sc => new { sc.DataImageId, sc.ProdtuctId });
            builder.Entity<ProductImage>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.ProductImages)
                .HasForeignKey(sc => sc.ProdtuctId);

            builder.Entity<ProductImage>()
                .HasOne<DataImage>(sc => sc.DataImage)
                .WithMany(s => s.ProductImages)
                .HasForeignKey(sc => sc.DataImageId);
        }
     
    }   
}
