using Demo.DTOs.Account;

namespace Demo.Interfaces
{
    public interface IAccountRepository
    {
        Task<NewUserDTO?> SignUpAsync(SignUpDTO signUpDTO);
        Task<NewUserDTO?> SignInAsync(SignInDTO signInDTO);
    }
}
