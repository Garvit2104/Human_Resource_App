using Human_Resource_App.DTOs.GradesDTO;
using Human_Resource_App.DAL.GradesRepository;
namespace Human_Resource_App.BLL.GradesService
{
    public class GradesServiceClass : IGradesService
    {
        private readonly IGradesRepo gradesRepo;
                public GradesServiceClass(IGradesRepo gradesRepo)
                {
                    this.gradesRepo = gradesRepo;        
                }
                public List<GradesResponseDTO>  GetAllGrades()
                {
                    var result = gradesRepo.GetAllGrades();

                    List<GradesResponseDTO> ls = new List<GradesResponseDTO>();

                    foreach(var item in result)
                    {
                        GradesResponseDTO gradeResponse = new GradesResponseDTO();
                        gradeResponse.id = item.Id;
                        gradeResponse.name = item.Name;
                       ls.Add(gradeResponse);
                    }
                    return ls;
                }
          
    }
}
