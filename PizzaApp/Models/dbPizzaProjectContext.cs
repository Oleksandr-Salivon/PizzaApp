using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PizzaApp.Models
{
    public partial class dbPizzaProjectContext : DbContext
    {
        public dbPizzaProjectContext()
        {
        }

        public dbPizzaProjectContext(DbContextOptions<dbPizzaProjectContext> options)
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
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data source=DESKTOP-8PS4036\\KANINISQL2019; Integrated Security=true; Initial catalog=dbPizzaProject");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Delivercharge)
                    .HasMaxLength(50)
                    .IsUnicode(false)
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
                    .HasConstraintName("FK__Orders__user_id__45F365D3");
            });

            modelBuilder.Entity<OrdersDetail>(entity =>
            {
                entity.HasKey(e => e.OrdersDetailsId)
                    .HasName("PK__OrdersDe__232A061D465C97E6");

                entity.Property(e => e.OrdersDetailsId).HasColumnName("ordersDetails_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PizzaNumber).HasColumnName("pizza_number");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrdersDet__order__48CFD27E");

                entity.HasOne(d => d.PizzaNumberNavigation)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.PizzaNumber)
                    .HasConstraintName("FK__OrdersDet__pizza__49C3F6B7");
            });

            modelBuilder.Entity<OrdersNumberDetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.OrdersNumberDetailsId).HasColumnName("ordersNumberDetails_id");

                entity.Property(e => e.TopppingId).HasColumnName("toppping_id");

                entity.HasOne(d => d.OrdersNumberDetails)
                    .WithMany()
                    .HasForeignKey(d => d.OrdersNumberDetailsId)
                    .HasConstraintName("FK__OrdersNum__order__4BAC3F29");

                entity.HasOne(d => d.Toppping)
                    .WithMany()
                    .HasForeignKey(d => d.TopppingId)
                    .HasConstraintName("FK__OrdersNum__toppp__4CA06362");
            });

            modelBuilder.Entity<PizzaName>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("PK__pizzaNam__52B89DE3D0C2FEA5");

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
                    .HasName("PK__Users__AB6E6165E069DD9D");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
