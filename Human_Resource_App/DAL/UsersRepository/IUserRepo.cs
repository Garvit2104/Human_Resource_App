using Human_Resource_App.Models;

namespace Human_Resource_App.DAL.UsersRepository
{
    public interface IUserRepo
    {
        Task<User> AddEmployee(User user);

        public Task<IEnumerable<User>> GetAllEmployee();

        Task<User> GetEmployeeById(int employeeId);

        Task<bool> updateEmployeeById(User user);

        Task<Boolean> DeleteEmployeeById(int id);
    }
}
