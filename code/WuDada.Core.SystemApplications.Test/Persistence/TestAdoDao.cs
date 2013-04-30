using System;
using System.Data;
using WuDada.Core.Persistence.Ado;
using NUnit.Framework;

namespace WuDada.Core.SystemApplications.Test.Persistence
{
    [TestFixture]
    public class TestAdoDao
    {
        public TestHelper TestHelper { get; set; }
        AdoDao AdoDao = null;

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            TestHelper = new TestHelper();
            AdoDao = TestHelper.GetAdoDao();
        }

        [Test]
        public void Test_001_ExecuteScalarInt()
        {
            string cmdText = "SELECT COUNT(*) FROM SYSTEM_SYSTEM_PARAM ";

            int result = AdoDao.ExecuteScalarInt(CommandType.Text, cmdText);

            Console.Out.WriteLine("result = " + result);
        }

        [Test]
        public void Test_002_GetDataTable()
        {
            string cmdText = "SELECT * FROM SYSTEM_SYSTEM_PARAM ";

            DataTable dataTable = AdoDao.GetDataTable(CommandType.Text, cmdText);

            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Console.Out.WriteLine("Id = " + dataRow["Id"]);
                }
            }
        }

        [Test]
        public void Test_003_GetDataTableByPage()
        {
            string cmdText = "SELECT * FROM SYSTEM_SYSTEM_PARAM ";
            string orderBy = "SystemParamId DESC";

            DataTable dataTable = AdoDao.GetDataTable(CommandType.Text, cmdText, orderBy, 0, 10);

            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Console.Out.WriteLine("Id = " + dataRow["Id"]);
                }
            }            
        }
    }
}
