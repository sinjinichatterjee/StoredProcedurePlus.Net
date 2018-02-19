using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures;
using StoredProcedurePlus.Net.UnitTestEntities;

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

        private int Sp_OnMockExecution(IDataEntityAdapter input)
        {
            long id = input.GetLong(0);
            if (id > 0)
            {
                input.SetBool(1, true);
                //input.SetInt(2, 19);
            }

            return 1;
        }
    }
}
