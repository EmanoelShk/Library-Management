﻿// <auto-generated />
using System;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagement.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20240527005449_InitialCreate.02")]
    partial class InitialCreate02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("LibraryManagement.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CopiesAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "George Orwell",
                            CopiesAvailable = 3,
                            PublicationYear = 1949,
                            Title = "1984"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Aldous Huxley",
                            CopiesAvailable = 2,
                            PublicationYear = 1932,
                            Title = "Brave New World"
                        });
                });

            modelBuilder.Entity("LibraryManagement.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "John",
                            Gender = "Male",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1985, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Jane",
                            Gender = "Female",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("LibraryManagement.Models.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("OverdueFee")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ClientId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("LibraryManagement.Models.Loan", b =>
                {
                    b.HasOne("LibraryManagement.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagement.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Client");
                });
#pragma warning restore 612, 618
        }
    }
}
