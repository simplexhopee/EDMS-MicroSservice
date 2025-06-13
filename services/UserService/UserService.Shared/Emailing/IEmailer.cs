namespace UserService.Shared.Emailing
{
    public interface IEmailer
    {
        void SendEmail(object model, string template);
    }
}