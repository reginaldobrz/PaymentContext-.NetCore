using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string FirstName, string LastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string firstName { get; private set; }
        public string lastName { get; private set; }
    }
}