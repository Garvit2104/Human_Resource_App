using Human_Resource_App.BLL.GradesService;
using Human_Resource_App.BLL.UserServices;
using Human_Resource_App.DAL.GradesHistoryRepository;
using Human_Resource_App.Data;
using Human_Resource_App.DTOs.UsersDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resource_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IGradesHistory gradeHistoryRepo;
        private readonly HRDbContext context;
        public UsersController(IUserService userService, HRDbContext context, IGradesHistory gradeHistoryRepo)
        {
            this.userService = userService;
            this.context = context;
            this.gradeHistoryRepo = gradeHistoryRepo;
        }

        [HttpPost("employee")]
        public UserResponseDTO AddEmployee(UserRequestDTO userRequestDTO)
        {
            var result = userService.AddEmployee(userRequestDTO);
            return result;
        }

        [HttpGet("employess")]

        public List<UserResponseDTO> GetAllEmployees()
        {
            var result = userService.GetAllEmployess();
            return result;
        }

        [HttpGet("employee/{id}")]

        public UserResponseDTO GetEmployeeById(int id)
        {
            var result = userService.GetEmployeeById(id);
            return result;
        }

        [HttpDelete("employee/{id}")]

        public IActionResult deleteEmployeeById(int id)
        {
            var employee = context.Users.Find(id);

            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            gradeHistoryRepo.DeleteAllGrades(id);

            context.Users.Remove(employee);
            context.SaveChanges();
            return Ok($"Employee with ID {id} deleted successfully.");

        }

        [HttpPut("empoyeed/{id}")]

        public UserResponseDTO updateEmployeeById(int id, UserRequestDTO userRequestDTO)
        {
            var result = userService.updateEmployeeById(id, userRequestDTO);
            return result;
        }
    }
}
