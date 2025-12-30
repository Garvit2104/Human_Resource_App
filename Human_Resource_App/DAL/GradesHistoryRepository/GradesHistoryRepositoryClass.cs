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
        public async Task AddGradeHistory(GradeHistory gradeHistory)
        {
            context.GradeHistories.Add(gradeHistory);
            await context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<GradeHistory>> GetAllGradeHistoryByEmployeeId(int? id)
        {
            var result = this.context.GradeHistories.Where(gh => gh.EmployeeId == id).AsEnumerable();

            return await Task.FromResult(result);
           
        }
        

    }
}
