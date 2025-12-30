using Human_Resource_App.Models;

namespace Human_Resource_App.DAL.GradesRepository
{
    public interface IGradesRepo
    {
        public Task<IEnumerable<Grade>> GetAllGrades();

        public Task<Grade> GetGradeById(int id);
    }
}
