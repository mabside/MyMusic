using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;
using MyMusic.Data.Configurations;

namespace MyMusic.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyMusicDbContext _context;
        public UnitOfWork(MyMusicDbContext context)
        {
            _context = context;
        }

        public IMusicRepository Musics {get; private set;}
        public IArtistRepository Artists {get; private set;}

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
