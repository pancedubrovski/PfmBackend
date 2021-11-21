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
    [Migration("20211121141042_ChangeName2")]
    partial class ChangeName2
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

            modelBuilder.Entity("PmfBackend.Database.Entities.MccEntity", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("MerchactType")
                        .HasColumnType("text");

                    b.HasKey("Code");

                    b.ToTable("MccCodes");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.SplitTransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("CatCode")
                        .HasColumnType("text");

                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CatCode");

                    b.HasIndex("TransactionId");

                    b.ToTable("splitTransactions");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.TransactionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision")
                        .HasColumnName("amount");

                    b.Property<string>("BeneficiaryName")
                        .HasColumnType("text")
                        .HasColumnName("beneficiaryname");

                    b.Property<string>("CatCode")
                        .HasColumnType("text")
                        .HasColumnName("catcode");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character(3)")
                        .HasColumnName("currency")
                        .IsFixedLength(true);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Direction")
                        .HasColumnType("integer")
                        .HasColumnName("direction");

                    b.Property<bool>("IsSplit")
                        .HasColumnType("boolean")
                        .HasColumnName("issplit");

                    b.Property<int>("Kind")
                        .HasColumnType("integer")
                        .HasColumnName("kind");

                    b.Property<string>("Mcc")
                        .HasColumnType("text")
                        .HasColumnName("mcc");

                    b.HasKey("Id");

                    b.HasIndex("CatCode");

                    b.HasIndex("Mcc");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("PmfBackend.Models.AnalyticsModel", b =>
                {
                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("CatCode")
                        .HasColumnType("text");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.ToTable("AnalyticsModels");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.CategoryEntity", b =>
                {
                    b.HasOne("PmfBackend.Database.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryCode");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.SplitTransactionEntity", b =>
                {
                    b.HasOne("PmfBackend.Database.Entities.CategoryEntity", "CategoryEntity")
                        .WithMany("Splits")
                        .HasForeignKey("CatCode");

                    b.HasOne("PmfBackend.Database.Entities.TransactionEntity", "Transaction")
                        .WithMany("splits")
                        .HasForeignKey("TransactionId");

                    b.Navigation("CategoryEntity");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.TransactionEntity", b =>
                {
                    b.HasOne("PmfBackend.Database.Entities.CategoryEntity", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CatCode");

                    b.HasOne("PmfBackend.Database.Entities.MccEntity", "MccEntity")
                        .WithMany("transactions")
                        .HasForeignKey("Mcc");

                    b.Navigation("Category");

                    b.Navigation("MccEntity");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Splits");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.MccEntity", b =>
                {
                    b.Navigation("transactions");
                });

            modelBuilder.Entity("PmfBackend.Database.Entities.TransactionEntity", b =>
                {
                    b.Navigation("splits");
                });
#pragma warning restore 612, 618
        }
    }
}
