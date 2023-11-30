using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Monitor_Engagement
{
    public class SeniorTutorUI
    {
        private Database database;

        public SeniorTutorUI(Database db)
        {
            database = db;
        }

        public void ShowMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.Clear();
                Console.WriteLine("Senior Tutor Menu:");
                Console.WriteLine("1. View All Students");
                Console.WriteLine("2. View All Personal Supervisors");
                Console.WriteLine("3. View All Meetings");
                Console.WriteLine("4. Generate Reports");
                Console.WriteLine("5. Logout");

                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewAllStudents();
                        break;
                    case "2":
                        ViewAllPersonalSupervisors();
                        break;
                    case "3":
                        ViewAllMeetings();
                        break;
                    case "4":
                        GenerateReports();
                        break;
                    case "5":
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

        private void ViewAllStudents()
        {
            // Implement to display all students
        }

        private void ViewAllPersonalSupervisors()
        {
            // Implement to display all personal supervisors
        }

        private void ViewAllMeetings()
        {
            // Implement to display all meetings
        }

        private void GenerateReports()
        {
            // Implement to generate and display various reports
        }
    }
}
