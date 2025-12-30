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
        public class GradeUpdateRuleViolationException : Exception
        {
            public GradeUpdateRuleViolationException() : base("Grade Update Rule Violation Exception") { }
        }
        public async Task<UserResponseDTO> MapEntityResponseToUserResponseDTO(User user)
        {
            
            //Grade grade = await gradesRepo.GetGradeById(user.CurrentGradeId.Value
  
            return new UserResponseDTO
            {
                employee_id = user.EmployeeId,
                first_name = user.FirstName,
                last_name = user.LastName,
                phone_number = user.PhoneNumber,
                email_address = user.EmailAddress,
                role = user.Role,
                current_grade_id = user.CurrentGrade?.Name
            };
        }

        public static User MapUserRequestDTOtoEntity(UserRequestDTO user)
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


        private static void ValidateEmployee(UserRequestDTO userRequestDTO)
        {

            // Rule 2: Email must end with @cognizant.com
            if (string.IsNullOrWhiteSpace(userRequestDTO.email_address) ||
                !userRequestDTO.email_address.EndsWith("@cognizant.com", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Email address must be in the format xxxx@cognizant.com");
            }
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllEmployess()
        {
            var result = await userRepo.GetAllEmployee();
            List<UserResponseDTO> ls = new List<UserResponseDTO>();

            foreach(var item in result)
            {
                ls.Add(await MapEntityResponseToUserResponseDTO(item));
            }
            return ls;
        }

        public async Task<UserResponseDTO> GetEmployeeById(int employeeId)
        {
            var empData =  await userRepo.GetEmployeeById(employeeId);
            //List<UserResponseDTO> ls = new List<UserResponseDTO>();

            return await MapEntityResponseToUserResponseDTO(empData); 
        }

        public async Task<UserResponseDTO> AddEmployee(UserRequestDTO userRequestDTO)
        {
            ValidateEmployee(userRequestDTO);
            var userEntity = MapUserRequestDTOtoEntity(userRequestDTO); // from mapper function

            if (userEntity.Role.Equals("TravelDeskExec", StringComparison.OrdinalIgnoreCase))
            {
                userEntity.CurrentGradeId = 1;
            }

            var storeData = await userRepo.AddEmployee(userEntity);        // save to db
                
            GradeHistory gradeHistoryEntity = new GradeHistory();
            gradeHistoryEntity.AssignedOn = DateOnly.FromDateTime(DateTime.UtcNow);
            gradeHistoryEntity.EmployeeId = storeData.EmployeeId;
            gradeHistoryEntity.GradeId = storeData.CurrentGradeId;

            await gradesHistoryRepo.AddGradeHistory(gradeHistoryEntity);
            
            

            // convert entity to DTO using mapper function
            return await MapEntityResponseToUserResponseDTO(userEntity);

        }

        public async Task<UserResponseDTO> updateEmployeeById(int id, UserRequestDTO userRequestDTO)
        {
            ValidateEmployee(userRequestDTO);
            var empData = await userRepo.GetEmployeeById(id);

            int? currentGrade = empData.CurrentGradeId;
            int? newGrade = userRequestDTO.current_grade_id;

            if(newGrade > currentGrade)
                throw new Exception("Employee cannot be downgrade");

            empData.FirstName = userRequestDTO.first_name;
            empData.LastName = userRequestDTO.last_name;
            empData.PhoneNumber = userRequestDTO.phone_number;
            empData.EmailAddress = userRequestDTO.email_address;
            empData.Role = userRequestDTO.role;
            empData.CurrentGradeId = userRequestDTO.current_grade_id;

            if(newGrade != currentGrade)
            {
                var gradeHistory = new GradeHistory
                {
                    AssignedOn = DateOnly.FromDateTime(DateTime.UtcNow),
                    EmployeeId = empData.EmployeeId,
                    GradeId = empData.CurrentGradeId
                };

                var gradeHistoryByEmployeeId = await gradesHistoryRepo.GetAllGradeHistoryByEmployeeId(gradeHistory.EmployeeId);

                GradeHistory prevGradeHistoryId = gradeHistoryByEmployeeId.First();
                GradeHistory newGradeHistoryId = gradeHistoryByEmployeeId.Last();

                var today = DateTime.Now;

                var prevAssignedOn = prevGradeHistoryId.AssignedOn.GetValueOrDefault().ToDateTime(TimeOnly.MinValue);
                var newAssignedOn = newGradeHistoryId.AssignedOn.GetValueOrDefault().ToDateTime(TimeOnly.MinValue);


                if ((today - prevAssignedOn).TotalDays > (2 * 365))
                {
                    if ((today - newAssignedOn).TotalDays > 365)
                    {
                        gradesHistoryRepo.AddGradeHistory(gradeHistory);
                    }
                    else
                        throw new GradeUpdateRuleViolationException();
                }
                else
                    throw new GradeUpdateRuleViolationException();

            }

            return await MapEntityResponseToUserResponseDTO(empData);
        }
    }
}
