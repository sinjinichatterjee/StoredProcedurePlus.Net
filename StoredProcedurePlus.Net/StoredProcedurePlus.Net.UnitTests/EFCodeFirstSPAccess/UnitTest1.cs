using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFDrivenSPAccess.DatabaseContext;
using StoredProcedurePlus.Net.UnitTestEntities;
using System.Diagnostics;
using System.Collections.Generic;
using StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures;
using System.Linq;

namespace StoredProcedurePlus.Net.UnitTests.EFCodeFirstSPAccess
{
    [TestClass]
    public class UnitTest1
    {
        static string Hash = Guid.NewGuid().ToString();

        [TestMethod]
        public void CF_ResourseSummaryRetrivalTest()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",
                Country = "INDIA",
                State = "GUJRAT",
                City = "BARODA",
                District = "UNSPECIFIED",
                Street = "1 SHANTI NAGAR STREET",
                HouseNo = "C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry = "USA",
                EmployerCity = "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",
                EmployerPin = "19015",
                EmployerStreet = "1800 GREEN STREET",
                EmployerHouseNo = "F2",
                CTC = 1000.56M,
                NET = 10000.65M,
                Gross = 10000.11M,
                MobileNo2 = Hash
            };


            PerformanceDbContext_NonVirtual DbContext = new PerformanceDbContext_NonVirtual();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var x = DbContext.ResourceSummaryDetails.CallStoredProc(Input);
            object[] xxx = x[0].ToArray();

            sw.Stop();

            Console.Write(string.Format("Record Retrived : {0} in time : {1}", xxx != null ? xxx.Length : 0, sw.Elapsed.TotalMilliseconds));
        }

        [TestMethod]
        public void SelectTest()
        {
            SPSelectAdjustments sp = new SPSelectAdjustments();
            sp.Execute(null);


            List<Steven_Singer_ADJUSTMENTS> result =sp.GetResult<Steven_Singer_ADJUSTMENTS>().ToList();
            Assert.AreEqual(result.Count, 399);
        }
    }
}
