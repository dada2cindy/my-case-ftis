using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace WuDada.Core.SystemApplications.Test.NHibernate
{
    [TestFixture]
    public class TestSchema
    {
        public TestHelper TestHelper { get; set; }
        public readonly string[] AddAssemblies = new string[] { "WuDada.Core", "WuDada.Core.SystemApplications", "WuDada.Core.Common" }; 

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            this.TestHelper = new TestHelper();
        }

        [Test]
        public void Test_001_DropSchema()
        {
            Configuration cfg = this.TestHelper.GetCfg(AddAssemblies);
            SchemaExport export = new SchemaExport(cfg);
            export.Drop(false, true);
        }

        [Test]
        public void Test_002_CreateSchema()
        {
            Configuration cfg = this.TestHelper.GetCfg(AddAssemblies);
            SchemaExport export = new SchemaExport(cfg);
            export.Create(false, true);
        }
    }
}
