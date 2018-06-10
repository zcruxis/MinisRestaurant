using MinisBack.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MinisBack.Data
{
    public class MinisBackContext : DbContext
    {
        public MinisBackContext() : base("MinisContext")
        {
        }

        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<SandwichEntity> Sandwichs { get; set; }
        public DbSet<SandwichTypeEntity> SandwichTypes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderEntity>().ToTable("Order");
            modelBuilder.Entity<OrderItemEntity>().ToTable("OrderItem");
            modelBuilder.Entity<SandwichEntity>().ToTable("Sandwich");
            modelBuilder.Entity<SandwichTypeEntity>().ToTable("SandwichType");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<OrderEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<SandwichEntity>()
                .HasMany<Ingredient>(s => s.Ingredients)
                .WithMany(c => c.Sandwichs)
                .Map(cs =>
                {
                    cs.MapLeftKey("SandwichId");
                    cs.MapRightKey("IngredientId");
                    cs.ToTable("SandwichIngredient");
                });
        }
    }
}
