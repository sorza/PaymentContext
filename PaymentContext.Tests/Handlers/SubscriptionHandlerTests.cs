using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Moks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "12345678910";
            command.Email = "teste2@teste.com";
            command.BarCode = "123456789";
            command.BoletoNumber = "1316198494";
            command.PaymentNumber = "984564651";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Wayne Corp";
            command.PayerDocument = "1234567891011"; 
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";  
            command.Street = "Rua Um";
            command.Number = "123";
            command.Neighborhood = "Flores";
            command.City = "Gotham";
            command.State = "SP";
            command.Country = "Brasil";
            command.ZipCode = "12345000";

            handler.Handle(command);
            Assert.AreEqual(false, handler.IsValid);
        }        
    }
}