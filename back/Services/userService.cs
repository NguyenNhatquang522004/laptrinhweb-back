using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class userService : IUserRepository
    {

        private readonly ApplicationDbContext applicationDbContext;
        private readonly IEmailRepository _emailService;


        public userService(ApplicationDbContext applicationDbContext, IEmailRepository emailService)
        {

            this.applicationDbContext = applicationDbContext;
            _emailService = emailService;
        }


        public async Task<globalResponds> CreateUserAsync(User user)
        {
            try
            {
                await applicationDbContext.Users.AddAsync(user);
                await applicationDbContext.SaveChangesAsync();
                return new globalResponds("1", "thành công.", user);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while creating the user.", null);

            }


        }

        public async Task<globalResponds> DeleteUserAsync(Guid userId)
        {
            try
            {
                User searchUser = await applicationDbContext.Users.FindAsync(userId);
                applicationDbContext.Users.Remove(searchUser);
                await applicationDbContext.SaveChangesAsync();
                return new globalResponds("400", "thành công.", null);
            }
            catch (Exception ex)
            {
                return new globalResponds("500", "An error occurred while deleting the user.", null);
            }
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await applicationDbContext.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all users.", ex);
            }


        }
        public async Task<globalResponds> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = applicationDbContext.Users.FirstOrDefault(u => u.Email == email);
                return new globalResponds("1", "thành công.", user);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving the user by email.", null);
            }

        }
        public async Task<globalResponds> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                return new globalResponds("400", "thành công.", user);
            }
            catch (Exception ex)
            {

                return new globalResponds("500", "An error occurred while retrieving the user by ID.", null);

            }
        }
        public Task<globalResponds> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = applicationDbContext.Users.FirstOrDefault(u => u.Username == username);

                return Task.FromResult(new globalResponds("400", "thành công.", user));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new globalResponds("500", "An error occurred while retrieving the user by username.", null));
            }

        }
        public Task<globalResponds> SaveChangesAsync()
        {

            throw new NotImplementedException();
        }
        public async Task<globalResponds> UpdateUserAsync(User user)
        {
            try
            {
                applicationDbContext.Users.Update(user);
                await applicationDbContext.SaveChangesAsync();
                return new globalResponds("1", "thành công.", null);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while updating the user.", null);
            }

        }
        public async Task<globalResponds> UserExistsAsync(Guid userId)
        {
            try
            {
                var userExists = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                return new globalResponds("400", "thành công.", userExists);
            }
            catch (Exception ex)
            {
                return new globalResponds("500", "An error occurred while checking if the user exists.", null);
            }
        }


    }
}
