using Properties.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public interface ICountryStatesRepository
    {
        Task<ICountryStates> GetCountryState(Abbreviation abbreviation);
        Task<ICountryStates> GetCountryState(CountryStatesId countryStatesId);
        Task<ICountryStates> GetCountryState(Name name);
    }
}
