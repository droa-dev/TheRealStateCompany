using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Properties.Application.BussinesCases.CreateProperty;
using Properties.Application.Services;
using Properties.WebApi.Modules.Common;
using Properties.WebApi.Modules.Common.Features;
using Properties.WebApi.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Properties.WebApi.UseCases.V1.Property.CreateProperty
{
    /// <summary>
    ///     Properties.
    /// </summary>
    [ApiVersion("1.0")]
    [FeatureGate(CustomFeature.CreateProperty)]
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
            this._viewModel = this.Ok(new CreatePropertyResponse(new PropertyModel(property)));


        /// <summary>
        ///     Create a property.
        /// </summary>
        /// <response code="200">Property already exists.</response>
        /// <response code="201">Property was created successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <param name="useCase">Use case.</param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="price"></param>
        /// <param name="tax"></param>
        /// <param name="codeInternal"></param>
        /// <param name="year"></param>
        /// <param name="ownerId"></param>
        /// <param name="stateAbbr"></param>
        /// <returns>The newly registered property.</returns>
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatePropertyResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatePropertyResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
        public async Task<IActionResult> Post(
            [FromServices] ICreatePropertyUseCase useCase,
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
                name: name, address: address, price: price, tax: tax, codeInternal: codeInternal,
                year: year, ownerIdentification: ownerId, countryStateAbb: stateAbbr)
                .ConfigureAwait(false);

            return this._viewModel!;
        }
    }
}
