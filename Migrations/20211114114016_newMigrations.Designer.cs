﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PmfBackend.Database;

namespace PmfBackend.Migrations
{
    [DbContext(typeof(TransactionDbContext))]
    [Migration("20211114114016_newMigrations")]
    partial class newMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PmfBackend.Database.Entities.CategoryEntity", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CategoryCode")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParentCode")
                        .HasColumnType("text");

                    b.HasKey("Code");

                    b.HasIndex("CategoryCode");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.TransactionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Amount")
                        .HasColumnType("text");

                    b.Property<string>("BeneficiaryName")
                        .HasColumnType("text");

                    b.Property<string>("CatCode")
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Direction")
                        .HasColumnType("text");

                    b.Property<string>("Kind")
                        .HasColumnType("text");

                    b.Property<string>("Mcc")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CatCode");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.CategoryEntity", b =>
                {
                    b.HasOne("PmfBackend.Database.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryCode");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.TransactionEntity", b =>
                {
                    b.HasOne("PmfBackend.Database.Entities.CategoryEntity", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CatCode");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
