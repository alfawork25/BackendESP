﻿// <auto-generated />
using System;
using ESP.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ESP.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230822172835_BlockTechnologist")]
    partial class BlockTechnologist
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CheckBlockCheckCode", b =>
                {
                    b.Property<int>("CheckBlocksId")
                        .HasColumnType("int");

                    b.Property<int>("CheckCodesId")
                        .HasColumnType("int");

                    b.HasKey("CheckBlocksId", "CheckCodesId");

                    b.HasIndex("CheckCodesId");

                    b.ToTable("CheckBlocksAndCheckCodes", (string)null);
                });

            modelBuilder.Entity("CheckBlockClientType", b =>
                {
                    b.Property<int>("CheckBlocksId")
                        .HasColumnType("int");

                    b.Property<int>("ClientTypesId")
                        .HasColumnType("int");

                    b.HasKey("CheckBlocksId", "ClientTypesId");

                    b.HasIndex("ClientTypesId");

                    b.ToTable("CheckBlocksAndClientTypes", (string)null);
                });

            modelBuilder.Entity("CheckBlockProcess", b =>
                {
                    b.Property<int>("CheckBlocksId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessesId")
                        .HasColumnType("int");

                    b.HasKey("CheckBlocksId", "ProcessesId");

                    b.HasIndex("ProcessesId");

                    b.ToTable("ProcessesAndCheckBlocks", (string)null);
                });

            modelBuilder.Entity("CheckBlockSubjectType", b =>
                {
                    b.Property<int>("CheckBlocksId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectTypesId")
                        .HasColumnType("int");

                    b.HasKey("CheckBlocksId", "SubjectTypesId");

                    b.HasIndex("SubjectTypesId");

                    b.ToTable("CheckBlocksAndSubjectTypes", (string)null);
                });

            modelBuilder.Entity("CheckCodeProcess", b =>
                {
                    b.Property<int>("CheckCodesId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessesId")
                        .HasColumnType("int");

                    b.HasKey("CheckCodesId", "ProcessesId");

                    b.HasIndex("ProcessesId");

                    b.ToTable("ProcessesAndCheckCodes", (string)null);
                });

            modelBuilder.Entity("CheckCodeRoute", b =>
                {
                    b.Property<int>("CheckCodesId")
                        .HasColumnType("int");

                    b.Property<int>("RoutesId")
                        .HasColumnType("int");

                    b.HasKey("CheckCodesId", "RoutesId");

                    b.HasIndex("RoutesId");

                    b.ToTable("RoutesAndCheckCodes", (string)null);
                });

            modelBuilder.Entity("CheckCodeSubjectType", b =>
                {
                    b.Property<int>("CheckCodesId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectTypesId")
                        .HasColumnType("int");

                    b.HasKey("CheckCodesId", "SubjectTypesId");

                    b.HasIndex("SubjectTypesId");

                    b.ToTable("CheckCodesAndSubjectTypes", (string)null);
                });

            modelBuilder.Entity("ClientTypeSubjectType", b =>
                {
                    b.Property<int>("ClientTypesId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectTypesId")
                        .HasColumnType("int");

                    b.HasKey("ClientTypesId", "SubjectTypesId");

                    b.HasIndex("SubjectTypesId");

                    b.ToTable("SubjectTypesAndClientTypes", (string)null);
                });

            modelBuilder.Entity("ESP.Models.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("ESP.Models.CheckBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BlockId")
                        .HasColumnType("int");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.ToTable("CheckBlocks");
                });

            modelBuilder.Entity("ESP.Models.CheckCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CheckCodes");
                });

            modelBuilder.Entity("ESP.Models.ClientType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClientTypes");
                });

            modelBuilder.Entity("ESP.Models.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BlockTechnologist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Steps")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SystemBlockId")
                        .HasColumnType("int");

                    b.Property<int?>("SystemTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemBlockId");

                    b.HasIndex("SystemTypeId");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("ESP.Models.ProcessSubjectState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsNewClient")
                        .HasColumnType("bit");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProcessSubjectStates");
                });

            modelBuilder.Entity("ESP.Models.ProhibitionCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CheckCodeId")
                        .HasColumnType("int");

                    b.Property<bool>("Default")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CheckCodeId");

                    b.ToTable("ProhibitionCodes");
                });

            modelBuilder.Entity("ESP.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("ESP.Models.StepNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CheckBlockId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CheckBlockId");

                    b.HasIndex("ProcessId");

                    b.ToTable("StepNumbers");
                });

            modelBuilder.Entity("ESP.Models.SubjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubjectTypes");
                });

            modelBuilder.Entity("ESP.Models.SystemBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SystemBlocks");
                });

            modelBuilder.Entity("ESP.Models.SystemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SystemTypes");
                });

            modelBuilder.Entity("ProcessProhibitionCode", b =>
                {
                    b.Property<int>("ProcessesId")
                        .HasColumnType("int");

                    b.Property<int>("ProhibitionCodesId")
                        .HasColumnType("int");

                    b.HasKey("ProcessesId", "ProhibitionCodesId");

                    b.HasIndex("ProhibitionCodesId");

                    b.ToTable("ProcessesAndProhibitionCodes", (string)null);
                });

            modelBuilder.Entity("ProcessSubjectType", b =>
                {
                    b.Property<int>("ProcessesId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectTypesId")
                        .HasColumnType("int");

                    b.HasKey("ProcessesId", "SubjectTypesId");

                    b.HasIndex("SubjectTypesId");

                    b.ToTable("ProcessesAndSubjectTypes", (string)null);
                });

            modelBuilder.Entity("ProhibitionCodeRoute", b =>
                {
                    b.Property<int>("ProhibitionCodesId")
                        .HasColumnType("int");

                    b.Property<int>("RoutesId")
                        .HasColumnType("int");

                    b.HasKey("ProhibitionCodesId", "RoutesId");

                    b.HasIndex("RoutesId");

                    b.ToTable("RoutesAndProhibitionCodes", (string)null);
                });

            modelBuilder.Entity("CheckBlockCheckCode", b =>
                {
                    b.HasOne("ESP.Models.CheckBlock", null)
                        .WithMany()
                        .HasForeignKey("CheckBlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.CheckCode", null)
                        .WithMany()
                        .HasForeignKey("CheckCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckBlockClientType", b =>
                {
                    b.HasOne("ESP.Models.CheckBlock", null)
                        .WithMany()
                        .HasForeignKey("CheckBlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.ClientType", null)
                        .WithMany()
                        .HasForeignKey("ClientTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckBlockProcess", b =>
                {
                    b.HasOne("ESP.Models.CheckBlock", null)
                        .WithMany()
                        .HasForeignKey("CheckBlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.Process", null)
                        .WithMany()
                        .HasForeignKey("ProcessesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckBlockSubjectType", b =>
                {
                    b.HasOne("ESP.Models.CheckBlock", null)
                        .WithMany()
                        .HasForeignKey("CheckBlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.SubjectType", null)
                        .WithMany()
                        .HasForeignKey("SubjectTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckCodeProcess", b =>
                {
                    b.HasOne("ESP.Models.CheckCode", null)
                        .WithMany()
                        .HasForeignKey("CheckCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.Process", null)
                        .WithMany()
                        .HasForeignKey("ProcessesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckCodeRoute", b =>
                {
                    b.HasOne("ESP.Models.CheckCode", null)
                        .WithMany()
                        .HasForeignKey("CheckCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckCodeSubjectType", b =>
                {
                    b.HasOne("ESP.Models.CheckCode", null)
                        .WithMany()
                        .HasForeignKey("CheckCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.SubjectType", null)
                        .WithMany()
                        .HasForeignKey("SubjectTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClientTypeSubjectType", b =>
                {
                    b.HasOne("ESP.Models.ClientType", null)
                        .WithMany()
                        .HasForeignKey("ClientTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.SubjectType", null)
                        .WithMany()
                        .HasForeignKey("SubjectTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESP.Models.CheckBlock", b =>
                {
                    b.HasOne("ESP.Models.Block", "Block")
                        .WithMany("CheckBlocks")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Block");
                });

            modelBuilder.Entity("ESP.Models.Process", b =>
                {
                    b.HasOne("ESP.Models.SystemBlock", "SystemBlock")
                        .WithMany("Processes")
                        .HasForeignKey("SystemBlockId");

                    b.HasOne("ESP.Models.SystemType", "SystemType")
                        .WithMany("Processes")
                        .HasForeignKey("SystemTypeId");

                    b.Navigation("SystemBlock");

                    b.Navigation("SystemType");
                });

            modelBuilder.Entity("ESP.Models.ProhibitionCode", b =>
                {
                    b.HasOne("ESP.Models.CheckCode", "CheckCode")
                        .WithMany("ProhibitionCodes")
                        .HasForeignKey("CheckCodeId");

                    b.Navigation("CheckCode");
                });

            modelBuilder.Entity("ESP.Models.StepNumber", b =>
                {
                    b.HasOne("ESP.Models.CheckBlock", "CheckBlock")
                        .WithMany("StepNumbers")
                        .HasForeignKey("CheckBlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.Process", "Process")
                        .WithMany("StepNumbers")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckBlock");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("ProcessProhibitionCode", b =>
                {
                    b.HasOne("ESP.Models.Process", null)
                        .WithMany()
                        .HasForeignKey("ProcessesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.ProhibitionCode", null)
                        .WithMany()
                        .HasForeignKey("ProhibitionCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProcessSubjectType", b =>
                {
                    b.HasOne("ESP.Models.Process", null)
                        .WithMany()
                        .HasForeignKey("ProcessesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.SubjectType", null)
                        .WithMany()
                        .HasForeignKey("SubjectTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProhibitionCodeRoute", b =>
                {
                    b.HasOne("ESP.Models.ProhibitionCode", null)
                        .WithMany()
                        .HasForeignKey("ProhibitionCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESP.Models.Block", b =>
                {
                    b.Navigation("CheckBlocks");
                });

            modelBuilder.Entity("ESP.Models.CheckBlock", b =>
                {
                    b.Navigation("StepNumbers");
                });

            modelBuilder.Entity("ESP.Models.CheckCode", b =>
                {
                    b.Navigation("ProhibitionCodes");
                });

            modelBuilder.Entity("ESP.Models.Process", b =>
                {
                    b.Navigation("StepNumbers");
                });

            modelBuilder.Entity("ESP.Models.SystemBlock", b =>
                {
                    b.Navigation("Processes");
                });

            modelBuilder.Entity("ESP.Models.SystemType", b =>
                {
                    b.Navigation("Processes");
                });
#pragma warning restore 612, 618
        }
    }
}
