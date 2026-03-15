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
        public async Task<ActionResult<UserResponseDTO>> AddEmployee(UserRequestDTO userRequestDTO)
        {
            try
            {
                if (userRequestDTO == null)
                    return BadRequest("Employee Data cannot be null");

                var result = await userService.AddEmployee(userRequestDTO);
                return StatusCode(201, result);
            }
            catch(Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }
        }

        [HttpGet("employees")]

        public async Task<ActionResult<UserResponseDTO>> GetAllEmployees()
        {
            try
            {
                var result = await userService.GetAllEmployess();
                if (result == null || !result.Any())
                    return NotFound("No Employee Found");

                return Ok(result);

            }
            catch(Exception ex)
            {
                return Problem(title: "Error Fetching Employees", detail: ex.Message, statusCode: 400);
            }
        }

        [HttpGet("employees/{id}")]

        public async Task<ActionResult<UserResponseDTO>> GetEmployeeById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Invalid Employee Id");
                }
            var result = await userService.GetEmployeeById(id);
            return Ok(result);
            }
            catch(Exception ex)
            {
                return Problem(title: "Error Fetching Employee", detail: ex.Message, statusCode: 400);
            }
        }

        [HttpDelete("employees/{id}")]

        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Employee Id");
                }

                await _userRepo.DeleteEmployeeById(id);

               return Ok($"Employee with ID {id} deleted successfully.");
                
            }
            catch(Exception ex)
            {
                return Problem(title: "Error Deleting Employee", detail: ex.Message, statusCode: 400);
            }
        }

        [HttpPut("employees/{id}")]

        public async Task<ActionResult<UserResponseDTO>> updateEmployeeById(int id, UserRequestDTO userRequestDTO)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Employee Id");
                }
                if (userRequestDTO == null)
                {
                    return BadRequest("Employee Data cannot be null");
                }

             var result = await userService.updateEmployeeById(id, userRequestDTO);
            return Ok(result);

            }
            catch (Exception ex)
            { 
                return Problem(title: "Error updating Status", detail: ex.Message, statusCode: 400); 
            }
        }
    }
}
