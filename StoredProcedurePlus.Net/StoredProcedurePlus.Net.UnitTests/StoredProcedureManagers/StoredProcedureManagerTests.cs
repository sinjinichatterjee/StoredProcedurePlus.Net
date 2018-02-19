using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.UnitTestEntities;
using StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StoredProcedurePlus.Net.StoredProcedureManagers.UnitTests
{
    [TestClass()]
    public class StoredProcedureManagerTests
    {
        static string Hash = Guid.NewGuid().ToString();

        [TestMethod()]
        public void MockTest()
        {
            MyFirstStoredProcedure p1 = new MyFirstStoredProcedure();
            MyFirstStoredProcedure p2 = new MyFirstStoredProcedure();

            School Entity = new School() { SchoolName = "Bhadrakali", SchoolId=30 };
            School Entity1 = new School() { SchoolName = "l" };

            p1.Execute(Entity);
            p1.Execute(Entity1);
        }

        [TestMethod()]
        public void BasicTest()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName="SQL++.NET LIB PERFORMANCE TEST",
                Country="INDIA",
                State="GUJRAT",
                City="BARODA",
                District="UNSPECIFIED",
                Street="1 SHANTI NAGAR STREET",
                HouseNo="C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry="USA",
                EmployerCity= "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",                
                EmployerPin="19015",
                EmployerStreet="1800 GREEN STREET",
                EmployerHouseNo="F2",
                CTC = 1000.56M,
                NET=10000.65M,
                Gross = 10000.11M,   
                MobileNo2= Hash
            };

            SpResourceSummary Sp = new SpResourceSummary();
            
            Sp.Execute(Input);
            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void MultipleInstanceTest()
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
                CTC = 100.56M,
                NET = 10000.65M,
                Gross = 10000.11M,
                MobileNo2 = Hash
            };

            SpResourceSummary[] Multiple = new SpResourceSummary[10];
            for (int i = 0; i < Multiple.Length; i++)
            {
                Multiple[i] = new SpResourceSummary();
            }

            StringBuilder Log = new StringBuilder();
            for (int i = 0; i < Multiple.Length; i++)
            {
                Input.CTC = Input.CTC + 1;
                Multiple[i].Execute(Input);
                Log.AppendLine(string.Format("Person ID : {0}", Input.PersonId));
            }

            Console.Write(Log);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestCloseEach()
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

            using (ConnectionScope scope = new ConnectionScope(ConnectionScopeTypes.CloseAfterEachExecution))
            {
                SpResourceSummary Sp = new SpResourceSummary();

                for (int i = 0; i < 100; i++)
                {
                    Sp.Execute(Input, scope);
                }
            }

            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestKeepOpen()
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

            using (ConnectionScope scope = new ConnectionScope(ConnectionScopeTypes.KeepOpen))
            {
                SpResourceSummary Sp = new SpResourceSummary();

                for (int i = 0; i < 100; i++)
                {
                    Sp.Execute(Input, scope);
                }
            }

            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestDisposeEach()
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

            using (ConnectionScope scope = new ConnectionScope(ConnectionScopeTypes.DisposeAfterEachExecution))
            {
                SpResourceSummary Sp = new SpResourceSummary();

                for (int i = 0; i < 100; i++)
                {
                    Sp.Execute(Input, scope);
                }
            }

            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestDefault()
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

            using (ConnectionScope scope = new ConnectionScope())
            {
                SpResourceSummary Sp = new SpResourceSummary();

                for (int i = 0; i < 100; i++)
                {
                    Sp.Execute(Input, scope);
                }
            }

            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestRetrival()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",                
            };

            IEnumerable<ResourceSummary> Result=null;

            using (ConnectionScope scope = new ConnectionScope())
            {
                SpResourceSummaryDetails Sp = new SpResourceSummaryDetails();

                Stopwatch sw = new Stopwatch();
                sw.Start();

                Sp.Execute(Input, scope);

                Result =
                   Sp.GetResult<ResourceSummary>();

                sw.Stop();

                Console.Write( string.Format("Record Retrived : {0} in time : {1}", Result!=null?Result.Count():0,  sw.Elapsed.TotalMilliseconds));

            }

            //Assert.IsTrue(Result.Count > 0);
        }
    }
}