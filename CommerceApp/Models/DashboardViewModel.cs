namespace CommerceApp.Models
{
    public class DashboardViewModel
    {
        public int TotalPatients { get; set; }
        public int PatientsNeedingCheckup { get; set; }
        public double AveragePatientAge { get; set; }
        public List<GenderDistributionViewModel> GenderDistribution { get; set; }
        public List<AgeGroupViewModel> AgeGroups { get; set; }
    }
}
