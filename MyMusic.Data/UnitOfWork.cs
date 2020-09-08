﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;
using MyMusic.Data.Configurations;
using MyMusic.Data.Repositories;

namespace MyMusic.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyMusicDbContext _context;
        private MusicRepository _musicRepository;
        private ArtistRepository _artistRepository;
        public UnitOfWork(MyMusicDbContext context)
        {
            _context = context;
        }

        public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);
        public IMusicRepository Musics => _musicRepository = _musicRepository ?? new MusicRepository(_context);

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
