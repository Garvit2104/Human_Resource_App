using Human_Resource_App.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Human_Resource_App.DAL.GradesHistoryRepository
{
    public interface IGradesHistory
    {
        public void saveGradeHistory(GradeHistory gradeHistory);

        public void DeleteAllGrades(int id);
    }
}
