using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {         
            var doc = new Document("123",EDocumentType.CNPJ);
            Assert.IsTrue(!doc.IsValid);
        }

         [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {         
            var doc = new Document("31521273000105",EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }        

         [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {         
            var doc = new Document("123",EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }

         [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {         
            var doc = new Document("45057894897",EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}