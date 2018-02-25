namespace PatientCaseApplication
{
    public class Medic : Person
    {
        public Clinic PlaceOfWork { get; set; }

        public Occupation CurrentOccupation { get; set; }
    }
}
