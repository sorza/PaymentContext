using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, 
                IHandler<CreateBoletoSubscriptionCommand>,
                IHandler<CreatePayPalSubscriptionCommand>,
                IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if(!command.IsValid) 
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }                
            
            // Verificar se documento já está cadastrado
            if(_repository.DocumentExists(command.Document)) 
                AddNotification("Document", "Este CPF já está em uso");
            
            // Verificar se email já está cadastrado
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName,command.LastName);  
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);      

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email,
                command.BarCode, 
                command.BoletoNumber );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validaçoes
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as notificações
            if(!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Salvar Infos
            _repository.CreateSubscription(student);

            //Enviar Email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo!", "Sua Assinatura foi criada");

            //Retornar Informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Verificar se documento já está cadastrado
            if(_repository.DocumentExists(command.Document)) 
                AddNotification("Document", "Este CPF já está em uso");
            
            // Verificar s eemail já está cadastrado
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName,command.LastName);  
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);      

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validaçoes
            AddNotifications(name, document, email, address, student, subscription, payment);

             //Checar as notificações
            if(!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Salvar Infos
            _repository.CreateSubscription(student);

            //Enviar Email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo!", "Sua Assinatura foi criada");

            //Retornar Informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
           // Verificar se documento já está cadastrado
            if(_repository.DocumentExists(command.Document)) 
                AddNotification("Document", "Este CPF já está em uso");
            
            // Verificar s eemail já está cadastrado
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName,command.LastName);  
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);      

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email,
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNumber);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validaçoes
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as notificações
            if(!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Salvar Infos
            _repository.CreateSubscription(student);

            //Enviar Email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo!", "Sua Assinatura foi criada");

            //Retornar Informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}