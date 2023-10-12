using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
  public class Patient
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Patient has to have a name. Please type in.")]
        public string Name { get; set; }
        public List<DoctorPatient> JoinEntities { get;}
    }
}