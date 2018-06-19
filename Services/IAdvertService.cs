using PPG_projekt.Models;
using PPG_projekt.Models.AdminViewModels;
using PPG_projekt.Models.IndexViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PPG_projekt.Models.AdvertViewModels;

namespace PPG_projekt.Services
{
    public interface IAdvertService
    {
        Task<AdvertDetailViewModel> DetalisAdvert(int id);
        Task<IEnumerable<Advert>> GetIncompleteAdvertAsync(string id);
        Task<IEnumerable<Advert>> GetIncompleteAdvertAsync();
        Task<IEnumerable<Advert>> GetIncompleteDeactivateAdvertAsync();
        Task<IEnumerable<AdvertsListVievModel>> AdvertsListAsync();
        Task<IEnumerable<AdvertsListVievModel>> DeactivateAdvertsListAsync();
        Task<AdvertsListVievModel> AdvertDetail(int ID);
        Task<bool> DeleteAdvertAsync(int ID);
        Task<bool> DeleteAllAdvertsOfUserAsync(string ID);
        Task<bool> DeactivateAdvert(int ID);
        Task<bool> ActivateAdvert(int ID);
        Task<bool> DeactivateAllUsersAdvert(string ID);

        Task<IEnumerable<Advert>> Search(string searchString);
        Task<IEnumerable<AdvertsListVievModel>> Search(SearchModel s);







    }
}
