using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebApi.Identity.User)]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDb> _UserStore;

        public UsersApiController(WebStoreDb db) { _UserStore = new UserStore<User, Role, WebStoreDb>(db); }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers() => await _UserStore.Users.ToArrayAsync();

        #region User

        [HttpPost("UserId")]
        public async Task<string> GetUserIdAsync([FromBody] User user) => await _UserStore.GetUserIdAsync(user);

        [HttpPost("UserName")]
        public async Task<string> GetUserNameAsync([FromBody] User user) => await _UserStore.GetUserNameAsync(user);

        [HttpPost("userName/{name}")]
        public async Task<string> SetUserNameAsync([FromBody] User user, string name)
        {
            await _UserStore.SetUserNameAsync(user, name);
            await _UserStore.UpdateAsync(user);
            return user.UserName;
        }

        [HttpPost("NormalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody] User user) => await _UserStore.GetNormalizedUserNameAsync(user);

        [HttpPost("NormalUserName/{name}")]
        public async Task<string> SetNormalizedUserNameAsync([FromBody] User user, string name)
        {
            await _UserStore.SetNormalizedUserNameAsync(user, name);
            await _UserStore.UpdateAsync(user);
            return user.NormalizedUserName;
        }

        [HttpPost("User")]
        public async Task<bool> CreateAsync([FromBody] User user)
        {
            var creationResult = await _UserStore.CreateAsync(user);
            return creationResult.Succeeded;
        }

        [HttpPut("User")]
        public async Task<bool> UpdateAsync([FromBody] User user)
        {
            var updateResult = await _UserStore.UpdateAsync(user);
            return updateResult.Succeeded;
        }

        [HttpPost("User/Delete")]
        public async Task<bool> DeleteAsync([FromBody] User user)
        {
            var deleteResult = await _UserStore.DeleteAsync(user);
            return deleteResult.Succeeded;
        }

        [HttpGet("User/Find/{id}")]
        public async Task<User> FindByIdAsync(string id) => await _UserStore.FindByIdAsync(id);

        [HttpGet("User/Normal/{name}")]
        public async Task<User> FindByNameAsync(string name) => await _UserStore.FindByNameAsync(name);

        [HttpPost("Role/{role}")]
        public async Task AddToRoleAsync([FromBody] User user, string role, [FromServices] WebStoreDb db)
        {
            await _UserStore.AddToRoleAsync(user, role);
            await db.SaveChangesAsync();
        }

        [HttpPost("Role/Delete/{role}")]
        public async Task RemoveFromRoleAsync([FromBody] User user, string role, [FromServices] WebStoreDb db)
        {
            await _UserStore.RemoveFromRoleAsync(user, role);
            await db.SaveChangesAsync();
        }

        [HttpPost("InRole/{role}")]
        public async Task<bool> IsInRoleAsync([FromBody] User user, string role) => await _UserStore.IsInRoleAsync(user, role);

        [HttpGet("UsersInRole/{role}")]
        public async Task<IList<User>> GetUsersInRoleAsync(string role) => await _UserStore.GetUsersInRoleAsync(role);

        [HttpPost("GetPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody] User user) => await _UserStore.GetPasswordHashAsync(user);

        [HttpPost("SetPasswordHash")]
        public async Task<string> SetPAsswordHashAsync([FromBody] PasswordHashDTO hash)
        {
            await _UserStore.SetPasswordHashAsync(hash.User, hash.Hash);
            await _UserStore.UpdateAsync(hash.User);
            return hash.User.PasswordHash;
        }

        [HttpPost("HasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody] User user) => await _UserStore.HasPasswordAsync(user);

        #endregion
        #region Claims

        [HttpPost("GetClaims")]
        public async Task<IList<Claim>> GetClaimsAsync([FromBody] User user) => await _UserStore.GetClaimsAsync(user);

        [HttpPost("AddClaims")]
        public async Task AddClaimsAsync([FromBody] AddClaimDTO ClaimInfo, [FromServices] WebStoreDb db)
        {
            await _UserStore.AddClaimsAsync(ClaimInfo.User, ClaimInfo.Claims);
            await db.SaveChangesAsync();
        }

        [HttpPost("ReplaceClaim")]
        public async Task ReplaceClaimAsync([FromBody] ReplaceClaimDTO ClaimInfo, [FromServices] WebStoreDb db)
        {
            await _UserStore.ReplaceClaimAsync(ClaimInfo.User, ClaimInfo.Claim, ClaimInfo.NewClaim);
            await db.SaveChangesAsync();
        }

        [HttpPost("RemoveClaim")]
        public async Task RemoveClaimsAsync([FromBody] RemoveClaimDTO ClaimInfo, [FromServices] WebStoreDb db)
        {
            await _UserStore.RemoveClaimsAsync(ClaimInfo.User, ClaimInfo.Claims);
            await db.SaveChangesAsync();
        }

        [HttpPost("GetUsersForClaim")]
        public async Task<IList<User>> GetUsersForClaimAsync([FromBody] Claim claim) =>
            await _UserStore.GetUsersForClaimAsync(claim);

        #endregion

    }
}
