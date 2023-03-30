﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using Senparc.Xncf.DatabaseToolkit.Models.MultipleDatabase;

#nullable disable

namespace Senparc.Xncf.DatabaseToolkit.Domain.Migrations.Oracle
{
    [DbContext(typeof(DatabaseToolkitSenparcEntities_Oracle))]
    partial class DatabaseToolkitSenparcEntities_OracleModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1, 1);

            modelBuilder.Entity("Senparc.Xncf.DatabaseToolkit.DbConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1, 1);

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR2(300)");

                    b.Property<int>("BackupCycleMinutes")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("BackupPath")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR2(300)");

                    b.Property<bool>("Flag")
                        .HasColumnType("NUMBER(1)");

                    b.Property<DateTime>("LastBackupTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR2(300)");

                    b.Property<int>("TenantId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.ToTable("DatabaseToolkitDbConfig");
                });
#pragma warning restore 612, 618
        }
    }
}