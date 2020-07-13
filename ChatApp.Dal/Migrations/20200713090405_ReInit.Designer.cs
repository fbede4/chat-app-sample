﻿// <auto-generated />
using System;
using ChatApp.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChatApp.Dal.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20200713090405_ReInit")]
    partial class ReInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("ChatApp.Domain.Model.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FirstParticipantUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SecondParticipantUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FirstParticipantUserId");

                    b.HasIndex("SecondParticipantUserId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("ChatApp.Domain.Model.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConversationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SentByUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("SentByUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatApp.Domain.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatApp.Domain.Model.Conversation", b =>
                {
                    b.HasOne("ChatApp.Domain.Model.User", "FirstParticipantUser")
                        .WithMany()
                        .HasForeignKey("FirstParticipantUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatApp.Domain.Model.User", "SecondParticipantUser")
                        .WithMany()
                        .HasForeignKey("SecondParticipantUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ChatApp.Domain.Model.Message", b =>
                {
                    b.HasOne("ChatApp.Domain.Model.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatApp.Domain.Model.User", "SentByUser")
                        .WithMany()
                        .HasForeignKey("SentByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
