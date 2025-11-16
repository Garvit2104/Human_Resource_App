using Human_Resource_App.DAL.UsersRepository;
using Human_Resource_App.Data;
using Human_Resource_App.DTOs.UsersDTO;
using Human_Resource_App.Models;
namespace Human_Resource_App.BLL.UserServices
{
    public class UserServiceClass :IUserService
    {
        private readonly IUserRepo userRepo;

        public UserServiceClass(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public static  UserResponseDTO mapUserResponseToUserUserResponseDTO(User user)
        {
            return new UserResponseDTO
            {
                employee_id = user.EmployeeId,
                first_name = user.FirstName,
                last_name = user.LastName,
                phone_number = user.PhoneNumber,
                email_address = user.EmailAddress,
                role = user.Role,
                current_grade_id = user.CurrentGradeId
            };
        }

        public static User mapUserRequestDTOtoUser(UserRequestDTO user)
        {
            return new User
            {

                FirstName = user.first_name,
                LastName = user.last_name,
                PhoneNumber = user.phone_number,
                EmailAddress = user.email_address,
                Role = user.role,
                CurrentGradeId = user.current_grade_id

            };
        }
        public List<UserResponseDTO> GetAllEmployess()
        {
            var result = userRepo.GetAllEmployee();
            List<UserResponseDTO> ls = new List<UserResponseDTO>();

            foreach(var item in result)
            {
                ls.Add(mapUserResponseToUserUserResponseDTO(item));
            }
            return ls;
        }

        public UserResponseDTO GetEmployeeById(int employeeId)
        {
            var empData = userRepo.GetEmployeeById(employeeId);
            //List<UserResponseDTO> ls = new List<UserResponseDTO>();

            return mapUserResponseToUserUserResponseDTO(empData); 
        }

        public UserResponseDTO AddEmployee(UserRequestDTO userRequestDTO)
        {
            var userEntity = mapUserRequestDTOtoUser(userRequestDTO); // from mapper function
            
            var storeData = userRepo.AddEmployee(userEntity);        // save to db

            // convert entity to DTO using mapper function
            return mapUserResponseToUserUserResponseDTO(userEntity);

        }

        public UserResponseDTO updateEmployeeById(int id, UserRequestDTO userRequestDTO)
        {
            var empData = userRepo.GetEmployeeById(id);

            empData.FirstName = userRequestDTO.first_name;
            empData.LastName = userRequestDTO.last_name;
            empData.PhoneNumber = userRequestDTO.phone_number;
            empData.EmailAddress = userRequestDTO.email_address;
            empData.Role = userRequestDTO.role;
            empData.CurrentGradeId = userRequestDTO.current_grade_id;

            return mapUserResponseToUserUserResponseDTO(empData);
        }
    }
}
