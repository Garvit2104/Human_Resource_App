using Human_Resource_App.DTOs.GradesDTO;

namespace Human_Resource_App.BLL.GradesService
{
    public interface IGradesService
    {
        public  Task<IEnumerable<GradesResponseDTO>> GetAllGrades();
    }
}
