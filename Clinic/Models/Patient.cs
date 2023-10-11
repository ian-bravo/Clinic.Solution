using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
  public class Patient
    {
        [Range(1, int.MaxValue, ErrorMessage = "Must select a doctor first")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Patient has to have a Name. Please type in.")]
        public string Name { get; set; }
        public Doctor Doctor { get; set; }
        public List<DoctorPatient> JoinEntities { get;}
    }
}