using Flunt.Validations;
using PaymentContext.Shared;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        
        AddNotifications(new Contract()
            .Requires()
            .HasMinLen(FirstName, 3, "Name.FirstName", "O primeiro nome devew conter pelo menos 3 caracteres.")
            .HasMinLen(LastName, 3, "Name.LastName", "O sobrenome deve conter pelo menos 3 caracteres.")
            .HasMaxLen(FirstName, 40, "Name.FirstName", "O primeiro nome deve conter no máximo 40 caracteres.")
            .HasMaxLen(LastName, 40, "Name.LastName", "O sobrenome deve conter no máximo 40 caracteres."));
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public override string ToString() => $"{FirstName} {LastName}";
}