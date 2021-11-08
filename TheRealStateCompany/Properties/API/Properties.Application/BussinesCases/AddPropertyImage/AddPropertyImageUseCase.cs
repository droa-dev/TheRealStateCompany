﻿using Properties.Application.Services;
using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.AddPropertyImage
{
    public class AddPropertyImageUseCase : IAddPropertyImageUseCase
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyRepository _propertyRepository;       
        private readonly IPropertyFactory _propertyFactory;
        private readonly IUnitOfWork _unitOfWork;
        private IOutputPort _outputPort;

        public AddPropertyImageUseCase(
            IPropertyImageRepository propertyImageRepository,
            IPropertyRepository propertyRepository,            
            IPropertyFactory propertyFactory,
            IUnitOfWork unitOfWork)
        {
            _propertyImageRepository = propertyImageRepository;
            _propertyRepository = propertyRepository;            
            _propertyFactory = propertyFactory;
            _unitOfWork = unitOfWork;
            _outputPort = new AddPropertyImagePresenter();
        }

        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        public Task Execute(string fileName, byte[] file, Guid propertyGuid) =>
            this.AddPropertyImage(
                new Name(fileName), new File(file), new PropertyGuid(propertyGuid));

        private async Task AddPropertyImage(
            Name fileName, File file, PropertyGuid propertyGuid)
        {
            Property property = await _propertyRepository
                .GetProperty(propertyGuid)
                .ConfigureAwait(false);

            if (property is Property registeredProperty)
            {
                PropertyImage propertyImage = _propertyFactory
                    .NewPropertyImage(fileName, file, registeredProperty.PropertyId);

                await this.Create(propertyImage)
                    .ConfigureAwait(false);

                this._outputPort?.Ok(propertyImage);
            }

            this._outputPort.NotFound();
        }

        private async Task Create(PropertyImage propertyImage)
        {
            await this._propertyImageRepository
                .Add(propertyImage)
                .ConfigureAwait(false);

            await this._unitOfWork
                .Save()
                .ConfigureAwait(false);
        }
    }
}
