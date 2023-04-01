using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<APIUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<IEnumerable<IdentityError>> Register(APIUserModel userModel)
        {
            var user = _mapper.Map<APIUser>(userModel);
            user.UserName = userModel.Email;

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }
    }
}
