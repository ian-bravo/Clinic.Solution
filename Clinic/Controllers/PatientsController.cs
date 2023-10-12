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
      return View(_db.Patients.ToList());
    }

    public ActionResult Create()
    {
      // PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patient patient)
    {
      if (!ModelState.IsValid)
      {
        return View(patient);
      }
      
      {
        _db.Patients.Add(patient);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    
  }
}