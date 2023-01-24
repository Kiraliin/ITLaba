using System.Text.Json.Serialization;
using domain.models;

namespace InternerTechnologies.View;
public class DoctorView
{
    [JsonPropertyName("id")]
    public int DoctorId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("specialization")]
    public Specialization Specialization { get; set; }
}