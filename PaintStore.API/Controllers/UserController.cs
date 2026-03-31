using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.Models;
using PaintStore.Models.DTOs;
using PaintStore.Models.Interfaces.Services;
using Serilog;

namespace PaintStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new User resource
        /// </summary>
        /// <param name="">User model data</param>
        /// <returns code="201">Returns new User if success</return>
        /// <returns code="500">Throw 500 if unknown exception</return>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {          
            _logger.LogInformation("Create User: Received request");
            
            User user = _mapper.Map<User>(userCreateDto);

            User newUser = await _userService.CreateUserAsync(user);

            UserResponseDto dto = _mapper.Map<UserResponseDto>(newUser);

            _logger.LogInformation($"Create User: User created with id {newUser.Id}");
            return Created("GetUserById",dto);
        }
    }
}
