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
                Console.WriteLine("4. Logout");
                Console.WriteLine("5. Delete a Student");
                Console.WriteLine("6. Delete a Personal Supervisor");
                Console.WriteLine("7. View System Overview");
                Console.WriteLine("8. Add New Student");
                Console.WriteLine("9. Add New Personal Supervisor");

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
                        exitMenu = true;
                        break;
                    case "5":
                        DeleteStudent();
                        break;
                    case "6":
                        DeletePersonalSupervisor();
                        break;
                    case "7":
                        ViewSystemOverview();
                        break;
                    case "8":
                        AddNewStudent();
                        break;
                    case "9":
                        AddNewPersonalSupervisor();
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
            var students = database.GetAllStudents();

            if (students.Any())
            {
                Console.WriteLine("\nList of All Students:");
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}");
                    // Display other relevant student information if needed
                }
            }
            else
            {
                Console.WriteLine("There are no students currently registered.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        private void ViewAllPersonalSupervisors()
        {
            var supervisors = database.GetAllPersonalSupervisors();

            if (supervisors.Any())
            {
                Console.WriteLine("\nList of All Personal Supervisors:");
                foreach (var supervisor in supervisors)
                {
                    Console.WriteLine($"ID: {supervisor.Id}, Name: {supervisor.FirstName} {supervisor.LastName}");
                    // Display other relevant supervisor information if needed
                }
            }
            else
            {
                Console.WriteLine("There are no personal supervisors currently registered.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        private void ViewAllMeetings()
        {
            var meetings = database.GetAllMeetings();

            if (meetings.Any())
            {
                Console.WriteLine("\nList of All Meetings:");
                foreach (var meeting in meetings)
                {
                    Console.WriteLine($"Meeting ID: {meeting.Id}, Student ID: {meeting.StudentId}, Supervisor ID: {meeting.SupervisorId}, Date: {meeting.MeetingDate}, Subject: {meeting.MeetingSubject}, Notes: {meeting.MeetingNotes}");
                    // Display other relevant meeting information if needed
                }
            }
            else
            {
                Console.WriteLine("There are no meetings currently scheduled.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
        private void DeleteStudent()
        {
            Console.Write("Enter the Student ID to delete: ");
            int studentId = Convert.ToInt32(Console.ReadLine());

            database.DeleteStudent(studentId);
            Console.WriteLine("Student deleted successfully.");
        }

        private void DeletePersonalSupervisor()
        {
            Console.Write("Enter the Personal Supervisor ID to delete: ");
            int supervisorId = Convert.ToInt32(Console.ReadLine());

            database.DeletePersonalSupervisor(supervisorId);
            Console.WriteLine("Personal Supervisor deleted successfully.");
        }
        private void ViewSystemOverview()
        {
            Console.WriteLine("\nSystem Overview:");

            // Display all meetings
            var meetings = database.GetAllMeetings();
            Console.WriteLine("\nMeetings:");
            foreach (var meeting in meetings)
            {
                Console.WriteLine($"Meeting ID: {meeting.Id}, Date: {meeting.MeetingDate}, Subject: {meeting.MeetingSubject}");
            }

            // Display all self-reports
            var selfReports = database.GetAllSelfReports();
            Console.WriteLine("\nSelf Reports:");
            foreach (var report in selfReports)
            {
                Console.WriteLine($"Report ID: {report.Id}, Date: {report.ReportDate}, Content: {report.ReportContent}");
            }

            // Add similar sections for other types of data

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
        private void AddNewStudent()
        {
            Console.WriteLine("Enter details for the new student:");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine(); // In a real application, consider using a more secure method to handle password input

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            // Here you should hash the password. In this example, we'll keep it simple.
            string passwordHash = HashPassword(password);

            database.AddNewStudent(username, passwordHash, firstName, lastName);

            Console.WriteLine("New student added successfully.");
        }

        private string HashPassword(string password)
        {
            // Implement password hashing here
            // For simplicity, this example returns the password directly, which is not secure.
            return password;
        }


        private void AddNewPersonalSupervisor()
        {
            Console.WriteLine("Enter details for the new personal supervisor:");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine(); // In a real application, consider using a more secure method to handle password input

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            // Hash the password
            string passwordHash = HashPassword(password);

            database.AddNewPersonalSupervisor(username, passwordHash, firstName, lastName);

            Console.WriteLine("New personal supervisor added successfully.");
        }


    }
}
