using Flunt.Validations;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; set; }        
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address? Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions{ get { return _subscriptions.ToArray(); } } 

        public void AddSubscription (Subscription subscription)
        {
            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
                if(sub.Active)
                    hasSubscriptionActive = true;

            if(hasSubscriptionActive) AddNotification("Student.Subscriptions","Você já tem uma assinatura ativa");
            if(subscription.Payments?.Count == 0) AddNotification("Student.Subscription.Payments","Esta assinatura não possuem pagamentos");
           
            if(IsValid)
                _subscriptions.Add(subscription);
        }        
    }
}