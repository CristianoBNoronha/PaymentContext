using PaymentContext.Domain;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks;

public class FakeStudentRepository : IStudentRepository
{
    public bool DocumentExists(string document)
    {
        if (document == "99999999999")
            return true;
        return false;
    }

    public bool EmailExists(string email)
    {
        if (email == "hello@balta.io")
            return true;
        return false;
    }

    public void CreateSubscription(Student student) { }
}