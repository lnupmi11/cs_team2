﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using xBlogger.EF;
using xBlogger.Models;

namespace xBlogger.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180503152419_add-migration News")]
    partial class addmigrationNews
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("xBlogger.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("xBlogger.Models.Assigment", b =>
                {
                    b.Property<string>("AssigmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Deadline");

                    b.Property<string>("DetailedDescription");

                    b.Property<int>("Format");

                    b.Property<long>("MaxBudget");

                    b.Property<int>("Network");

                    b.Property<string>("ShortDescription");

                    b.Property<int>("Type");

                    b.Property<string>("UserProfileId");

                    b.HasKey("AssigmentId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Assigments");
                });

            modelBuilder.Entity("xBlogger.Models.Chanel", b =>
                {
                    b.Property<string>("ChanelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AvgLikeNum");

                    b.Property<long>("AvgViewNum");

                    b.Property<int>("Category");

                    b.Property<string>("Description");

                    b.Property<double>("LocalRank");

                    b.Property<int>("Network");

                    b.Property<long>("SubscribersNum");

                    b.Property<string>("UserProfileId");

                    b.HasKey("ChanelId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Chanels");
                });

            modelBuilder.Entity("xBlogger.Models.Deal", b =>
                {
                    b.Property<string>("DealId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssigmentId");

                    b.Property<string>("ChanelId");

                    b.Property<bool>("IsConfirmed");

                    b.Property<bool>("IsRead");

                    b.Property<string>("RecipientId");

                    b.Property<string>("SenderId");

                    b.HasKey("DealId");

                    b.HasIndex("AssigmentId");

                    b.HasIndex("ChanelId");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("xBlogger.Models.News", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatePublished");

                    b.Property<string>("Image");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("xBlogger.Models.UserProfile", b =>
                {
                    b.Property<string>("Id");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<string>("FirstName");

                    b.Property<string>("ImageName");

                    b.Property<string>("SecondName");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("xBlogger.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("xBlogger.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("xBlogger.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("xBlogger.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("xBlogger.Models.Assigment", b =>
                {
                    b.HasOne("xBlogger.Models.UserProfile", "UserProfile")
                        .WithMany("Assigments")
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("xBlogger.Models.Chanel", b =>
                {
                    b.HasOne("xBlogger.Models.UserProfile", "UserProfile")
                        .WithMany("Chanels")
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("xBlogger.Models.Deal", b =>
                {
                    b.HasOne("xBlogger.Models.Assigment", "Assigment")
                        .WithMany()
                        .HasForeignKey("AssigmentId");

                    b.HasOne("xBlogger.Models.Chanel", "Chanel")
                        .WithMany()
                        .HasForeignKey("ChanelId");
                });

            modelBuilder.Entity("xBlogger.Models.UserProfile", b =>
                {
                    b.HasOne("xBlogger.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("UserProfile")
                        .HasForeignKey("xBlogger.Models.UserProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
