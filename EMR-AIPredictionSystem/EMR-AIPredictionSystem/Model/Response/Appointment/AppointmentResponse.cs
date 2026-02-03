using EMR_AIPredictionSystem.Model.Entities;

namespace EMR_AIPredictionSystem.Model.Response.Appointment
{
    public class AppointmentResponse
    {
        public string AppointmentId { get; set; } = null!;

        public DoctorScheduleResponse ScheduleDetail { get; set; } = null!;

        public string? AppointmentType { get; set; }

        public string? AppointmentStatus { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual DoctorSchedule Schedule { get; set; } = null!;
    }
}
