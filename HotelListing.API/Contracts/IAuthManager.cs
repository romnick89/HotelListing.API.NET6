using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(APIUserModel userModel);
        Task <AuthResponseModel> Login(LoginModel loginModel);
        Task<string> CreateRefreshToken();
        Task<AuthResponseModel> VerifyRefreshToken(AuthResponseModel responseModel);
    }
}
