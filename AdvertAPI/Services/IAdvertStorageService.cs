using AdvertApi.Models;

namespace AdvertAPI.Services
{
    public interface IAdvertStorageService
    {
        Task<string> Add(AdvertModel model);

        Task Confirm(ConfirmAdvertModel model);

        /* Task ConfirmAsync(ConfirmAdvertModel model);
         Task<AdvertModel> GetByIdAsync(string id);
         Task<bool> CheckHealthAsync();
         Task<List<AdvertModel>> GetAllAsync();*/
    }
}
