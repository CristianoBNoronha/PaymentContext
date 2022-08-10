using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenCnpjIsInvalid()
    {
        var doc = new Document("123", EDocumentType.Cnpj);
        Assert.IsTrue(doc.Invalid);
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenCnpjIsValid()
    {
        var doc = new Document("12311111111111", EDocumentType.Cnpj);
        Assert.IsTrue(doc.Valid);
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenCpfIsInvalid()
    {
        var doc = new Document("123", EDocumentType.Cpf);
        Assert.IsTrue(doc.Invalid);
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenCpfIsValid()
    {
        var doc = new Document("12311111111", EDocumentType.Cpf);
        Assert.IsTrue(doc.Valid);
    }
}