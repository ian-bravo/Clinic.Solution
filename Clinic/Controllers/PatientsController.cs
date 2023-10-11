using Microsoft.AspNetCore.Mvc;
using Clinic.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic.Controllers
{
  public class PatientsController : Controller
  {
    private readonly ClinicContext _db;

    public PatientsController(ClinicContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Patient> model= _db.Patients.Include(i => i.Doctor).ToList();
      return View(model);
    }    

    public ActionResult Create()
    {
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patient patient)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
        return View(patient);
      }
      else
      {
        _db.Patient.Add(patient);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
  }
}