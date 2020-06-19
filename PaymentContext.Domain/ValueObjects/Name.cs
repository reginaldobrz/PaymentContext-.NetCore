using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string FirstName, string LastName)
        {
            FirstName = firstName;
            LastName = lastName;

            if(string.IsNullOrEmpty(firstName))
                AddNotification("Name.FirstName","Nome Inválido");
        }

        public string firstName { get; private set; }
        public string lastName { get; private set; }
    }
}