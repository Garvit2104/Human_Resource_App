using Human_Resource_App.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Human_Resource_App.DAL.GradesHistoryRepository
{
    public interface IGradesHistory
    {
        public void AddGradeHistory(GradeHistory gradeHistory);

        List<GradeHistory> GetAllGradeHistoryByEmployeeId(int? id);
        public void DeleteAllGrades(int id);
    }
}
