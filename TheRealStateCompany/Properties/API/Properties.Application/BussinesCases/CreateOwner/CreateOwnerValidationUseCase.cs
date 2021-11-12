using Properties.Application.Services;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.CreateOwner
{
    public sealed class CreateOwnerValidationUseCase : ICreateOwnerUseCase
    {
        private readonly ICreateOwnerUseCase _useCase;
        private readonly Notification _notification;
        private IOutputPort _outputPort;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateOwnerValidationUseCase" /> class.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="notification"></param>
        public CreateOwnerValidationUseCase(ICreateOwnerUseCase useCase, Notification notification)
        {
            this._useCase = useCase;
            this._notification = notification;
            this._outputPort = new CreateOwnerPresenter();
        }

        /// <inheritdoc />
        public void SetOutputPort(IOutputPort outputPort)
        {
            this._outputPort = outputPort;
            this._useCase.SetOutputPort(outputPort);
        }

        /// <inheritdoc />
        public async Task Execute(decimal identificationNumber, string name, string address, byte[]? photo, DateTime? birthday)
        {
            if (identificationNumber <= 0)
            {
                this._notification
                    .Add(nameof(identificationNumber), "Owner identification needs to be greater than zero.");
            }

            if (string.IsNullOrEmpty(name))
            {
                this._notification
                    .Add(nameof(name), "Name is required.");
            }

            if (string.IsNullOrEmpty(address))
            {
                this._notification
                    .Add(nameof(address), "Address is required.");
            }

            if (this._notification
                .IsInvalid)
            {
                this._outputPort
                    .Invalid();
                return;
            }

            await this._useCase
                .Execute(identificationNumber, name, address, photo, birthday)
                .ConfigureAwait(false);
        }
    }
}
