﻿using Properties.Domain;

namespace Properties.Application.BussinesCases.CreateProperty
{
    public sealed class CreatePropertyPresenter : IOutputPort
    {
        public Property Property { get; private set; }
        public bool? IsNotFound { get; private set; }
        public bool? InvalidOutput { get; private set; }
        public void Invalid() => InvalidOutput = true;
        public void NotFound() => IsNotFound = true;
        public void Ok(Property property) => Property = property;
    }
}