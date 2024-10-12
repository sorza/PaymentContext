namespace PaymentContext.Domain.Entities
{
    public class Student
    {
        private IList<Subscription> _subscriptions;
        public Student(string firstName, string lastName, string document, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
        }

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Document { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;
        public IReadOnlyCollection<Subscription> Subscriptions{ get { return _subscriptions.ToArray(); } } 

        public void AddSubscription (Subscription subscription)
        {
            foreach (var sub in Subscriptions)
                sub.Inactivate();
            
            _subscriptions.Add(subscription);
        }
        
    }
}