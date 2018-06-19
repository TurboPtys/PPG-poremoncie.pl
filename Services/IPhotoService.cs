using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PPG_projekt.Models;

namespace PPG_projekt.Services
{
    public interface IPhotoService
    {
        Task<Photo> GetMainPhotoAsync(int Id);
    }
}
