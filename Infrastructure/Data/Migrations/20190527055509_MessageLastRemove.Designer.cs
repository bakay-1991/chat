﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20190527055509_MessageLastRemove")]
    partial class MessageLastRemove
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            Name = "Group 1"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            Name = "Group 2"
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GroupId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId", "GroupId")
                        .IsUnique();

                    b.ToTable("GroupUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000006"),
                            GroupId = new Guid("00000000-0000-0000-0000-000000000004"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000007"),
                            GroupId = new Guid("00000000-0000-0000-0000-000000000004"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            GroupId = new Guid("00000000-0000-0000-0000-000000000004"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000003")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000009"),
                            GroupId = new Guid("00000000-0000-0000-0000-000000000005"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000010"),
                            GroupId = new Guid("00000000-0000-0000-0000-000000000005"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000003")
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("FromUserId");

                    b.Property<string>("Text");

                    b.Property<Guid?>("ToGroupId");

                    b.Property<Guid?>("ToUserId");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToGroupId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ApplicationCore.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Email = "user1@gmail.com",
                            Name = "user1",
                            Password = "123456"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Email = "user2@gmail.com",
                            Name = "user2",
                            Password = "123456"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            Email = "user3@gmail.com",
                            Name = "user3",
                            Password = "123456"
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupUser", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationCore.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationCore.Entities.Message", b =>
                {
                    b.HasOne("ApplicationCore.Entities.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationCore.Entities.Group", "ToGroup")
                        .WithMany()
                        .HasForeignKey("ToGroupId");

                    b.HasOne("ApplicationCore.Entities.User", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
