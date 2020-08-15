using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;

namespace MyMusic.Data.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(MyMusicDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Artist>> GetAllWithMusicAsync()
        {
           return await _context.Set<Artist>().AsNoTracking().Include(a => a.Musics).ToListAsync();
        }

        public async Task<Artist> GetWithMusicsByIdAsync(int id)
        {
            return await _context.Set<Artist>().Include(a => a.Musics).SingleOrDefaultAsync(a => a.Id == id); 
        }
    }
}