using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using CacheDB;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CacheDbTest
{
    public class Testtype
    {
        [Key] public int Id { get; set; }
 
        public string A { get; set; }

        public int D { get; set; }


    public Testtype(string a, int d)
        {
            this.A = a;
            this.D = d;
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CacheDB.ChacheDB<Testtype> d = new();
            d.BuildDb();
            d.Items.Add(new Testtype("test1", 1));
            d.Items.Add(new Testtype("test2", 2));
            d.SaveChanges();
            d.MemoryDbClose();
        }

        [TestMethod]
        public void TestMethod2()
        {
            CacheDB.ChacheDB<Testtype> d2 = new(DbType.File, "testdb.db");
            d2.BuildDb();
            d2.Items.Add(new Testtype("test1", 1));
            d2.Items.Add(new Testtype("test2", 2));
            d2.SaveChanges();
            Assert.AreEqual(2, d2.Items.ToList().Count);
        }
        [TestMethod]
        public void TestMethod3a()
        {
            CacheDB.ChacheDB<Testtype> d3 = new(DbType.File, "test2db.db", false);
            d3.BuildDb();
            d3.Items.Add(new Testtype("test1", 1));
            d3.Items.Add(new Testtype("test2", 2));
            d3.SaveChanges();
            Assert.IsTrue(2 == d3.Items.ToList().Count);
        }

        [TestMethod]
        public void TestMethod3b()
        {
            CacheDB.ChacheDB<Testtype> d3b = new(DbType.File, "test2db.db", false);
            d3b.BuildDb();
            d3b.Items.Add(new Testtype("test1", 1));
            d3b.Items.Add(new Testtype("test2", 2));
            d3b.SaveChanges();
            Assert.IsTrue(2 < d3b.Items.ToList().Count, "Expected to fail first run!!!");
            if (2 < d3b.Items.ToList().Count)
            {
                d3b.Distroy();
            }
        }
    }
}
