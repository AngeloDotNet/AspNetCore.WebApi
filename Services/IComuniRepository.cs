using System.Collections.Generic;
using System.Threading.Tasks;
using AEP_WebApi.Models;

namespace AEP_WebApi.Services
{
    public interface IComuniRepository
    {
        Task<ICollection<Comuni>> GetComuni();
        Task<Comuni> GetComune(string comune);
        Task<ICollection<Comuni>> GetCap(string cap);

        bool NuovoComune(Comuni comune);
        bool ModificaComune(Comuni comune);
        bool Salva();

        Task<bool> EsisteComune(string comune);
        Task<bool> EsisteCap(string cap);
    }
}