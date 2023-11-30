using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Monitor_Engagement
{
    public class StudentUI
    {
        private Database database;
        private int studentId;

        public StudentUI(Database db, int studentId)
        {
            this.database = db;
            this.studentId = studentId; // Initialize the field with the value passed to the constructor
        }

        public void ShowMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.Clear();
                Console.WriteLine("Student Menu:");
                Console.WriteLine("1. View Self Reports");
                Console.WriteLine("2. Schedule Meeting with Supervisor");
                Console.WriteLine("3. Logout");

                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewSelfReports();
                        break;
                    case "2":
                        ScheduleMeeting();
                        break;
                    case "3":
                        exitMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (!exitMenu)
                {
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }

        private void ViewSelfReports()
        {
            var selfReports = database.GetAllSelfReportsForStudent(studentId);

            if (selfReports.Any())
            {
                Console.WriteLine("\nYour Self Reports:");
                foreach (var report in selfReports)
                {
                    Console.WriteLine($"Date: {report.ReportDate}, Content: {report.ReportContent}");
                }
            }
            else
            {
                Console.WriteLine("\nNo self-reports found.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }


        private void ScheduleMeeting()
        {
            // Implementation to schedule a meeting with a supervisor
            // This could involve selecting a supervisor, choosing a date and time, etc.
        }
    }
}
