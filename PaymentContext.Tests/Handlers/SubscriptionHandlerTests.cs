using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

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
        command.Document = "99999999999";
        command.Email = "hello@balta.io2";
        command.BarCode = "12345612";
        command.BoletoNumber = "12345612";
        command.PaymentNumber = "12345612";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddMonths(1);
        command.Total = 60;
        command.TotalPaid = 60;
        command.Payer = "Wayne Coorp";
        command.PayerDocument = "12345678910";
        command.PayerDocumentType = EDocumentType.Cpf;
        command.PayerEmail = "batman@dc.com";
        command.Street = "Rua";
        command.Number = "1";
        command.Neighborhood = "Aqui";
        command.City = "Cidade";
        command.State = "Estado";
        command.Country = "País";
        command.ZipCode = "05788000";

        handler.Handle(command);
        Assert.AreEqual(false, handler.Valid);
    }
}