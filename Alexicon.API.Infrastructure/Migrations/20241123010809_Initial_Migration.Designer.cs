﻿// <auto-generated />
using System;
using Alexicon.API.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Alexicon.API.Infrastructure.Migrations
{
    [DbContext(typeof(AlexiconContext))]
    [Migration("20241123010809_Initial_Migration")]
    partial class Initial_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("Alexicon.API.Infrastructure.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Alexicon.API.Infrastructure.Entities.GameMove", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("LettersUsed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LettersUsedForDb")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<short>("LocationX")
                        .HasColumnType("INTEGER");

                    b.Property<short>("LocationY")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<short>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WordsCreated")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WordsCreatedForDb")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GameMove");
                });

            modelBuilder.Entity("Alexicon.API.Infrastructure.Entities.GamePlayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentRack")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentRackForDb")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerUsername")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerUsername");

                    b.ToTable("GamePlayer");
                });

            modelBuilder.Entity("Alexicon.API.Infrastructure.Entities.Player", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.HasKey("Username");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Alexicon.API.Infrastructure.Entities.GameMove", b =>
                {
                    b.HasOne("Alexicon.API.Infrastructure.Entities.Game", "Game")
                        .WithMany("MovesPlayed")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alexicon.API.Infrastructure.Entities.GamePlayer", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Alexicon.API.Infrastructure.Entities.GamePlayer", b =>
                {
                    b.HasOne("Alexicon.API.Infrastructure.Entities.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alexicon.API.Infrastructure.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Alexicon.API.Infrastructure.Entities.Game", b =>
                {
                    b.Navigation("MovesPlayed");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
