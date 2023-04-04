using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public interface IAppUserModelRepo
    {
        public Task<List<AppUserModel>?> GetAllUsersAsync();
        public Task<AppUserModel?> GetUserByIdAsyncAsync(int id);
        public Task<AppUserModel?> GetUserByUserNameAsync(string userName);
        public Task AddAppUserAsync(AppUserModel newAppUser);
        public Task<bool> UpdateAppUserAsync(AppUserModel updatedAppUser);
        public Task DeleteAppUserAsync(AppUserModel appUserToDelete);

    }
}
