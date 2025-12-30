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

        public async Task<User> AddEmployee(User user)
        {
            if (user.Role == "TravelDeskExec")
                user.CurrentGradeId = 1;

            var savedUser = await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();
            return savedUser.Entity;
        }

        public async Task<IEnumerable<User>> GetAllEmployee()
        {
            return await this.context.Users.Include(u=> u.CurrentGrade).AsNoTracking().ToListAsync();

        }

        public async Task<User> GetEmployeeById(int employeeId)
        {
            var empData = await context.Users.Include(u=> u.CurrentGrade).FirstOrDefaultAsync(User => User.EmployeeId == employeeId);

            if(empData == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }

            return empData;
        }

        public async Task<bool> updateEmployeeById(User user)
        {
            if (user != null)
            {
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Boolean> DeleteEmployeeById(int id)
        {
           var employee = await context.Users.FirstOrDefaultAsync(u => u.EmployeeId == id);

            if(employee != null)
            {
                var result = context.GradeHistories.Where(u => u.EmployeeId == id);

                context.GradeHistories.RemoveRange(result);
                context.Users.Remove(employee);
                await context.SaveChangesAsync();
                return true;

            }
            return false;

        }
    }
}
