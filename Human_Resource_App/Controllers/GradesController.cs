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

        public async Task<IActionResult> GetAllGrades()
        {
            try
            {
                var grades = await gradesService.GetAllGrades();
                return Ok(grades);

            }
            catch(Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }

        }
    }
}
