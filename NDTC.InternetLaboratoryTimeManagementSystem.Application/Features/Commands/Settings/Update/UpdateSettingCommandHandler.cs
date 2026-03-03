using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Settings;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Settings.Update
{
    internal class UpdateSettingCommandHandler(
        ISettingRepository settingRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateSettingCommand, Result>
    {
        public async Task<Result> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            var setting = await settingRepository.Find();

            setting.SetIsSyncing(request.IsSyncing);

            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}
