﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rick_and_Morty.Infrastructure.Persistence.Context;

namespace Rick_and_Morty.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(RickAndMortyContext))]
    partial class RickAndMortyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Rick_and_Morty.Domain.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("ImageName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PlaceOfBirthId")
                        .HasColumnType("uuid");

                    b.Property<string>("Race")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UpdatedUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("PlaceOfBirthId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Rick_and_Morty.Domain.Episode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ImageName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Plot")
                        .HasColumnType("text");

                    b.Property<DateTime>("Premiere")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Season")
                        .HasColumnType("integer");

                    b.Property<int>("Series")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UpdatedUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("Rick_and_Morty.Domain.EpisodeCharacters", b =>
                {
                    b.Property<Guid?>("EpisodeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CharacterId")
                        .HasColumnType("uuid");

                    b.HasKey("EpisodeId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("EpisodeCharacters");
                });

            modelBuilder.Entity("Rick_and_Morty.Domain.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ImageName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Measurements")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UpdatedUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Rick_and_Morty.Domain.Character", b =>
                {
                    b.HasOne("Rick_and_Morty.Domain.Location", "Location")
                        .WithMany("Characters")
                        .HasForeignKey("LocationId");

                    b.HasOne("Rick_and_Morty.Domain.Location", "PlaceOfBirth")
                        .WithMany("PlaceOfBirthCharacters")
                        .HasForeignKey("PlaceOfBirthId");
                });

            modelBuilder.Entity("Rick_and_Morty.Domain.EpisodeCharacters", b =>
                {
                    b.HasOne("Rick_and_Morty.Domain.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rick_and_Morty.Domain.Episode", "Episode")
                        .WithMany()
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
