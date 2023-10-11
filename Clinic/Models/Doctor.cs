using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
  public class Doctor
  {
    public int DoctorId { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public List<DoctorPatient> JoinEntities { get;}
  }
}