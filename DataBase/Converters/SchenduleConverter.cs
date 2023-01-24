using domain.models;
using DataBase.Models;
namespace DataBase.Converters;
public static class ScheduleConverter
{
    public static SchenduleModel ToModel(this Schendule schendule)
    {
        return new SchenduleModel
        {
            Id = schendule.Id,
            DoctorId = schendule.DoctorId,
            StartTime = schendule.StartTime,
            EndTime = schendule.EndTime
        };
    }

    public static Schendule ToDomain(this SchenduleModel schenduleModel)
    {
        return new Schendule(
            schenduleModel.Id,
            schenduleModel.DoctorId,
            schenduleModel.StartTime,
            schenduleModel.EndTime
        );
    }
}