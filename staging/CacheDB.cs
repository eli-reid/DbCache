﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace CacheDB
{
    public enum DbType
    {
        Memory,
        File
    }

    public class ChacheDB<Item> : DbContext where Item : class
    { 
        private DbType dbType;
        private SqliteConnection dbConn;
        private readonly string fileName;
        private readonly bool singleUseDb;
        public DbSet<Item> Items { get; set; }
        public bool isTemp { get; set; }
        public ChacheDB(DbType dbType=DbType.Memory, string fileName=null, bool singleUseDb=true)
        {
            this.dbType = dbType;
            this.fileName = fileName;
            this.singleUseDb = singleUseDb;
            this.isTemp = false;
        }
        public void buildDb()
        {
            if (this.dbType == DbType.File && this.fileName is null)
            {
                throw new ArgumentException("Parameter cannot be null", nameof(this.fileName));
            }

            if (this.singleUseDb)
                Database.EnsureDeleted();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (this.dbType)
            {
                case DbType.Memory:
                    this.dbConn = new SqliteConnection("Data Source=:memory:");
                    this.dbConn.Open();
                    optionsBuilder.UseSqlite(this.dbConn);
                    break;

                case DbType.File:
                    string conn = "Data Source=" + this.fileName;
                    optionsBuilder.UseSqlite(conn);
                    break;

                default:
                    throw new ArgumentException("Parameter cannot be null", nameof(this.dbType));
            }
        }

        public void MemoryDbClose()
        {
            this.dbConn.Close();
        }

        public void Distroy()
        {
            Database.EnsureDeleted();
        }
    }
}
