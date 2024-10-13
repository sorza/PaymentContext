using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.Entities.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            var contract = new Contract<Name>().Requires();

            if (FirstName.Length < 3 || FirstName.Length > 40)            
                contract.AddNotification("Name.FirstName", "Nome deve conter entre 3 e 40 caracteres");
            

            if (LastName.Length < 3 || LastName.Length > 40)            
                contract.AddNotification("Name.LastName", "Sobrenome deve conter entre 3 e 40 caracteres");
            
            AddNotifications(contract);
    
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}