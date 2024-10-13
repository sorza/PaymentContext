using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.Entities.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract<Address>().Requires()
                .IsLowerOrEqualsThan(Street, 3, "Adress.Street", "A rua deve conter pelo menos 3 caracteres")
                .IsGreaterOrEqualsThan(Street, 80, "Adress.Street", "A rua deve conter no máximo 80 caracteres")
                
                .IsLowerOrEqualsThan(Number, 1, "Adress.Number", "O número deve conter pelo menos 1 caracteres")
                .IsGreaterOrEqualsThan(Number, 7, "Adress.Number", "O número deve conter no máximo 7 caracteres")

                .IsLowerOrEqualsThan(Neighborhood, 3, "Adress.Neighborhood", "O bairro deve conter pelo menos 3 caracteres")
                .IsGreaterOrEqualsThan(Neighborhood, 80, "Adress.Neighborhood", "O bairro deve no conter máximo 80 caracteres")

                .IsLowerOrEqualsThan(City, 3, "Adress.City", "A cidade deve conter pelo menos 3 caracteres")
                .IsGreaterOrEqualsThan(City, 80, "Adress.City", "A cidade deve conter no máximo 80 caracteres")

                .IsLowerOrEqualsThan(State, 2, "Adress.State", "O estado deve conter pelo menos 2 caracteres")
                .IsGreaterOrEqualsThan(State, 50, "Adress.State", "O estado deve conter no máximo 50 caracteres")

                .IsLowerOrEqualsThan(Country, 2, "Adress.Country", "O país deve conter pelo menos 2 caracteres")
                .IsGreaterOrEqualsThan(Country, 50, "Adress.Country", "O país deve conter no máximo 50 caracteres")

                .AreNotEquals(ZipCode, 8, "Adress.Country", "O país deve conter 8 caracteres")                 
            );
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}