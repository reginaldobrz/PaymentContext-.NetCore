using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommands>

    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;


        public SubscriptionHandler(IStudentRepository repository,IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //fail fast validations
            command.Validate();
            if(command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false,"Nao foi possivel finalziar seu cadastro");
            }

            //verifica se o doc ja esta cadastrado
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document","Este CPF ja esta em uso");

            //verifica se o email ja esta cadastrado
            if(_repository.DocumentExists(command.Document))
                AddNotification("Email","Este Email ja esta em uso");

            //gerar vos
            var name = new Name(command.FirstName,command.LastName);
            var document = new Document(command.Document,EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number,command.Neighborhood,
                command.City,command.State,command.Country,command.ZipCode);

            // gerar entidades
            var student = new Student(name,document,email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument,command.PayerDocumentType),
                address,
                email
            );

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //agrupar as validacoes
            AddNotifications(name,document,email,address,student,subscription,payment);

            //salvar as informacoes
            _repository.CreateSubscription(student);

            //enviar email de boas vindas
            _emailService.Send(student.Name.ToString(),student.Email.Address,"Bem vindo a sua pagina","Sua assinatura foi criada");

            //retornar informacoes 
            return new CommandResult(true,"Assinatura realziada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommands command)
        {
            //verifica se o doc ja esta cadastrado
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document","Este CPF ja esta em uso");

            //verifica se o email ja esta cadastrado
            if(_repository.DocumentExists(command.Document))
                AddNotification("Email","Este Email ja esta em uso");

            //gerar vos
            var name = new Name(command.firstName,command.lastName);
            var document = new Document(command.Document,EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number,command.Neighborhood,
                command.City,command.State,command.Country,command.ZipCode);

            // gerar entidades
            var student = new Student(name,document,email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument,command.PayerDocumentType),
                address,
                email
            );

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //agrupar as validacoes
            AddNotifications(name,document,email,address,student,subscription,payment);

            //salvar as informacoes
            _repository.CreateSubscription(student);

            //enviar email de boas vindas
            _emailService.Send(student.Name.ToString(),student.Email.Address,"Bem vindo a sua pagina","Sua assinatura foi criada");

            //retornar informacoes 
            return new CommandResult(true,"Assinatura realziada com sucesso");
        }
    }
}