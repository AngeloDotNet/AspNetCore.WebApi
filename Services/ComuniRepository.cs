using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AEP_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AEP_WebApi.Services
{
    public class ComuniRepository : IComuniRepository
    {
        ComuniDbContext comuniDbContext;
        public ComuniRepository(ComuniDbContext comuniDbContext)
        {
            this.comuniDbContext = comuniDbContext;
        }
        public async Task<ICollection<Comuni>> GetComuni()
        {
            return await this.comuniDbContext.Comuni
                .Where(a => a.Comune != null)
                .OrderBy(a => a.IdComune)
                .ToListAsync();
        }
        public async Task<Comuni> GetComune(string comune)
        {
            return await this.comuniDbContext.Comuni
                .Where(a => a.Comune == comune)
                .OrderBy(a => a.IdComune)
                .FirstOrDefaultAsync();
        }
        public async Task<ICollection<Comuni>> GetCap(string cap)
        {
            return await this.comuniDbContext.Comuni
                .Where(a => a.Cap == cap)
                .OrderBy(a => a.IdComune)
                .ToListAsync();
        }
        public async Task<bool> EsisteComune(string comune)
        {
            return await this.comuniDbContext.Comuni
                .AnyAsync(a => a.Comune == comune);
        }
        public async Task<bool> EsisteCap(string cap)
        {
            return await this.comuniDbContext.Comuni
                .AnyAsync(a => a.Cap == cap);
        }
        public bool NuovoComune(Comuni comune)
        {
            this.comuniDbContext.Add(comune);
            return Salva();
        }
        public bool ModificaComune(Comuni comune)
        {
            this.comuniDbContext.Update(comune);
            return Salva();
        }
        public bool Salva()
        {
            var SalvaDato = this.comuniDbContext.SaveChanges();
            return SalvaDato >= 0 ? true : false;
        }
    }
}