using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Monitor_Engagement
{
    public class PersonalSupervisorUI
    {
        private Database database;
        private int supervisorId; // The supervisor's ID

        public PersonalSupervisorUI(Database db, int supervisorId)
        {
            this.database = db;
            this.supervisorId = supervisorId;
        }

        public void ShowMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.Clear();
                Console.WriteLine("Personal Supervisor Menu:");
                Console.WriteLine("1. View My Students");
                Console.WriteLine("2. View Scheduled Meetings");
                Console.WriteLine("3. Logout");

                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewMyStudents();
                        break;
                    case "2":
                        ViewScheduledMeetings();
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

        private void ViewMyStudents()
        {
            var students = database.GetStudentsBySupervisorId(supervisorId);

            if (students.Any())
            {
                Console.WriteLine("\nStudents Assigned to You:");
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}");
                    // Display other relevant student information if needed
                }
            }
            else
            {
                Console.WriteLine("No students assigned to you.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        private void ViewScheduledMeetings()
        {
            // Implementation to view meetings scheduled with students
            // Fetch meetings from the database and display them
        }
    }

}
