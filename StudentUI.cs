﻿using System;
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
                Console.WriteLine("4. Create Self Report");
                Console.WriteLine("5. View Scheduled Meetings");



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
                    case "4":
                        CreateSelfReport();
                        break;
                    case "5":
                        ViewMeetings();
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


        public void ScheduleMeeting()
        {
            Console.WriteLine("\nSchedule a Meeting with Your Supervisor");

            // Assuming each student is assigned to one personal supervisor
            var supervisorId = GetSupervisorIdForStudent(studentId);
            if (supervisorId == -1)
            {
                Console.WriteLine("No supervisor assigned to you.");
                return;
            }

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
        private int GetSupervisorIdForStudent(int studentId)
        {
            // Call the method in the Database class to get the supervisor ID
            return database.GetSupervisorIdByStudentId(studentId);
        }
        public void CreateSelfReport()
        {
            Console.WriteLine("\nCreate a New Self Report");

            Console.Write("Enter the report content: ");
            string reportContent = Console.ReadLine();

            // Get current date as the report date
            string reportDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Call the Database method to insert the self-report
            database.InsertSelfReport(studentId, reportDate, reportContent);

            Console.WriteLine("Self-report submitted successfully.");
        }
        public void ViewMeetings()
        {
            Console.WriteLine("\nYour Scheduled Meetings:");

            var meetings = database.GetAllMeetingsForStudent(studentId);

            if (meetings.Any())
            {
                foreach (var meeting in meetings)
                {
                    Console.WriteLine($"Date: {meeting.MeetingDate}, Subject: {meeting.MeetingSubject}, Notes: {meeting.MeetingNotes}");
                }
            }
            else
            {
                Console.WriteLine("No meetings scheduled.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }


    }
}
