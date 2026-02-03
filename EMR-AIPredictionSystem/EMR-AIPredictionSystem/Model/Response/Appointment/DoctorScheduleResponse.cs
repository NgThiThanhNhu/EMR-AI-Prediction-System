using EMR_AIPredictionSystem.Model.Entities;

namespace EMR_AIPredictionSystem.Model.Response.Appointment
{
    public class DoctorScheduleResponse
    {
        public string ScheduleId { get; set; } = null!;

        public string DoctorName { get; set; } = null!;

        public DateOnly WorkDate { get; set; }

        public string? Shift { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public int? MaxPatients { get; set; }

        public int? BookedPatients { get; set; }

        public bool? IsAvailable { get; set; }
    }
}
