using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using CacheDB;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CacheDbTest
{
    public class testtype
    {
        [Key] public int id { get; set; }
 
        public string a { get; set; }

        public int d { get; set; }


    public testtype(string a, int d)
        {
            this.a = a;
            this.d = d;
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CacheDB.ChacheDB<testtype> d = new ChacheDB<testtype>();
            d.buildDb();
            d.Items.Add(new testtype("test1", 1));
            d.Items.Add(new testtype("test2", 2));
            d.SaveChanges();
            d.MemoryDbClose();
        }

        [TestMethod]
        public void TestMethod2()
        {
            CacheDB.ChacheDB<testtype> d2 = new ChacheDB<testtype>(DbType.File, "testdb.db");
            d2.buildDb();
            d2.Items.Add(new testtype("test1", 1));
            d2.Items.Add(new testtype("test2", 2));
            d2.SaveChanges();
            Assert.AreEqual(2, d2.Items.ToList().Count());
        }
        [TestMethod]
        public void TestMethod3a()
        {
            CacheDB.ChacheDB<testtype> d3 = new ChacheDB<testtype>(DbType.File, "test2db.db", false);
            d3.buildDb();
            d3.Items.Add(new testtype("test1", 1));
            d3.Items.Add(new testtype("test2", 2));
            d3.SaveChanges();
            Assert.IsTrue(2 == d3.Items.ToList().Count());
        }

        [TestMethod]
        public void TestMethod3b()
        {
            CacheDB.ChacheDB<testtype> d3b = new ChacheDB<testtype>(DbType.File, "test2db.db", false);
            d3b.buildDb();
            d3b.Items.Add(new testtype("test1", 1));
            d3b.Items.Add(new testtype("test2", 2));
            d3b.SaveChanges();
            Assert.IsTrue(2 < d3b.Items.ToList().Count(), "Expected to fail first run!!!");
            if (2 < d3b.Items.ToList().Count())
            {
                d3b.Distroy();
            }
        }
    }
}
