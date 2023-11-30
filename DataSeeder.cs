using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Monitor_Engagement
{
    public class DataSeeder
    {
        private Database database;

        public DataSeeder(Database db)
        {
            database = db;
        }

        public void InsertSampleData()
        {
            // Insert students
            database.InsertStudent("student1", "password1", "John", "Doe");
            database.InsertStudent("student2", "password2", "Jane", "Smith");

            // Insert personal supervisors
            database.InsertPersonalSupervisor("supervisor1", "password3", "Alice", "Johnson");
            database.InsertPersonalSupervisor("supervisor2", "password4", "Bob", "Brown");

            // Insert senior tutors
            database.InsertSeniorTutor("seniortutor1", "password5", "Dr.", "Adams");
            database.InsertSeniorTutor("seniortutor2", "password6", "Dr.", "Miller");

            database.InsertRelationship(1, 1, "2023-01-01", null);
            database.InsertRelationship(2, 2, "2023-01-01", null);

            // Insert self reports
            database.InsertSelfReport(1, "2023-01-02", "Feeling good about the progress.");
            database.InsertSelfReport(2, "2023-01-03", "Need help with the latest assignment.");

            // Insert meetings
            database.InsertMeeting(1, 1, "2023-01-05", "Monthly Check-In", "Discuss progress and upcoming projects.");
            database.InsertMeeting(2, 2, "2023-01-06", "Assignment Help", "Review the requirements for the assignment.");

            
        }

        private string HashPassword(string password)
        {
            // Placeholder for password hashing
            // In a real application, use a strong hashing algorithm
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

}
