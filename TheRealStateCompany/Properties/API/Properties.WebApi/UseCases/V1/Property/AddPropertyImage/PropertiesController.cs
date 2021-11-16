using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Properties.Application.BussinesCases.AddPropertyImage;
using Properties.Application.Services;
using Properties.WebApi.Modules.Common;
using Properties.WebApi.Modules.Common.Features;
using Properties.WebApi.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Properties.WebApi.UseCases.V1.Property.AddPropertyImage
{
    /// <summary>
    ///     Properties
    /// </summary>
    [ApiVersion("1.0")]
    [FeatureGate(CustomFeature.AddPropertyImage)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase, IOutputPort
    {
        private readonly Notification _notification;

        /// <summary>
        ///     Properties Constructor
        /// </summary>
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

        void IOutputPort.Ok(Domain.PropertyImage propertyImage) =>
            this._viewModel = this.Ok(new AddPropertyImageResponse(new PropertyImageModel(propertyImage)));

        /// <summary>
        ///     Add image for an existing property.
        /// </summary>        
        /// <response code="201">Iamge was uploaded successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Property Not Found.</response>
        /// <param name="useCase">Use case.</param>
        /// <param name="fileName"></param>
        /// <param name="fileByteArray"></param>
        /// <param name="propertyId"></param>        
        /// <returns>The newly registered image for the property.</returns>
        [Authorize]
        [HttpPost("Image/Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddPropertyImageResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
        public async Task<IActionResult> Add(
            [FromServices] IAddPropertyImageUseCase useCase,
            [FromForm][Required] string fileName,
            [FromForm][Required] byte[] fileByteArray,
            [FromForm][Required] Guid propertyId)
        {
            useCase.SetOutputPort(this);

            await useCase.Execute(
                fileName: fileName, file: fileByteArray, propertyGuid: propertyId)
                .ConfigureAwait(false);

            return this._viewModel!;
        }
    }
}
