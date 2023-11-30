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
                Console.WriteLine("3. Schedule Meeting with Student");
                Console.WriteLine("4. Logout");
                


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
                        ScheduleMeetingWithStudent();
                        break;
                    case "4":
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
            var meetings = database.GetMeetingsBySupervisorId(supervisorId);

            if (meetings.Any())
            {
                Console.WriteLine("\nScheduled Meetings:");
                foreach (var meeting in meetings)
                {
                    Console.WriteLine($"Meeting ID: {meeting.Id}, Student ID: {meeting.StudentId}, Date: {meeting.MeetingDate}, Subject: {meeting.MeetingSubject}, Notes: {meeting.MeetingNotes}");
                    // Display other relevant meeting information if needed
                }
            }
            else
            {
                Console.WriteLine("No meetings scheduled.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
        public void ScheduleMeetingWithStudent()
        {
            Console.WriteLine("\nSchedule a Meeting with a Student");

            // List students assigned to this supervisor for selection
            var students = database.GetStudentsBySupervisorId(supervisorId);
            if (!students.Any())
            {
                Console.WriteLine("You do not have any assigned students.");
                return;
            }

            Console.WriteLine("Select a Student:");
            foreach (var student in students)
            {
                Console.WriteLine($"Student ID: {student.Id}, Name: {student.FirstName} {student.LastName}");
            }
            Console.Write("Enter the Student ID: ");
            int studentId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the date for the meeting (YYYY-MM-DD): ");
            string meetingDate = Console.ReadLine();
            Console.Write("Enter the subject for the meeting: ");
            string meetingSubject = Console.ReadLine();
            Console.Write("Enter any notes for the meeting: ");
            string meetingNotes = Console.ReadLine();

            // Call the Database method to insert the meeting
            database.InsertMeeting(studentId, supervisorId, meetingDate, meetingSubject, meetingNotes);

            Console.WriteLine("Meeting scheduled successfully.");
        }
    }

}
