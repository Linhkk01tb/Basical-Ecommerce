using Demo.DTOs.Account;
using Demo.Helpers;
using Demo.Helpers.Email;
using Demo.Interfaces;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;

namespace Demo.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICartRepository _cartRepository;
        private readonly IEmailSenderService _emailSenderService;

        public AccountRepository(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, ICartRepository cartRepository, IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _cartRepository = cartRepository;
            _emailSenderService = emailSenderService;
        }
        public async Task<NewUserDTO?> SignInAsync(SignInDTO signInDTO)
        {
            var user = await _userManager.FindByNameAsync(signInDTO.UserName);
            if (user == null)
                return null;
            if(!await _userManager.IsEmailConfirmedAsync(user))
                throw new Exception("Email is not confirmed!");
            if (!await _userManager.CheckPasswordAsync(user, signInDTO.Password))
                throw new Exception("Invalid password!");
            var result = await _signInManager.CheckPasswordSignInAsync(user, signInDTO.Password, false);
            if (!result.Succeeded)
                throw new Exception("User or password incorrect!Try again!");
            return new NewUserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user)
            };

        }

        public async Task<NewUserDTO> SignUpAsync(SignUpDTO signUpDTO)
        {
            var user = new AppUser
            {
                UserName = signUpDTO.UserName,
                Email = signUpDTO.Email,
            };
            var newUser = await _userManager.CreateAsync(user, signUpDTO.Password);
            if (!newUser.Succeeded)
                throw new Exception($"Username {user.UserName} existed or invalid password!");
            var userRole = await _userManager.AddToRoleAsync(user, AppRoles.Admin);
            if (!userRole.Succeeded)
                throw new Exception("Invalid role!");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", user.Email}
            };
            var callback = QueryHelpers.AddQueryString(signUpDTO.ClientUri!, param);
            var message = new EmailMessage([user.Email], "Confirmation Email", $"Please click the link to confirm your account!\nThe link is only valid for 10 minutes from the time this mail is received!\nLink: {callback}");
            await _emailSenderService.SendEmailAsync(message);
            await _cartRepository.CreateCartAsync(user);
            return new NewUserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user)
            };

        }
    }
}
