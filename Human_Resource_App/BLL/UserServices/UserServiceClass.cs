using Human_Resource_App.DAL.UsersRepository;
using Human_Resource_App.Data;
using Human_Resource_App.DTOs.UsersDTO;
using Human_Resource_App.Models;
using Human_Resource_App.DTOs.GradesHistoryDTO;
using Human_Resource_App.DAL.GradesRepository;
using Human_Resource_App.DAL.GradesHistoryRepository;
namespace Human_Resource_App.BLL.UserServices
{
    public class UserServiceClass :IUserService
    {
        private readonly IUserRepo userRepo;
        private readonly IGradesHistory gradesHistoryRepo;
        private readonly IGradesRepo gradesRepo;

        public UserServiceClass(IUserRepo userRepo, IGradesHistory gradesHistoryRepo, IGradesRepo gradesRepo)
        {
            this.userRepo = userRepo;
            this.gradesHistoryRepo = gradesHistoryRepo;
            this.gradesRepo = gradesRepo;
        }
        public UserResponseDTO mapUserResponseToUserUserResponseDTO(User user)
        {
            
            Grade grade = gradesRepo.GetGradeById(user.CurrentGradeId.Value);

            
            return new UserResponseDTO
            {
                employee_id = user.EmployeeId,
                first_name = user.FirstName,
                last_name = user.LastName,
                phone_number = user.PhoneNumber,
                email_address = user.EmailAddress,
                role = user.Role,
                current_grade_id = grade.Name
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


        private void ValidateEmployee(UserRequestDTO userRequestDTO)
        {

            // Rule 2: Email must end with @cognizant.com
            if (string.IsNullOrWhiteSpace(userRequestDTO.email_address) ||
                !userRequestDTO.email_address.EndsWith("@cognizant.com", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Email address must be in the format xxxx@cognizant.com.");
            }
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
            ValidateEmployee(userRequestDTO);
            var userEntity = mapUserRequestDTOtoUser(userRequestDTO); // from mapper function


            if (userEntity.Role.Equals("TravelDeskExec", StringComparison.OrdinalIgnoreCase))
            {
                userEntity.CurrentGradeId = 1;
            }

            var storeData = userRepo.AddEmployee(userEntity);        // save to db
                
            GradeHistory gradeHistoryEntity = new GradeHistory();
            gradeHistoryEntity.AssignedOn = DateOnly.FromDateTime(DateTime.UtcNow);
            gradeHistoryEntity.EmployeeId = storeData.EmployeeId;
            gradeHistoryEntity.GradeId = storeData.CurrentGradeId;

            gradesHistoryRepo.saveGradeHistory(gradeHistoryEntity);
            

            // convert entity to DTO using mapper function
            return mapUserResponseToUserUserResponseDTO(userEntity);

        }

        public UserResponseDTO updateEmployeeById(int id, UserRequestDTO userRequestDTO)
        {
            ValidateEmployee(userRequestDTO);
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
