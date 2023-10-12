using Microsoft.AspNetCore.Mvc;
using Clinic.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic.Controllers
{
  public class DoctorsController : Controller
  {
    private readonly ClinicContext _db;

    public DoctorsController(ClinicContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Doctors.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Doctor doctor)
    {
      if (!ModelState.IsValid)
      {
        return View(doctor);
      }
      else
      {
        _db.Doctors.Add(doctor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Doctor thisDoctor = _db.Doctors

                .Include(doc => doc.JoinEntities)
                .ThenInclude(j => j.Patient)
                .FirstOrDefault(doc => doc.DoctorId == id);
      return View(thisDoctor);
    }

    public ActionResult Edit(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(doc => doc.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult Edit(Doctor doctor)
    {
      _db.Doctors.Update(doctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(doc => doc.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(doc => doc.DoctorId == id);
      _db.Doctors.Remove(thisDoctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPatient(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(d => d.DoctorId == id);
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult AddPatient(Doctor doctor, int patientId)
    {
      #nullable enable
      DoctorPatient? joinEntity = _db.DoctorPatients.FirstOrDefault(join => (join.PatientId == patientId && join.DoctorId == doctor.DoctorId));
      #nullable disable
      if (joinEntity == null && patientId != 0)
      {
        _db.DoctorPatients.Add(new DoctorPatient() {
          PatientId = patientId, DoctorId = doctor.DoctorId
        });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = doctor.DoctorId });
    }
  }
}