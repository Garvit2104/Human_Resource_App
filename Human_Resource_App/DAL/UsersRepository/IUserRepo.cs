using Human_Resource_App.Models;

namespace Human_Resource_App.DAL.UsersRepository
{
    public interface IUserRepo
    {
        User AddEmployee(User user);

        List<User> GetAllEmployee();

        User GetEmployeeById(int employeeId);

        bool updateEmployeeById(User user);
    }
}
