using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using NUnit.Framework;

namespace ConsoleApplication1.Tests
{
    public class BoundedContextsTests
    {
        [Test]
        public void CanInsertCustomer()
        {
            using (var context = new PrimaryContext())
            {
                var customer = new Customer
                {
                    Name = "Customer 1",
                    Code = "C1"
                };
                context.Customers.Add(customer);
                context.SaveChanges();
                Assert.Pass();
            }
        }

        [Test]
        public void CanRetrieveCustomerReference()
        {
            using (var context = new SecondaryContext())
            {
                var customerReference = context.Customers.SingleOrDefault(p => p.Name == "Customer 1");
                Assert.IsNotNull(customerReference);
            }
        }

        [Test]
        public void GenerateEdmxFromHistoryMigrationTable()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConsoleApplication1"].ConnectionString))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Model FROM dbo.__MigrationHistory ORDER BY MigrationId DESC", cn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                byte[] b = rd[0] as byte[];

                using (MemoryStream ms = new MemoryStream(b))
                {
                    using (GZipStream stream2 = new GZipStream(ms, CompressionMode.Decompress))
                    {
                        var edmx = XDocument.Load(stream2);
                        var path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "ConsoleApplication1.edmx");
                        System.Diagnostics.Debug.WriteLine(path);
                        System.IO.File.WriteAllText(path, edmx.ToString());
                    }

                }
            }
            Assert.Pass();
        }
    }
}
