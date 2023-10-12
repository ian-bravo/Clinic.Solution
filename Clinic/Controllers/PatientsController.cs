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

    public ActionResult Details(int id)
    {
      Patient thisPatient = _db.Patients.Include(p => p.JoinEntities)
                                        .ThenInclude(together => together.Doctor)
                                        .FirstOrDefault(p => p.PatientId == id);
      return View(thisPatient);
    }
    
    public ActionResult Edit(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(p => p.PatientId == id);
      return View(thisPatient);
    }

    // [HttpPost]
    // public ActionResult Edit(Patient patient, string action, int id)
    // {
    //     if (action == "edit")
    //     {
    //         if (!ModelState.IsValid)
    //         {
    //             return View(patient);
    //         }

    //         // Fetch the patient with the correct ID from the database
    //         var existingPatient = _db.Patients.FirstOrDefault(p => p.PatientId == id);

    //         if (existingPatient != null)
    //         {
    //             // Update patient properties based on the edited data
    //             existingPatient.Name = patient.Name; // Update other properties as needed

    //             _db.Patients.Update(existingPatient);
    //             _db.SaveChanges();
    //         }
    //     }
    //     else if (action == "delete")
    //     {
    //         // Fetch the patient with the correct ID from the database
    //         var patientToDelete = _db.Patients.FirstOrDefault(p => p.PatientId == id);

    //         if (patientToDelete != null)
    //         {
    //             _db.Patients.Remove(patientToDelete);
    //             _db.SaveChanges();
    //         }
    //     }

    //     return RedirectToAction("Index");
    //   }

    [HttpPost]
    public ActionResult Edit(Patient patient)
    {
      _db.Patients.Update(patient);
      _db.SaveChanges();
      return RedirectToAction ("Index");
    }
    

    public ActionResult Delete(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(p => p.PatientId == id);
      return View(thisPatient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(p => p.PatientId == id);
      _db.Patients.Remove(thisPatient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddDoctor(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(p => p.PatientId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult AddDoctor(Patient patient, int doctorId)
    {
      #nullable enable
      DoctorPatient? joinEntity = _db.DoctorPatients.FirstOrDefault(join => (join.DoctorId == doctorId && join.PatientId == patient.PatientId));
      #nullable disable
      if (joinEntity == null && doctorId != 0)
      {
        _db.DoctorPatients.Add(new DoctorPatient() {
          DoctorId = doctorId, PatientId = patient.PatientId
        });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = patient.PatientId });
    }
  }
}