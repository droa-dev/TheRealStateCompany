using Properties.Application.Services;
using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.CreateOwner
{
    public sealed class CreateOwnerUseCase : ICreateOwnerUseCase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IOwnerFactory _ownerFactory;
        private readonly IUnitOfWork _unitOfWork;
        private IOutputPort _outputPort;

        public CreateOwnerUseCase(
            IOwnerRepository ownerRepository,
            IOwnerFactory ownerFactory,
            IUnitOfWork unitOfWork)
        {
            _ownerRepository = ownerRepository;
            _ownerFactory = ownerFactory;
            _unitOfWork = unitOfWork;
            _outputPort = new CreateOwnerPresenter();
        }

        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        public Task Execute(decimal identificationNumber, string name, string address, byte[]? photo, DateTime? birthday) =>
            this.CreateOwner(
                new Identification(identificationNumber), new Name(name), new Address(address), new File(photo!), birthday);

        private async Task CreateOwner(
            Identification identificationNumber, Name name, Address address, File? photo, DateTime? birthday)
        {
            Owner owner = this._ownerFactory
                .NewOwner(identificationNumber, name, address, photo, birthday);

            await this.Create(owner)
                .ConfigureAwait(false);

            this._outputPort?.Ok(owner);
        }

        private async Task Create(Owner owner)
        {
            await this._ownerRepository
                .Create(owner)
                .ConfigureAwait(false);

            await this._unitOfWork
                .Save()
                .ConfigureAwait(false);
        }
    }
}
