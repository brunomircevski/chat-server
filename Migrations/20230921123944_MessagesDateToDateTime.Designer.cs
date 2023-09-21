﻿// <auto-generated />
using System;
using Chat.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chat.Migrations
{
    [DbContext(typeof(MysqlDB))]
    [Migration("20230921123944_MessagesDateToDateTime")]
    partial class MessagesDateToDateTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Chat.Models.Channel", b =>
                {
                    b.Property<string>("uuid")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("accessKey")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateOnly>("dateCreated")
                        .HasColumnType("date");

                    b.HasKey("uuid");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("Chat.Models.Invite", b =>
                {
                    b.Property<string>("uuid")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("accessKey")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.Property<string>("encryptedKey")
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("useruuid")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("uuid");

                    b.HasIndex("useruuid");

                    b.ToTable("Invites");
                });

            modelBuilder.Entity("Chat.Models.Message", b =>
                {
                    b.Property<string>("uuid")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("channeluuid")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasMaxLength(131072)
                        .HasColumnType("longtext");

                    b.Property<DateTime>("dateCreated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("uuid");

                    b.HasIndex("channeluuid");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Chat.Models.User", b =>
                {
                    b.Property<string>("uuid")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<bool>("acceptsInvites")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("encryptedInvitesData")
                        .HasMaxLength(131072)
                        .HasColumnType("longtext");

                    b.Property<string>("encryptedUserData")
                        .HasMaxLength(131072)
                        .HasColumnType("longtext");

                    b.Property<string>("publicKey")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("uuid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chat.Models.Invite", b =>
                {
                    b.HasOne("Chat.Models.User", "user")
                        .WithMany("Invites")
                        .HasForeignKey("useruuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Chat.Models.Message", b =>
                {
                    b.HasOne("Chat.Models.Channel", "channel")
                        .WithMany()
                        .HasForeignKey("channeluuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("channel");
                });

            modelBuilder.Entity("Chat.Models.User", b =>
                {
                    b.Navigation("Invites");
                });
#pragma warning restore 612, 618
        }
    }
}
