using PaymentContext.Shared.ValueObjects;
using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string FirstName, string LastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName,3,"Name.FirstName","Nome deve conter pelo menos 3 caracteres")
                .HasMinLen(LastName,3,"Name.LastName","Nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName,40,"Name.FirstName","Nome deve até 40 caracteres")
            );
        }

        public string firstName { get; private set; }
        public string lastName { get; private set; }
    }
}