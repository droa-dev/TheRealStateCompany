using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.ListProperty
{
    public interface IListPropertyUseCase
    {
        /// <summary>
        ///     Executes the Use Case
        /// </summary>
        Task Execute(decimal? ownerIdentification, string countryStateAbb, decimal? initialPrice,
            decimal? maxPrice, string year, string codeInternal);
        /// <summary>
        ///     Sets the Output Port.
        /// </summary>
        /// <param name="outputPort">Output Port</param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
