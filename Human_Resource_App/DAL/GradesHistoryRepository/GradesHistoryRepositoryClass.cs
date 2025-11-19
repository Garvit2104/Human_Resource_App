using Human_Resource_App.Data;
using Human_Resource_App.Models;

namespace Human_Resource_App.DAL.GradesHistoryRepository
{
    public class GradesHistoryRepositoryClass : IGradesHistory
    {
        private readonly HRDbContext context;

        public GradesHistoryRepositoryClass(HRDbContext context)
        {
            this.context = context;
        }
        public void saveGradeHistory(GradeHistory gradeHistory)
        {
            context.GradeHistories.Add(gradeHistory);
            context.SaveChanges();
        }

        public void DeleteAllGrades(int id)
        {
            var grades = context.GradeHistories.Where(g => g.EmployeeId == id).ToList();
            context.GradeHistories.RemoveRange(grades);
            context.SaveChanges();
        }

    }
}
