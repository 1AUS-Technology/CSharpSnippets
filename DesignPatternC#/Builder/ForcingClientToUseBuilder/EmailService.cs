namespace DesignPatternC_.Builder.ForcingClientToUseBuilder;

public class MailService
{
    private void SendEmailInternal(Email email)
    {
    }

    // The action has a builder as the parameter 
    public void SendEmail(Action<EmailBuilder> builder)
    {
        var email = new Email();
        
        builder(new EmailBuilder(email));


        SendEmailInternal(email);
    }
}

public class EmailBuilder
{
    private readonly Email email;
    public EmailBuilder(Email email) => this.email = email;

    public EmailBuilder From(string from)
    {
        email.From = from;
        return this;
    }

    public EmailBuilder To(string to)
    {
        email.To = to;
        return this;
    }
    // other fluent members here
}

public class Email
{
    public string From, To, Subject, Body;
    // other members here
}