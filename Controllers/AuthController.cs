using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoWish.DataContext;
using TodoWish.Dto;
using TodoWish.Helpers;
using TodoWish.Models;
using TodoWish.Services;

namespace TodoWish.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly AppDataContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public AuthController(IUserService userService, IMapper mapper, IAuthService authService,
            AppDataContext context)
        {
            this.userService = userService;
            this.authService = authService;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDto regUser)
        {
            if (await authService.UserExistsAsync(regUser.Email))
                ModelState.AddModelError("UserName", "Username already exist");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addUser = new User
            {
                Email = regUser.Email,
                Name = regUser.Name,
                FirstName = regUser.FirstName,
                UserRole = "user",
                RegisterDate = DateTime.Now,
                LastLog = DateTime.Now
            };

            var newUser = await authService.RegisterAsync(addUser, regUser.Password);
            return StatusCode(201, newUser);
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto logUser)
        {
            var user = await authService.LoginAsync(logUser.Email, logUser.Password);

            if (user == null)
                return Unauthorized();

            if (user.UserRole == "admin")
            {
                var adminUser = await context.Users.FindAsync(user.Id);
                adminUser.LastLog = DateTime.Now;
            }

            if (user.UserRole == "user")
            {
                var customerUser = await context.Users.FindAsync(user.Id);
                customerUser.LastLog = DateTime.Now;
            }

            await userService.CheckStatusAsync(user.Id);
            await context.SaveChangesAsync();
            return Ok(mapper.Map<LoginResultDto>(user));
        }
    }
}