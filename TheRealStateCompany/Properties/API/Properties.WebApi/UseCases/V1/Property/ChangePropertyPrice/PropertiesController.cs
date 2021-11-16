using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Properties.Application.BussinesCases.ChangePropertyPrice;
using Properties.Application.Services;
using Properties.WebApi.Modules.Common;
using Properties.WebApi.Modules.Common.Features;
using Properties.WebApi.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Properties.WebApi.UseCases.V1.Property.ChangePropertyPrice
{
    /// <summary>
    ///     Properties.
    /// </summary>
    [ApiVersion("1.0")]
    [FeatureGate(CustomFeature.ChangePropertyPrice)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase, IOutputPort
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
            this._viewModel = this.Ok(new ChangePropertyPriceResponse(new PropertyModel(property)));

        /// <summary>
        ///     Change price of a property.
        /// </summary>
        /// <response code="200">Updated property price.</response>        
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not Found.</response>
        /// <param name="useCase">Use case.</param>
        /// <param name="propertyGuid"></param>        
        /// <param name="price"></param>
        /// <param name="tax"></param>       
        /// <returns>The property updated.</returns>
        [Authorize]
        [HttpPatch("{propertyGuid:guid}/ChangePropertyPrice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangePropertyPriceResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Patch))]
        public async Task<IActionResult> ChangePropertyPrice(
            [FromServices] IChangePropertyPriceUseCase useCase,
            [FromForm][Required] Guid propertyGuid,
            [FromForm][Required] decimal price,
            [FromForm][Required] decimal tax)
        {
            useCase.SetOutputPort(this);

            await useCase.Execute(
                propertyGuid: propertyGuid, price: price, tax: tax)
                .ConfigureAwait(false);

            return this._viewModel!;
        }
    }
}
