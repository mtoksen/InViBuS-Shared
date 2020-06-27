using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InViBuS.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InViBuS.API.Services
{
    public class UserStoreService : IUserStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>
    {
        UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(new InViBuSContext());

        public UserStoreService() {}


        public void Dispose()
        {
            userStore.Dispose();
        }

        public Task CreateAsync(User user)
        {
            var context = userStore.Context as InViBuSContext;
            if (user != null)
            {
                if (context != null)
                {
                    context.Users.Add(user);
                    context.Configuration.ValidateOnSaveEnabled = false;
                    return context.SaveChangesAsync();
                }
            }
            // If no user
            return null;
        }

        public Task UpdateAsync(User user)
        {
            var context = userStore.Context as InViBuSContext;
            if (user != null)
            {
                if (context != null)
                {
                    context.Users.Attach(user);
                    context.Entry(user).State = EntityState.Modified;
                    context.Configuration.ValidateOnSaveEnabled = false;
                    return context.SaveChangesAsync();
                }
            }
            // If no user
            return null;
        }

        public Task DeleteAsync(User user)
        {
            var context = userStore.Context as InViBuSContext;
            if (user != null)
            {
                if (context != null)
                {
                    context.Users.Remove(user);
                    context.Configuration.AutoDetectChangesEnabled = false;
                    return context.SaveChangesAsync();
                }
            }
            // If no user
            return null;
        }

        public Task<User> FindByIdAsync(string userId)
        {
            var context = userStore.Context as InViBuSContext;
            if (userId != null)
            {
                if (context != null)
                {
                    return context.Users.Where(user => user.Id.ToLower() == userId.ToLower()).FirstOrDefaultAsync();
                }
            }
            // If no user
            return null;
        }

        public Task<User> FindByNameAsync(string userName)
        {
            var context = userStore.Context as InViBuSContext;
            if (userName != null)
            {
                if (context != null)
                {
                    return context.Users.Where(user => user.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
                }
            }
            // If no user
            return null;
        }

        public async Task<User> FindUserByUsernamePasswordHash(string userName, string passwordHash)
        {
            var context = userStore.Context as InViBuSContext;
            if (userName != null || passwordHash != null)
            {
                if (context != null)
                {
                    return await context.Users.Where(user => user.UserName.ToLower() == userName.ToLower())
                        .Where(user => user.Password == passwordHash)
                        .FirstOrDefaultAsync();
                }
            }
            // If context, username or passwordHash is null
            return null;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            var identityUser = ToIdentityUser(user);
            var pwTask = userStore.SetPasswordHashAsync(identityUser, passwordHash);
            SetApplicationUser(user, identityUser);
            return pwTask;
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            var identityUser = ToIdentityUser(user);
            var pwTask = userStore.GetPasswordHashAsync(identityUser);
            SetApplicationUser(user, identityUser);
            return pwTask;
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            var identityUser = ToIdentityUser(user);
            var pwTask = userStore.HasPasswordAsync(identityUser);
            SetApplicationUser(user, identityUser);
            return pwTask;
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            var identityUser = ToIdentityUser(user);
            var pwTask = userStore.SetSecurityStampAsync(identityUser, stamp);
            SetApplicationUser(user, identityUser);
            return pwTask;
        }

        public Task<string> GetSecurityStampAsync(User user)
        {
            var identityUser = ToIdentityUser(user);
            var pwTask = userStore.GetSecurityStampAsync(identityUser);
            SetApplicationUser(user,identityUser);
            return pwTask;
        }

        private static void SetApplicationUser(User user, IdentityUser identityUser)
	    {
	        user.Password = identityUser.PasswordHash;
	        user.Id = identityUser.Id;
	        user.UserName = identityUser.UserName;
	    }

        // Cast User to IdentityUser for use in the Identity Framework
        private IdentityUser ToIdentityUser(User user)
	    {
	        return new IdentityUser
	        {
	            Id = user.Id,
	            PasswordHash = user.Password,
	            UserName = user.UserName
	        };
	    }
    }
}
