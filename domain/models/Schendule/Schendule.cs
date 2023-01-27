namespace domain.models;

public class Schendule
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Schendule(int id, int doctorId, DateTime startTime, DateTime endTime)
    {
        Id = id;
        DoctorId = doctorId;
        StartTime = startTime;
        EndTime = endTime;
    }
}
