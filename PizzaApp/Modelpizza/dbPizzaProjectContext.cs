using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PizzaApp.Modelpizza
{
    public partial class DbPizzaProjectContext : DbContext
    {
        public DbPizzaProjectContext()
        {
        }

        public DbPizzaProjectContext(DbContextOptions<DbPizzaProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }
        public virtual DbSet<OrdersNumberDetail> OrdersNumberDetails { get; set; }
        public virtual DbSet<PizzaName> PizzaNames { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                if (System.Environment.MachineName == "DESKTOP-8PS4036")
                {
                    optionsBuilder.UseSqlServer("Data source=DESKTOP-8PS4036\\KANINISQL2019; Integrated Security=true; Initial catalog=dbPizzaProject");
                }
                else if (System.Environment.MachineName == "KANINI-LTP-534")
                {
                    optionsBuilder.UseSqlServer("Data Source=KANINI-LTP-534\\SQLSERVERPAVI; user id=sa; password=murugi@1999; Initial catalog=dbPizzaProject");

                }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Delivercharge)
                    .HasColumnName("delivercharge");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__user_id__3C69FB99");
            });

            modelBuilder.Entity<OrdersDetail>(entity =>
            {
                entity.HasKey(e => e.OrdersDetailsId)
                    .HasName("PK__OrdersDe__232A061D38F86718");

                entity.Property(e => e.OrdersDetailsId).HasColumnName("ordersDetails_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PizzaNumber).HasColumnName("pizza_number");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrdersDet__order__3F466844");

                entity.HasOne(d => d.PizzaNumberNavigation)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.PizzaNumber)
                    .HasConstraintName("FK__OrdersDet__pizza__403A8C7D");
            });

            modelBuilder.Entity<OrdersNumberDetail>(entity =>
            {
               
                entity.HasKey(e => new { e.ID })
                    .HasName("ID");
               
                entity.Property(e => e.OrdersNumberDetailsId).HasColumnName("ordersNumberDetails_id");

                entity.Property(e => e.TopppingId).HasColumnName("toppping_id");

                entity.HasOne(d => d.OrdersNumberDetails)
                    .WithMany(p => p.OrdersNumberDetails)
                    .HasForeignKey(d => d.OrdersNumberDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersNum__order__5CD6CB2B");

                entity.HasOne(d => d.Toppping)
                    .WithMany(p => p.OrdersNumberDetails)
                    .HasForeignKey(d => d.TopppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersNum__toppp__5DCAEF64");
            });

            

            modelBuilder.Entity<PizzaName>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("PK__pizzaNam__52B89DE3F6FFFACF");

                entity.ToTable("pizzaName");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.Property(e => e.ToppingId).HasColumnName("topping_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Users__AB6E61651D769263");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Adress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adress");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

#pragma warning disable S3251 // Implementations should be provided for "partial" methods
            OnModelCreatingPartial(modelBuilder);
#pragma warning restore S3251 // Implementations should be provided for "partial" methods
        }

#pragma warning disable S3251 // Implementations should be provided for "partial" methods
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
#pragma warning restore S3251 // Implementations should be provided for "partial" methods
    }
}
