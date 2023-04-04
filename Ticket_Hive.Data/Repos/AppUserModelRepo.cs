using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public class AppUserModelRepo : IAppUserModelRepo
    {
        public Task<bool> AddAppUserAsync(AppUserModel newAppUser)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAppUserAsync(AppUserModel appUserToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUserModel>?> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppUserModel?> GetUserByIdAsyncAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUserModel?> GetUserByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAppUserAsync(AppUserModel updatedAppUser)
        {
            throw new NotImplementedException();
        }
    }
}
