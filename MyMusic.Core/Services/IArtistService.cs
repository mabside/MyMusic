using System.Collections.Generic;
using System.Threading.Tasks;
using MyMusic.Core.Models;

namespace MyMusic.Core.Services
{   
    public interface IArtistervice
    {
        Task<IEnumerable<Artist>> GetAllArtist();
        Task<Artist> GetArtistById(int id);
        Task<Artist> CreateArtisit(int id);
        Task UpdateArtist(Artist artisitToBeUpdated, Artist artist);
        Task DeleteArtist(Artist artist);
    }
}