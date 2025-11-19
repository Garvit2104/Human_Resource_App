using Human_Resource_App.Data;
using Human_Resource_App.DTOs.UsersDTO;
using Human_Resource_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_App.DAL.UsersRepository
{
    public class UserRepositoryClass : IUserRepo
    {
        private readonly HRDbContext context;
        public UserRepositoryClass(HRDbContext context)
        {
            this.context = context;
        }

        public User AddEmployee(User user)
        {
            if (user.Role == "TravelDeskExec")
                user.CurrentGradeId = 1;

                    var savedUser = context.Users.Add(user).Entity;
                   context.SaveChanges();
                    return savedUser;
        }

        public List<User> GetAllEmployee()
        {
            var result = context.Users.ToList();
            return result;
        }

        public User GetEmployeeById(int employeeId)
        {
            var empData = context.Users.FirstOrDefault(User => User.EmployeeId == employeeId);

            if(empData == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }

            return empData;
        }

        public bool updateEmployeeById(User user)
        {
            if (user != null)
            {
                context.Entry(user).State = EntityState.Modified;
                 context.SaveChanges();
                return true;
            }
            return false;


        }
    }
}
