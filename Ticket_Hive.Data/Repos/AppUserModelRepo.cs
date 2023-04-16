using Microsoft.EntityFrameworkCore;
using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public class AppUserModelRepo : IAppUserModelRepo
    {
        private readonly EventDbContext context;

        public AppUserModelRepo(EventDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds a new AppUser to the database 
        /// </summary>
        public async Task AddAppUserAsync(AppUserModel newAppUser)
        {
            await context.AppUsers.AddAsync(newAppUser);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an AppUser from the database 
        /// </summary>
        public async Task DeleteAppUserAsync(AppUserModel appUserToDelete)
        {
            AppUserModel? appUser = await context.AppUsers.FirstOrDefaultAsync(appUser => appUser.Id == appUserToDelete.Id);
            if (appUser != null)
            {
                context.AppUsers.Remove(appUser);
                await context.SaveChangesAsync();
            }

        }

        /// <summary>
        /// Retrieves all AppUsers from the database 
        /// </summary>
        public async Task<List<AppUserModel>?> GetAllUsersAsync()
        {
            return await context.AppUsers.Include(a => a.Bookings).ToListAsync();
        }

        /// <summary>
        /// Retrieves an AppUser by its ID from the database 
        /// </summary>
        public async Task<AppUserModel?> GetUserByIdAsyncAsync(int id)
        {
            return await context.AppUsers.Include(a => a.Bookings).FirstOrDefaultAsync(appUser => appUser.Id == id);
        }

        /// <summary>
        /// Retrieves an AppUser by its username from the database 
        /// </summary>
        /// <returns>AppUserModel object if found, otherwise null</returns>
        public async Task<AppUserModel?> GetUserByUserNameAsync(string userName)
        {
            return await context.AppUsers.Include(a => a.Bookings).FirstOrDefaultAsync(appUser => appUser.Username == userName);
        }

        /// <summary>
        /// Updates an AppUser in the database 
        /// </summary>
        /// <returns>True if the update was successful, otherwise false</returns>
        public async Task<bool> UpdateAppUserAsync(AppUserModel updatedAppUser)
        {
            AppUserModel? appUserToChange = await context.AppUsers.FirstOrDefaultAsync(appUser => appUser.Id == updatedAppUser.Id);
            if (appUserToChange != null)
            {
                appUserToChange.Username = updatedAppUser.Username;
                appUserToChange.Bookings = updatedAppUser.Bookings;
                context.AppUsers.Update(appUserToChange);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
