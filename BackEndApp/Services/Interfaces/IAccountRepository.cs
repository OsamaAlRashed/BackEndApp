using BackEndApp.Services.Dtos;

namespace BackEndApp.Services.Interfaces
{
    public interface IAccountRepository
    {
        public Task<string> Login(LoginDto dto);
        public Task<int> Signup(SignupDto dto);
    }
}
