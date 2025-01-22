using AutoMapper;
using HospitalReporting.Application.DTOs.Identity;
using HospitalReporting.Application.Exception;
using HospitalReporting.Application.Services.ServicesInterfaces;
using HospitalReporting.Domain.Entities.Identity;
using HospitalReporting.Domain.InterfacesRepository;
using HospitalReporting.Domain.InterfacesRepository.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceApp.Application.Services.Implementation.Authentication
{
    public class AuthenticationService(IRoleManagements _roleManage,
                                       ITokenManagements _tokenManage,
                                       IUserManagements _userManage,
                                       IMapper _mapper,
                                       IUnitOfWork _repo,
                                       UserManager<AppUser> _uaser) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {            
            var mappedUser = _mapper.Map<AppUser>(user);
            mappedUser.PasswordHash = user.Password;
            mappedUser.UserName = (user.Email).Split("@")[0];

            var createUserResult = await _userManage.CreateUserAsync(mappedUser);
            if (!createUserResult) return new ServiceResponse(Message: "Email Address maybe Exist already");

            var checkUser = await _userManage.GetUserByEmailAsync(user.Email);


            bool addingRoleResult = await _roleManage.AddRoleToUserAsync(checkUser, "User");
            if (!addingRoleResult)
            {
                var removingUserResult = await _userManage.RemoveUserByEmailAsync(user.Email);
                if (removingUserResult <= 0)
                {
                    return new ServiceResponse(true, "Faild To Add User");
                }
            }
            return new ServiceResponse(true, "User Added Successfully");
            //todo => verify email
        }

        public async Task<ServiceResponse> LoginNewCommer(LoginForFirstTime data)
        {
            var user = await _userManage.GetUserByEmailAsync(data.Email);
            if (user == null) return new ServiceResponse(Message: "Invalid Email");
            var checkUser = await _uaser.CheckPasswordAsync(user, data.Password);
            if(!checkUser)return new ServiceResponse(Message: "Invalid Password");

            user.FullName = data.FullName;
            user.PhoneNumber = data.PhoneNumber;
            var result = await _repo.CompleteAsync();
            return result <= 0 ? new ServiceResponse(Message: "Faild To Add Data") :
                new ServiceResponse(true, "Data Updated Successfully");
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var checkDataCompeletion = await _userManage.GetUserByEmailAsync(user.Email);
            if(checkDataCompeletion.FullName.IsNullOrEmpty() || checkDataCompeletion.PhoneNumber.IsNullOrEmpty())
                return new LoginResponse(Message: "Please Complete Your Data First");

            var mappedUser = _mapper.Map<AppUser>(user);
            mappedUser.PasswordHash = user.Password;

            var loginResult = await _userManage.LoginUserAsync(mappedUser);
            if (!loginResult) return new LoginResponse(false, "Faild to login");

            var getUserByEmail = await _userManage.GetUserByEmailAsync(user.Email);
            var getUserClaims = await _userManage.GetAllUserClaimsAsync(user.Email);

            var generatedToken = _tokenManage.GenerateToken(getUserClaims);
            var generateRefreshToken = _tokenManage.GetRefreshToken();

            int saveToken = 0;
            var checkUserToken = await _tokenManage.ValidateRefreshTokenAsync(generateRefreshToken);
            if (checkUserToken)
                saveToken = await _tokenManage.UpdateRefreshTokenAsync(getUserByEmail.Id, generateRefreshToken);
            else
                saveToken = await _tokenManage.AddRefreshTokenAsync(getUserByEmail.Id, generateRefreshToken);

            if (saveToken <= 0) return new LoginResponse(false, "Internal Error Occured while Authenticating");
            return new LoginResponse(true, "Loging Successfully", generatedToken, generateRefreshToken);
        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            bool checkTokenValidation = await _tokenManage.ValidateRefreshTokenAsync(refreshToken);
            if (!checkTokenValidation) return new LoginResponse(Message: "Invalid Token");

            var userId = await _tokenManage.GetUserIdByRefreshTokenAsync(refreshToken);
            var user = await _userManage.GetUserByIdAsync(userId);
            var userClaims = await _userManage.GetAllUserClaimsAsync(user!.Email!);

            var generatedToken = _tokenManage.GenerateToken(userClaims);
            var generateRefreshToken = _tokenManage.GetRefreshToken();

            await _tokenManage.UpdateRefreshTokenAsync(userId, generateRefreshToken);
            return new LoginResponse(true, Token: generatedToken, RefreshToken: generateRefreshToken);
        }
    }
}
