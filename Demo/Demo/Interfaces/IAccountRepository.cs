using Demo.DTOs.Account;
using Demo.Models;

namespace Demo.Interfaces
{
    public interface IAccountRepository
    {
        Task<string?> SignUpAsync(SignUpDTO signUpDTO);
        Task<string?> SignInAsync(SignInDTO signInDTO);
        Task<string> ChangePasswordAsync(AppUser user, ChangePasswordDTO changePasswordDTO);
        Task<string> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);
    }
}
