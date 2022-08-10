using PaymentContext.Domain;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.ValueObjects;

namespace PaymentContext.Tests.Entties;

[TestClass]
public class StudentTests
{
    private readonly Name _name;

    private readonly Document _document;

    private readonly Email _email;

    private readonly Address _address;
    
    private readonly Student _student;

    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Bruce", "Waine");
        _document = new Document("11111111111", EDocumentType.Cpf);
        _email = new Email("bruce@email.com");
        _address = new Address("Rua 1", "123", "Bairro Legal", 
            "Gothan", "SP", "Br", "13440123");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
        
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("111111111", DateTime.Now, DateTime.Now.AddDays(5),
            10, 10, "Wayne Coorp", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);
        
        Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);
        
        Assert.IsTrue(_student.Invalid);
        
    }
    
    [TestMethod]
    public void ShouldReturnSuccessWhenAddSubscription()
    {
        var payment = new PayPalPayment("111111111", DateTime.Now, DateTime.Now.AddDays(5),
            10, 10, "Wayne Coorp", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        
        Assert.IsTrue(_student.Valid);
    }
}