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
  }
}