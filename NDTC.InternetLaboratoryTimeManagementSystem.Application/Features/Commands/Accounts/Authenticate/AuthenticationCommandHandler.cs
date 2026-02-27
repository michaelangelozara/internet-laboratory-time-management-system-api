using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.DTOs.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.Authenticate
{
    internal class AuthenticationCommandHandler(
        IUnitOfWork unitOfWork,
        IAccountRepository accountRepository,
        IRoleManager roleManager,
        ITokenProvider tokenProvider,
        ISessionHubService sessionHubService) 
        : IRequestHandler<AuthenticationCommand, Result<AuthenticationResponseDTO>>
    {
        public async Task<Result<AuthenticationResponseDTO>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindByRFIDWithUserAsync(request.RFID);
            if (account is null)
                return Result.Failure<AuthenticationResponseDTO>(Error.NotFound("Account.Invalid", "Invalid rfid."));

            if(account.IsLoggedIn)
                return Result.Failure<AuthenticationResponseDTO>(Error.Problem("Account.Invalid", "Already logged in on another computer."));

            var user = account.User!;

            var roles = roleManager.GetRoles(user);

            var token = tokenProvider.Create(user, roles);
            
            if (roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
                return Result.Success(new AuthenticationResponseDTO(token, null));

            if (account.AvailableDuration <= TimeSpan.Zero)
                return Result.Failure<AuthenticationResponseDTO>(Error.NotFound("Account.Invalid", "You already consumed your time."));

            account.MarkAsLoggedIn();
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // publish new openned sesison
            await sessionHubService.PublishNewSessionOf(user.Id, user.SchoolId, account.AvailableDuration);
            
            return Result.Success(new AuthenticationResponseDTO(token, account.AvailableDuration));
        }
    }
}
