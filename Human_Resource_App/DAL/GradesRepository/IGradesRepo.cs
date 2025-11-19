using Human_Resource_App.Models;

namespace Human_Resource_App.DAL.GradesRepository
{
    public interface IGradesRepo
    {
        List<Grade> GetAllGrades();

        Grade GetGradeById(int id);
    }
}
