using Human_Resource_App.Data;
using Human_Resource_App.Models;

namespace Human_Resource_App.DAL.GradesRepository
{
    public class GradesRepositoryClass : IGradesRepo
    {
        private readonly HRDbContext context;

        public GradesRepositoryClass(HRDbContext context)
        {
            this.context = context;
        }

        public List<Grade> GetAllGrades()
        { 
            var data = context.Grades.ToList();
            return data;
        }
    }
}
