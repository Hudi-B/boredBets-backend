﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace boredBets.Models;

public partial class BoredbetsContext : DbContext
{
    public BoredbetsContext()
    {
    }

    public BoredbetsContext(DbContextOptions<BoredbetsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Horse> Horses { get; set; }

    public virtual DbSet<Jockey> Jockeys { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBet> UserBets { get; set; }

    public virtual DbSet<UserCard> UserCards { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        if (!optionsBuilder.IsConfigured)

        {

            IConfigurationRoot configuration = new ConfigurationBuilder()

                .SetBasePath(Directory.GetCurrentDirectory())

                .AddJsonFile("appsettings.json")

                .Build();



            string connectionString = configuration.GetConnectionString("YourConnectionString");



            optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

        }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Horse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("horses");

            entity.HasIndex(e => e.JockeyId, "fk_horses_jockey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.JockeyId).HasColumnName("jockey_id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Stallion).HasColumnName("stallion");

            entity.HasOne(d => d.Jockey).WithMany(p => p.Horses)
                .HasForeignKey(d => d.JockeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_horses_jockey");
        });

        modelBuilder.Entity<Jockey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("jockey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Male).HasColumnName("male");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.Quality).HasColumnName("quality");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("participants");

            entity.HasIndex(e => e.HorseId, "horse_id");

            entity.HasIndex(e => e.RaceId, "race_id");

            entity.Property(e => e.HorseId).HasColumnName("horse_id");
            entity.Property(e => e.Placement).HasColumnName("placement");
            entity.Property(e => e.RaceId).HasColumnName("race_id");

            entity.HasOne(d => d.Horse).WithMany(p => p.Participants)
                .HasForeignKey(d => d.HorseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("participant_ibfk_2");

            entity.HasOne(d => d.Race).WithMany(p => p.Participants)
                .HasForeignKey(d => d.RaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("participant_ibfk_1");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("races");

            entity.HasIndex(e => e.TrackId, "track_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RaceScheduled)
                .HasColumnType("datetime")
                .HasColumnName("race_scheduled");
            entity.Property(e => e.RaceTime).HasColumnName("race_time");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.Weather)
                .HasMaxLength(32)
                .HasColumnName("weather");

            entity.HasOne(d => d.Track).WithMany(p => p.Races)
                .HasForeignKey(d => d.TrackId)
                .HasConstraintName("races_ibfk_1");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tracks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(124)
                .HasColumnName("address");
            entity.Property(e => e.Country)
                .HasMaxLength(124)
                .HasColumnName("country");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Name)
                .HasMaxLength(124)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.Created)
                .HasMaxLength(6)
                .HasColumnName("created");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .HasColumnName("refresh_token");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
            entity.Property(e => e.Wallet).HasColumnName("wallet");
        });

        modelBuilder.Entity<UserBet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_bets");

            entity.HasIndex(e => e.UserId, "IX_user_bets_user_id");

            entity.HasIndex(e => e.HorseId, "horse_id1");

            entity.HasIndex(e => e.RaceId, "race_id1");

            entity.Property(e => e.BetAmount).HasColumnName("bet_amount");
            entity.Property(e => e.HorseId).HasColumnName("horse_id");
            entity.Property(e => e.RaceId).HasColumnName("race_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Horse).WithMany(p => p.UserBets)
                .HasForeignKey(d => d.HorseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_bets_ibfk_2");

            entity.HasOne(d => d.Race).WithMany(p => p.UserBets)
                .HasForeignKey(d => d.RaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_bets_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.UserBets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_bets_ibfk_3");
        });

        modelBuilder.Entity<UserCard>(entity =>
        {
            entity.HasKey(e => e.CreditcardNum).HasName("PRIMARY");

            entity.ToTable("user_cards");

            entity.HasIndex(e => e.UserId, "IX_user_cards_user_id");

            entity.Property(e => e.CreditcardNum)
                .HasMaxLength(17)
                .HasColumnName("creditcard_num");
            entity.Property(e => e.CardName)
                .HasMaxLength(60)
                .HasColumnName("card_name");
            entity.Property(e => e.Cvc)
                .HasMaxLength(4)
                .HasColumnName("cvc");
            entity.Property(e => e.ExpMonth)
                .HasMaxLength(3)
                .HasColumnName("exp_month");
            entity.Property(e => e.ExpYear)
                .HasMaxLength(3)
                .HasColumnName("exp_year");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserCards)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_cards_ibfk_1");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user_details");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasMaxLength(6)
                .HasColumnName("birth_date");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");

            entity.HasOne(d => d.User).WithOne(p => p.UserDetail)
                .HasForeignKey<UserDetail>(d => d.UserId)
                .HasConstraintName("user_details_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
