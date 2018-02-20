using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures;
using StoredProcedurePlus.Net.UnitTestEntities;
using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTests.MockTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MockTestSpp()
        {
            MockSp sp = new MockSp();
            sp.OnMockExecution += Sp_OnMockExecution;
            AllTypeParams p = new AllTypeParams() { Id = 10, IsEnabled = false };
            sp.Execute(p);

            Assert.IsTrue(p.IsEnabled);
        }

        private void Sp_OnMockExecution(object sender, MockEventArgs args)
        {
            long id = args.Input.GetLong(args.Input.GetOrdinal("Id"));

            if (id > 0)
            {
                args.Input.SetBool(args.Input.GetOrdinal("IsEnabled"), true);
                args.Input.SetInt(args.Input.GetOrdinal("RowChanged"), 19);
            }

            args.Result = 1;
        }
    }
}
