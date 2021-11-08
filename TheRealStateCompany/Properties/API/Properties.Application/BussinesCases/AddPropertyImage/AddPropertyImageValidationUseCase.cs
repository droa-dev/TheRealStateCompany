using Properties.Application.Services;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.AddPropertyImage
{
    public class AddPropertyImageValidationUseCase : IAddPropertyImageUseCase
    {
        private readonly IAddPropertyImageUseCase _useCase;
        private readonly Notification _notification;
        private IOutputPort _outputPort;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddPropertyImageValidationUseCase" /> class.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="notification"></param>
        public AddPropertyImageValidationUseCase(IAddPropertyImageUseCase useCase, Notification notification)
        {
            this._useCase = useCase;
            this._notification = notification;
            this._outputPort = new AddPropertyImagePresenter();
        }

        /// <inheritdoc />
        public void SetOutputPort(IOutputPort outputPort)
        {
            this._outputPort = outputPort;
            this._useCase.SetOutputPort(outputPort);
        }

        public async Task Execute(string fileName, byte[] file, Guid propertyGuid)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                this._notification
                    .Add(nameof(fileName), "Name is required.");
            }

            if (file.Length <= 0)
            {
                this._notification
                    .Add(nameof(file), "File is required.");
            }

            if (propertyGuid == Guid.Empty)
            {
                this._notification
                    .Add(nameof(propertyGuid), "propertyGuid is required.");
            }

            if (this._notification
                .IsInvalid)
            {
                this._outputPort
                    .Invalid();
                return;
            }

            await this._useCase
                .Execute(fileName, file, propertyGuid)
                .ConfigureAwait(false);
        }
    }
}
