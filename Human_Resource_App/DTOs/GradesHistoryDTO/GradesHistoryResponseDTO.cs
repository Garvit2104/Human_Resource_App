namespace Human_Resource_App.DTOs.GradesHistoryDTO
{
    public class GradesHistoryResponseDTO
    {
        public int Id { get; set; }

        public DateOnly? AssignedOn { get; set; }

        public int? EmployeeId { get; set; }

        public int? GradeId { get; set; }
    }
}
