﻿// <auto-generated />
using System;
using FinanceManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceManager.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240414173210_AddShouldOverwriteSetting")]
    partial class AddShouldOverwriteSetting
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinanceManager.Common.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Friendship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPending")
                        .HasColumnType("boolean");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Friendship");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FriendshipId")
                        .HasColumnType("uuid");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uuid");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("FriendshipId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.RecurringTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastTransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("NextTransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RecipientAccountId")
                        .HasColumnType("uuid");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SenderAccountId")
                        .HasColumnType("uuid");

                    b.Property<int>("TransactionInterval")
                        .HasColumnType("integer");

                    b.Property<string>("TransactionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("RecurringTransaction");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Role");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<bool>("HasBeenSimulated")
                        .HasColumnType("boolean");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<bool>("ShouldOverwrite")
                        .HasColumnType("boolean");

                    b.Property<bool>("ShouldSimulate")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RecipientAccountId")
                        .HasColumnType("uuid");

                    b.Property<bool>("RecurringTransaction")
                        .HasColumnType("boolean");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SenderAccountId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastOnline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.UserFriendship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FriendshipId")
                        .HasColumnType("uuid");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("FriendshipId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFriendship");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EditedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("WasSimulated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Account", b =>
                {
                    b.HasOne("FinanceManager.Common.Entities.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Message", b =>
                {
                    b.HasOne("FinanceManager.Common.Entities.Friendship", null)
                        .WithMany("Messages")
                        .HasForeignKey("FriendshipId");

                    b.HasOne("FinanceManager.Common.Entities.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceManager.Common.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.RecurringTransaction", b =>
                {
                    b.HasOne("FinanceManager.Common.Entities.Account", null)
                        .WithMany("RecurringTransactions")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.UserFriendship", b =>
                {
                    b.HasOne("FinanceManager.Common.Entities.Friendship", "Friendship")
                        .WithMany("UserFriendships")
                        .HasForeignKey("FriendshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Friendship_Users");

                    b.HasOne("FinanceManager.Common.Entities.User", "User")
                        .WithMany("UserFriendships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Friendship");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.UserRole", b =>
                {
                    b.HasOne("FinanceManager.Common.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("FinanceManager.Common.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Account", b =>
                {
                    b.Navigation("RecurringTransactions");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.Friendship", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserFriendships");
                });

            modelBuilder.Entity("FinanceManager.Common.Entities.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Roles");

                    b.Navigation("UserFriendships");
                });
#pragma warning restore 612, 618
        }
    }
}
