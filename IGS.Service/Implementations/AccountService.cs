using IGS.Domain.Entity;
using IGS.DAL.Interfaces;
using IGS.Domain.Responce;
using IGS.Domain.Extensions;
using System.Security.Claims;
using IGS.Domain.ViewModels.Account;
using StatusCode = IGS.Domain.Enums.StatusCode;
using System.Net.Http.Headers;

namespace IGS.Service.Implementations
{
    public class AccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                User user = await _userRepository.GetByLogin(model.Login);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже найден",
                        StatusCode = StatusCode.AddElementError,
                    };
                }

                user = new User()
                {
                    Login = model.Login,
                    Password = HashHelper.HashPassword(model.Password),
                };

                await _userRepository.Create(user);
                ClaimsIdentity result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OperationSuccess,
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                User user = await _userRepository.GetByLogin(model.Login);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.FoundElementError,
                    };
                }

                if(user.Password != HashHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "Неверный пароль",
                    };
                }

                ClaimsIdentity result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OperationSuccess,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        } 

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login), };
            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<BaseResponse<User>> GetUser(int id)
        {
            BaseResponse<User> response = new BaseResponse<User>();
            try
            {
                User user = await _userRepository.Get(id);

                if(user == null)
                {
                    response.Description = "Данный пользователь не найден";
                    response.StatusCode = StatusCode.UserNotFound;
                }

                response.Data = user;
                return response;
            }
            catch(Exception ex)
            {
                return new BaseResponse<User>
                {
                    Description = $"[GetUser] : {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<List<User>>> GetUsers()
        {
            BaseResponse<List<User>> response = new BaseResponse<List<User>>();
            try
            {
                List<User> users = await _userRepository.Select();

                if (users.Count == 0)
                {
                    response.Description = "Отсутсвуют элементы";
                    response.StatusCode = StatusCode.EmptyCollection;
                }

                response.Data = users;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<User>>
                {
                    Description = $"[GetUser] : {ex.Message}"
                };
            }
        }
    }
}
