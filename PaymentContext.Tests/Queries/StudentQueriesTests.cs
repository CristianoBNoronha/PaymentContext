using PaymentContext.Domain;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries;

[TestClass]
public class StudentQueriesTests
{
    private IList<Student> _students;

    public StudentQueriesTests()
    {
        for (var i = 0; i <= 10; i++)
            _students.Add(new Student(new Name("Aluno", i.ToString()), 
                new Document("1111111111" + i, EDocumentType.Cpf),
                new Email(i + "@balta.io")));
    }

    [TestMethod]
    public void ShouldReturnNullWhenDocumentNotExists()
    {
        var expression = StudentQueries.GetStudentInfo("99999999999");
        var student = _students.AsQueryable().Where(expression).FirstOrDefault();
        
        Assert.AreEqual(null, student);
    }

    [TestMethod]
    public void NotShouldReturnStudentWhenDocumentExists()
    {
        var expression = StudentQueries.GetStudentInfo("11111111111");
        var student = _students.AsQueryable().Where(expression).FirstOrDefault();
        
        Assert.AreEqual(null, student);
    }

}