using System.Collections.Generic;

namespace Clinic.Models
{
  public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public List<DoctorPatient> JoinEntities { get;}
    }
}