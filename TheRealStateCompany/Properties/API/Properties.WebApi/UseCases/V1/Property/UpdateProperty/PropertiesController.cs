using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Properties.Application.BussinesCases.UpdateProperty;
using Properties.Application.Services;
using Properties.WebApi.Modules.Common;
using Properties.WebApi.Modules.Common.Features;
using Properties.WebApi.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Properties.WebApi.UseCases.V1.Property.UpdateProperty
{
    /// <summary>
    ///     Properties.
    /// </summary>
    [ApiVersion("1.0")]
    [FeatureGate(CustomFeature.UpdateProperty)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class PropertiesController : ControllerBase, IOutputPort
    {
        private readonly Notification _notification;

        public PropertiesController(Notification notification)
        {
            this._notification = notification;
        }

        private IActionResult? _viewModel;

        void IOutputPort.Invalid()
        {
            ValidationProblemDetails problemDetails = new ValidationProblemDetails(this._notification.ModelState);
            this._viewModel = this.BadRequest(problemDetails);
        }

        void IOutputPort.NotFound() => this._viewModel = this.NotFound();

        void IOutputPort.Ok(Domain.Property property) =>
            this._viewModel = this.Ok(new UpdatePropertyResponse(new PropertyModel(property)));

        /// <summary>
        ///     Update a property.
        /// </summary>
        /// <response code="200">Updated property.</response>        
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not Found.</response>
        /// <param name="useCase">Use case.</param>
        /// <param name="propertyGuid"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="price"></param>
        /// <param name="tax"></param>
        /// <param name="codeInternal"></param>
        /// <param name="year"></param>
        /// <param name="ownerId"></param>
        /// <param name="stateAbbr"></param>
        /// <returns>The updated property.</returns>
        //[Authorize]
        [HttpPatch("{propertyGuid:guid}/Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatePropertyResponse))]        
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Patch))]
        public async Task<IActionResult> Update(
            [FromServices] IUpdatePropertyUseCase useCase,
            [FromForm][Required] Guid propertyGuid,
            [FromForm][Required] string name,
            [FromForm][Required] string address,
            [FromForm][Required] decimal price,
            [FromForm][Required] decimal tax,
            [FromForm][Required] string codeInternal,
            [FromForm][Required] string year,
            [FromForm][Required] decimal ownerId,
            [FromForm][Required] string stateAbbr)
        {
            useCase.SetOutputPort(this);

            await useCase.Execute(
                propertyGuid: propertyGuid, name: name, address: address, price: price, tax: tax, 
                codeInternal: codeInternal, year: year, ownerIdentification: ownerId, countryStateAbb: stateAbbr)
                .ConfigureAwait(false);

            return this._viewModel!;
        }
    }
}
