using Human_Resource_App.BLL.GradesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Human_Resource_App.DTOs;
using Human_Resource_App.DTOs.GradesDTO;

namespace Human_Resource_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradesService gradesService;
        public GradesController(IGradesService gradesService)
        {
            this.gradesService = gradesService;
        }

        [HttpGet("grades")]

        public List<GradesResponseDTO> GetAllGrades()
        {
            var grades = gradesService.GetAllGrades();
            return grades;

        }
    }
}
