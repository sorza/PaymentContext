using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void TestMethod()
        {
            var student = new Student("Alexandre", "Zordan", "123456789", "ale@zordan.com.br");
            
        }
    }
}