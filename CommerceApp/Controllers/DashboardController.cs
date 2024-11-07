using CommerceApp.Data;
using CommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CommerceApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = new DashboardViewModel
            {
                TotalPatients = await _context.Patients.CountAsync(),
                PatientsNeedingCheckup = await _context.Patients
                    .Where(p => p.IsCheckupOverdue)
                    .CountAsync(),
                AveragePatientAge = await _context.Patients
                    .AverageAsync(p => p.Age),
                GenderDistribution = await _context.Patients
                    .GroupBy(p => p.Gender)
                    .Select(g => new GenderDistributionViewModel
                    {
                        Gender = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync(),
                AgeGroups = await GetAgeGroupDistribution()
            };

            return View(dashboard);
        }

        private async Task<List<AgeGroupViewModel>> GetAgeGroupDistribution()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients
                .GroupBy(p => GetAgeGroup(p.Age))
                .Select(g => new AgeGroupViewModel
                {
                    AgeGroup = g.Key,
                    Count = g.Count()
                })
                .OrderBy(g => g.AgeGroup)
                .ToList();
        }

        private string GetAgeGroup(int age)
        {
            if (age < 18) return "Under 18";
            if (age < 30) return "18-29";
            if (age < 50) return "30-49";
            if (age < 70) return "50-69";
            return "70+";
        }

        public async Task<IActionResult> PatientAnalytics()
        {
            var checkupData = await _context.Patients
                .GroupBy(p => p.LastCheckupDate.Date)
                .Select(g => new CheckupDataViewModel
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(d => d.Date)
                .Take(30)
                .ToListAsync();

            return View(checkupData);
        }

        public async Task<IActionResult> ExportPatientData()
        {
            var patients = await _context.Patients.ToListAsync();
            var csv = new StringBuilder();

            // Add headers
            csv.AppendLine("Name,Age,Gender,Email,LastCheckup,IsCheckupOverdue");

            // Add data
            foreach (var patient in patients)
            {
                csv.AppendLine($"{patient.Name},{patient.Age},{patient.Gender}," +
                              $"{patient.Email},{patient.LastCheckupDate:yyyy-MM-dd}," +
                              $"{patient.IsCheckupOverdue}");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "patients_data.csv");
        }

        public async Task<IActionResult> SendCheckupReminders()
        {
            var overduePatients = await _context.Patients
                .Where(p => p.IsCheckupOverdue)
                .ToListAsync();

            foreach (var patient in overduePatients)
            {
                // Here you would implement actual email sending
                // For now, we'll just update a reminder sent date
                patient.LastReminderSent = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> HealthMetrics()
        {
            var metrics = new HealthMetricsViewModel
            {
                CheckupsByMonth = await GetCheckupsByMonth(),
                OverdueCheckupsByAgeGroup = await GetOverdueCheckupsByAgeGroup()
            };

            return View(metrics);
        }

        private async Task<Dictionary<string, int>> GetCheckupsByMonth()
        {
            return await _context.Patients
                .GroupBy(p => p.LastCheckupDate.ToString("MMMM yyyy"))
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Month, x => x.Count);
        }

        private async Task<Dictionary<string, int>> GetOverdueCheckupsByAgeGroup()
        {
            var overduePatients = await _context.Patients
                .Where(p => p.IsCheckupOverdue)
                .ToListAsync();

            return overduePatients
                .GroupBy(p => GetAgeGroup(p.Age))
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
