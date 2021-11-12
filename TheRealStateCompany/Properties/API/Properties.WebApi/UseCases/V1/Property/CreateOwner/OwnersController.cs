using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Properties.Application.BussinesCases.CreateOwner;
using Properties.Application.Services;
using Properties.Domain;
using Properties.WebApi.Modules.Common;
using Properties.WebApi.Modules.Common.Features;
using Properties.WebApi.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Properties.WebApi.UseCases.V1.Property.CreateOwner
{
    /// <summary>
    ///     Owners.
    /// </summary>
    [ApiVersion("1.0")]
    [FeatureGate(CustomFeature.CreateProperty)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase, IOutputPort
    {
        private readonly Notification _notification;

        public OwnersController(Notification notification)
        {
            this._notification = notification;
        }

        private IActionResult? _viewModel;

        void IOutputPort.Invalid()
        {
            ValidationProblemDetails problemDetails = new ValidationProblemDetails(this._notification.ModelState);
            this._viewModel = this.BadRequest(problemDetails);
        }

        void IOutputPort.Ok(Owner owner) =>
            this._viewModel = this.Ok(new CreateOwnerResponse(new OwnerModel(owner)));

        /// <summary>
        ///     Create a Owner.
        /// </summary>        
        /// <response code="201">Property was created successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <param name="useCase">Use case.</param>
        /// <param name="identificationNumber"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="photo"></param>
        /// <param name="birthday"></param>        
        /// <returns>The newly registered Owner.</returns>
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateOwnerResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateOwnerResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
        public async Task<IActionResult> Post(
            [FromServices] ICreateOwnerUseCase useCase,
            [FromForm][Required] decimal identificationNumber,
            [FromForm][Required] string name,
            [FromForm][Required] string address,
            [FromForm]           byte[]? photo,
            [FromForm]           DateTime? birthday)
        {
            useCase.SetOutputPort(this);

            await useCase.Execute(
                identificationNumber: identificationNumber, name: name, address: address, photo: photo, birthday: birthday)
                .ConfigureAwait(false);

            return this._viewModel!;
        }
    }
}
