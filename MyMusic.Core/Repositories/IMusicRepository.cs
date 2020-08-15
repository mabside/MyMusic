using System.Collections.Generic;
using System.Threading.Tasks;
using MyMusic.Core.Models;

namespace MyMusic.Core.Repositories
{
    interface IMusicRepository : IRepository<Music>
    {
        Task<IEnumerable<Music>> GetAllWithArtistAsync();
        Task<Music> GetWithArtistByIdAsync(int id);
        Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId);
    }
}