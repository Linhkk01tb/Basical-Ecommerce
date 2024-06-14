using Demo.DTOs.Account;
using Demo.Interfaces;
using Demo.Models;
using Demo.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoECommercePrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountRepository _accountRepository;

        public AccountController(UserManager<AppUser> userManager, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _accountRepository = accountRepository;
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInDTO signInDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var user = await _accountRepository.SignInAsync(signInDTO);
                if (user == null)
                    return StatusCode(StatusCodes.Status401Unauthorized, "Invalid username or password!");
                return StatusCode(StatusCodes.Status200OK, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var newUser = await _accountRepository.SignUpAsync(signUpDTO);
                if (newUser == null)
                    return StatusCode(StatusCodes.Status401Unauthorized, "Failed to sign up! Try again!");
                return StatusCode(StatusCodes.Status201Created, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid Email Confirmation Request!");

                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid Email Confirmation Request!");
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
