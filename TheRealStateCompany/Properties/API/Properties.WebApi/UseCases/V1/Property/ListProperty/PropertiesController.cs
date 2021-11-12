using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Properties.Application.BussinesCases.ListProperty;
using Properties.Application.Services;
using Properties.WebApi.Modules.Common;
using Properties.WebApi.Modules.Common.Features;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Properties.WebApi.UseCases.V1.Property.ListProperty
{
    /// <summary>
    ///     Properties.
    /// </summary>
    [ApiVersion("1.0")]
    [FeatureGate(CustomFeature.ListProperty)]
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

        void IOutputPort.Ok(IList<Domain.Property> properties) => this._viewModel = this.Ok(new ListPropertyResponse(properties));

        /// <summary>
        ///     Get List of Properties.
        /// </summary>
        /// <response code="200">The List of Properties.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not Found.</response>
        /// <returns>List of properties filtered</returns>
        //[Authorize]
        [HttpPost("FilteredList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListPropertyResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
        public async Task<IActionResult> Post([FromServices] IListPropertyUseCase useCase,
            [FromForm][Required] decimal ownerId,
            [FromForm][Required] string stateAbbr,
            [FromForm][Required] decimal initialPrice,
            [FromForm][Required] decimal maxPrice,
            [FromForm][Required] string year,
            [FromForm][Required] string codeInternal)
        {
            useCase.SetOutputPort(this);

            await useCase.Execute(ownerId, stateAbbr, initialPrice, maxPrice, year, codeInternal)
                .ConfigureAwait(false);

            return this._viewModel!;
        }
    }
}
