using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.CreateProperty
{
    public interface ICreatePropertyUseCase
    {
        /// <summary>
        ///     Executes the Use Case
        /// </summary>
        Task Execute(string name, string address, decimal price, decimal tax, string codeInternal, string year, decimal ownerIdentification, string countryStateAbb);
        /// <summary>
        ///     Sets the Output Port.
        /// </summary>
        /// <param name="outputPort">Output Port</param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
