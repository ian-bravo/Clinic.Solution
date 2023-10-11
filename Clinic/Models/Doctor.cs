using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
  public class Doctor
  {
    public int DoctorId { get; set; }
    [Required(ErrorMessage = "Doctor has to have a Name. Please type in.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Doctor has to have a specialty. Please type in.")]
    public string Speciality { get; set; }
    public List<DoctorPatient> JoinEntities { get;}
  }
}