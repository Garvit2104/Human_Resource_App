using Human_Resource_App.Data;
using Human_Resource_App.Models;
using System.Net.Sockets;

namespace Human_Resource_App.DAL.GradesHistoryRepository
{
    public class GradesHistoryRepositoryClass : IGradesHistory
    {
        private readonly HRDbContext context;

        public GradesHistoryRepositoryClass(HRDbContext context)
        {
            this.context = context;
        }
        public void AddGradeHistory(GradeHistory gradeHistory)
        {
            context.GradeHistories.Add(gradeHistory);
            context.SaveChanges();
        }
        
        public List<GradeHistory> GetAllGradeHistoryByEmployeeId(int? id)
        {
            var result = context.GradeHistories.Where(gh => gh.EmployeeId == id).ToList();

            return result;
           
        }
        public void DeleteAllGrades(int id)
        {
            var grades = context.GradeHistories.Where(g => g.EmployeeId == id).ToList();
            context.GradeHistories.RemoveRange(grades);
            context.SaveChanges();
        }

    }
}
