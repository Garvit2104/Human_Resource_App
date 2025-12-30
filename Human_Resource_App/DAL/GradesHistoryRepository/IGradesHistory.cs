using Human_Resource_App.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Human_Resource_App.DAL.GradesHistoryRepository
{
    public interface IGradesHistory
    {
        public Task AddGradeHistory(GradeHistory gradeHistory);

        Task<IEnumerable<GradeHistory>> GetAllGradeHistoryByEmployeeId(int? id);
        
    }
}
