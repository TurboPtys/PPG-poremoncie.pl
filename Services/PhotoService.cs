using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PPG_projekt.Data;
using PPG_projekt.Models;

namespace PPG_projekt.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly ApplicationDbContext _context;
        public PhotoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Photo> GetMainPhotoAsync(int id)
        {
            var img = await _context.Photos.Where(x => x.AdvertId == id && x.IsMain == true).SingleOrDefaultAsync();

            return img;
        }
    }
}
