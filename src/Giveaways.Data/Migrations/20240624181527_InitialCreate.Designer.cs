﻿// <auto-generated />
using System;
using Giveaways.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Giveaways.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240624181527_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Giveaways.Data.Giveaway", b =>
                {
                    b.Property<ulong>("MessageId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxWinners")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Prize")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("MessageId");

                    b.ToTable("Giveaways");
                });

            modelBuilder.Entity("Giveaways.Data.GiveawayParticipant", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("GiveawayId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "GiveawayId");

                    b.HasIndex("GiveawayId");

                    b.ToTable("GiveawayParticipants");
                });

            modelBuilder.Entity("Giveaways.Data.GiveawayParticipant", b =>
                {
                    b.HasOne("Giveaways.Data.Giveaway", "Giveaway")
                        .WithMany("Participants")
                        .HasForeignKey("GiveawayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Giveaway");
                });

            modelBuilder.Entity("Giveaways.Data.Giveaway", b =>
                {
                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}
