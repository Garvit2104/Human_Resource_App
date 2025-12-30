using Human_Resource_App.DTOs.UsersDTO;

namespace Human_Resource_App.BLL.UserServices
{
    public interface IUserService 
    {
        Task<UserResponseDTO> AddEmployee(UserRequestDTO userRequestDTO);

        public Task<IEnumerable<UserResponseDTO>> GetAllEmployess();

        Task<UserResponseDTO> GetEmployeeById(int employeeId);

        Task<UserResponseDTO> updateEmployeeById(int id, UserRequestDTO userRequestDTO);
    }
}
