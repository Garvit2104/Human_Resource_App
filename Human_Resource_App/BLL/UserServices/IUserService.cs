using Human_Resource_App.DTOs.UsersDTO;

namespace Human_Resource_App.BLL.UserServices
{
    public interface IUserService 
    {
        UserResponseDTO AddEmployee(UserRequestDTO userRequestDTO);

        List<UserResponseDTO> GetAllEmployess();

        UserResponseDTO GetEmployeeById(int employeeId);

        UserResponseDTO updateEmployeeById(int id, UserRequestDTO userRequestDTO);
    }
}
