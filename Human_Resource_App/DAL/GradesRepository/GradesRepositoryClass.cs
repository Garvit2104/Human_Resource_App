using Human_Resource_App.Data;
using Human_Resource_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_App.DAL.GradesRepository
{
    public class GradesRepositoryClass : IGradesRepo
    {
        private readonly HRDbContext context;

        public GradesRepositoryClass(HRDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Grade>> GetAllGrades()
        { 
            var data =  this.context.Grades.AsNoTracking().AsEnumerable();
            return await Task.FromResult(data);
        }

        public async Task<Grade> GetGradeById(int id)
        {
            var data =  await context.Grades.FirstOrDefaultAsync(g => g.Id == id);

            if (data == null)
                throw new KeyNotFoundException($"Grade with id {id} not found.");

            return data;
        }
    }
}
