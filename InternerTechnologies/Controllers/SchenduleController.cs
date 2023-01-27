using InternerTechnologies.View;
using domain.models;
using domain.services;
using Microsoft.AspNetCore.Mvc;

namespace InternerTechnologies.Controllers;

[ApiController]
[Route("schendule")]
public class SchenduleController: ControllerBase {
    private readonly SchenduleService _service;

    public SchenduleController(SchenduleService schenduleService) {
        _service = schenduleService;
    }

    [HttpGet("get_by_doctor")]
    public ActionResult<List<SchenduleView>> GetByDoctor(DoctorView doctor, DateOnly date) {
        var domainDoc = new Doctor(doctor.DoctorId, doctor.Name, doctor.Specialization);

        var res = _service.GetByDoctor(domainDoc, date);

        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        List<SchenduleView> schenduleViews = new List<SchenduleView>();

        foreach (var schendule in res.Value) {
            schenduleViews.Add(new SchenduleView {
                Id = schendule.Id,
                DoctorId = schendule.DoctorId,
                EndTime = schendule.EndTime,
                StartTime = schendule.StartTime
            });
        }

        return Ok(schenduleViews);
    }

    [HttpPost("add")]
    public ActionResult<SchenduleView> AddSchendule(SchenduleView schenduleView) {
        var schendule = new Schendule(
            schenduleView.Id,
            schenduleView.DoctorId,
            schenduleView.StartTime,
            schenduleView.EndTime
        );

        var res = _service.Add(schendule);
        
        if(!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(schenduleView);
    }

    [HttpPost("update")]
    public ActionResult<SchenduleView> UpdateSchendule(SchenduleView schenduleView) {
        var schendule = new Schendule(
            schenduleView.Id,
            schenduleView.DoctorId,
            schenduleView.StartTime,
            schenduleView.EndTime
        );

        var res = _service.Update(schendule);
        
        if(!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(schenduleView);
    }
    
    [HttpDelete("delete")]
    public ActionResult<SchenduleView> DeleteSchendule(SchenduleView schenduleView) {
        var schendule = new Schendule(
            schenduleView.Id,
            schenduleView.DoctorId,
            schenduleView.StartTime,
            schenduleView.EndTime
        );

        var res = _service.Delete(schendule);
        
        if(!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(schenduleView);
    }
}