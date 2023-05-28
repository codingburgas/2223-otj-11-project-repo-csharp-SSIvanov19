﻿// <auto-generated />
using CBCanteen.Server.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CBCanteen.Server.WebHost.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230526101758_AddMenuModels")]
    partial class AddMenuModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CBCanteen.Server.Data.Models.Canteen.Meal", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("CBCanteen.Server.Data.Models.Canteen.Menu", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppetizerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DessertId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MainDishId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AppetizerId");

                    b.HasIndex("DessertId");

                    b.HasIndex("MainDishId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("CBCanteen.Server.Data.Models.Canteen.MenuPrice", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("MenuPrices");
                });

            modelBuilder.Entity("CBCanteen.Server.Data.Models.Canteen.Menu", b =>
                {
                    b.HasOne("CBCanteen.Server.Data.Models.Canteen.Meal", "Appetizer")
                        .WithMany()
                        .HasForeignKey("AppetizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CBCanteen.Server.Data.Models.Canteen.Meal", "Dessert")
                        .WithMany()
                        .HasForeignKey("DessertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CBCanteen.Server.Data.Models.Canteen.Meal", "MainDish")
                        .WithMany()
                        .HasForeignKey("MainDishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appetizer");

                    b.Navigation("Dessert");

                    b.Navigation("MainDish");
                });
#pragma warning restore 612, 618
        }
    }
}
