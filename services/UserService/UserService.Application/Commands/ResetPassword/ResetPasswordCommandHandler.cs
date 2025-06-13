using MediatR;
using UserService.Shared.Exceptions;
using UserService.Application.Interfaces;
using UserService.Shared.Emailing;


namespace UserService.Application.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IUserServices _userServices;
        private readonly IUserRepository _repository;
        private readonly IEmailer _emailer;
        public ResetPasswordCommandHandler(IUserServices userServices, 
            IUserRepository repository, IEmailer emailer)
        {
            _userServices = userServices;
            _repository = repository;
            _emailer = emailer;

        }
        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmail(request.email);
            if (user == null)
            {
                throw new EntityNotFoundException("The email does not match our records");
            }
            var token = await _userServices.GeneratePasswordResetToken(user);
            var model = new
            {
                token,
                email = user.Email
            };
            _emailer.SendEmail(model, "reset");

        }
    }
}
