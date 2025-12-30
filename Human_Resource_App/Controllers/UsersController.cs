using Human_Resource_App.BLL.GradesService;
using Human_Resource_App.BLL.UserServices;
using Human_Resource_App.DAL.GradesHistoryRepository;
using Human_Resource_App.DAL.UsersRepository;
using Human_Resource_App.Data;
using Human_Resource_App.DTOs.UsersDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Human_Resource_App.BLL.UserServices.UserServiceClass;

namespace Human_Resource_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IGradesHistory gradeHistoryRepo;
        private readonly HRDbContext context;
        private readonly IUserRepo _userRepo;
        public UsersController(IUserService userService, HRDbContext context, IGradesHistory gradeHistoryRepo, IUserRepo _userRepo)
        {
            this.userService = userService;
            this.context = context;
            this._userRepo = _userRepo;
            this.gradeHistoryRepo = gradeHistoryRepo;
        }

        [HttpPost("employee")]
        public async Task<IActionResult> AddEmployee(UserRequestDTO userRequestDTO)
        {
            try
            {
                var result = await userService.AddEmployee(userRequestDTO);
                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }
        }

        [HttpGet("employee")]

        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await userService.GetAllEmployess();
                return Ok(result);

            }
            catch(Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }
        }

        [HttpGet("employee/{id}")]

        public async Task<UserResponseDTO> GetEmployeeById(int id)
        {
            var result = await userService.GetEmployeeById(id);
            return result;
        }

        [HttpDelete("employee/{id}")]

        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
             await _userRepo.DeleteEmployeeById(id);

            return Ok($"Employee with ID {id} deleted successfully.");

        }

        [HttpPut("employee/{id}")]

        public async Task<IActionResult> updateEmployeeById(int id, UserRequestDTO userRequestDTO)
        {
            try
            {
            var result = await userService.updateEmployeeById(id, userRequestDTO);
            return NoContent();

            }
            catch (Exception ex)
            { 
                return Problem(title: "Error", detail: ex.Message, statusCode: 400); 
            }
        }
    }
}
