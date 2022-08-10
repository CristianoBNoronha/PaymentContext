using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable, 
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
{
    private readonly IStudentRepository _repository;

    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
    {
        _repository = studentRepository;
        _emailService = emailService;

    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar a sua assinatura.");
        }
        
        // Verificar se o documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Esse Cpf já está em uso.");
        
        // Verificar se o email já está cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este e-mail já está em uso.");
        
        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, command.PayerDocumentType);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, 
            command.City, command.State, command.Country,command.ZipCode);
        
        // Gerar entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate,
            command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), 
            address, email);

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        // Checar as notificações
        if (Invalid)
            return new CommandResult(false, "Não foi possível realizar a sua assinatura.");

        // Salvar as informações
        _repository.CreateSubscription(student);
        
        // Enviar e-mail de boas vindas
        _emailService.Send(student.Name.ToString(), email.Address, "Bem vindo ao balta.io.",
            "Sua assinatura foi criada");
        
        // retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso.");
    }

    public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar a sua assinatura.");
        }
        
        // Verificar se o documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Esse Cpf já está em uso.");
        
        // Verificar se o email já está cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este e-mail já está em uso.");
        
        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, command.PayerDocumentType);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, 
            command.City, command.State, command.Country,command.ZipCode);
        
        // Gerar entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new PayPalPayment(command.TransactionCode, command.PaidDate, command.ExpireDate, command.Total, 
            command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), 
            address, email);

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        // Checar as notificações
        if (Invalid)
            return new CommandResult(false, "Não foi possível realizar a sua assinatura.");

        // Salvar as informações
        _repository.CreateSubscription(student);
        
        // Enviar e-mail de boas vindas
        _emailService.Send(student.Name.ToString(), email.Address, "Bem vindo ao balta.io.",
            "Sua assinatura foi criada");
        
        // retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso.");
    }

    public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar a sua assinatura.");
        }
        
        // Verificar se o documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Esse Cpf já está em uso.");
        
        // Verificar se o email já está cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este e-mail já está em uso.");
        
        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, command.PayerDocumentType);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, 
            command.City, command.State, command.Country,command.ZipCode);
        
        // Gerar entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new CreditCardPayment(command.PaidDate, command.ExpireDate, command.Total, 
            command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), 
            address, email, command.CardHolderName, command.CardNumber, command.LastTransactionNumber);

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        // Checar as notificações
        if (Invalid)
            return new CommandResult(false, "Não foi possível realizar a sua assinatura.");

        // Salvar as informações
        _repository.CreateSubscription(student);
        
        // Enviar e-mail de boas vindas
        _emailService.Send(student.Name.ToString(), email.Address, "Bem vindo ao balta.io.",
            "Sua assinatura foi criada");
        
        // retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso.");
    }
}