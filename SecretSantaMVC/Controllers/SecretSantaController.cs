using Microsoft.AspNetCore.Mvc;
using SecretSantaMVC.Models;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using CsvHelper;


namespace SecretSantaMVC.Controllers
{
    public class SecretSantaController : Controller
    {
        private static List<Employee> _employees = new();
        private static List<SecretSantaAssignment> _previousAssignments = new();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadEmployeeCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Please upload a valid CSV file.";
                return View("Index");
            }

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                _employees = csv.GetRecords<Employee>().ToList();
                ViewBag.Message = "Employee file uploaded successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error processing file: " + ex.Message;
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult UploadPreviousAssignmentsCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Please upload a valid previous assignments CSV.";
                return View("Index");
            }

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                _previousAssignments = csv.GetRecords<SecretSantaAssignment>().ToList();
                ViewBag.Message = "Previous assignments file uploaded successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error processing file: " + ex.Message;
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult GenerateAssignments()
        {
            if (_employees.Count < 2)
            {
                ViewBag.Message = "At least two employees are needed.";
                return View("Index");
            }

            var remainingChildren = new List<Employee>(_employees);
            var assignments = new List<SecretSantaAssignment>();
            var random = new Random();

            foreach (var giver in _employees)
            {
                var possibleChildren = remainingChildren
                    .Where(c => c.Employee_EmailID != giver.Employee_EmailID &&
                                !_previousAssignments.Any(pa => pa.Employee_EmailID == giver.Employee_EmailID &&
                                                                pa.Secret_Child_EmailID == c.Employee_EmailID))
                    .ToList();

                if (!possibleChildren.Any())
                {
                    ViewBag.Message = "Failed to generate valid assignments. Try again.";
                    return View("Index");
                }

                var chosenChild = possibleChildren[random.Next(possibleChildren.Count)];
                assignments.Add(new SecretSantaAssignment
                {
                    Employee_Name = giver.Employee_Name,
                    Employee_EmailID = giver.Employee_EmailID,
                    Secret_Child_Name = chosenChild.Employee_Name,
                    Secret_Child_EmailID = chosenChild.Employee_EmailID
                });

                remainingChildren.Remove(chosenChild);
            }

            var outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "SecretSantaAssignments.csv");
            using var writer = new StreamWriter(outputFilePath, false, Encoding.UTF8);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(assignments);

            ViewBag.Message = "Assignments generated successfully. Download available.";
            ViewBag.FilePath = "/SecretSantaAssignments.csv";
            return View("Index");
        }
    }
}
