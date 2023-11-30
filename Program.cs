using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Security.Cryptography.X509Certificates;


namespace Staff_Monitor_Engagement
{
    public enum UserRole
    {
        Student,
        PersonalSupervisor,
        SeniorTutor,
        None // Used for default or unidentified roles
    }
    public class Program
    {
        static void Main(string[] args)
        {


            string databaseFilePath = "MyDatabase.sqlite";
            Database myDatabase = new Database(databaseFilePath);

            // Optional: Uncomment to seed the database with initial data
            //DataSeeder seeder = new DataSeeder(myDatabase);
            //seeder.InsertSampleData();
            
            Console.WriteLine("Welcome to the Staff Monitor Engagement System");
            

            // Add similar code for other entities like PersonalSupervisor, SeniorTutor, etc.

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine(); // In a real application, consider using a more secure method to handle password input

            UserRole role = myDatabase.Login(username, password);
            int studentId = myDatabase.GetStudentIdByUsername(username);

            switch (role)
            {
                case UserRole.Student:
                    // Use the previously declared studentId variable
                    if (studentId != -1)
                    {
                        StudentUI studentUI = new StudentUI(myDatabase, studentId);
                        studentUI.ShowMenu();
                    }
                    else
                    {
                        // Handle the case where the student ID couldn't be found
                        Console.WriteLine("Student ID not found.");
                    }
                    break;
                case UserRole.PersonalSupervisor:
                    int supervisorId = myDatabase.GetSupervisorIdByUsername(username);
                    if (supervisorId != -1)
                    {
                        PersonalSupervisorUI supervisorUI = new PersonalSupervisorUI(myDatabase, supervisorId);
                        supervisorUI.ShowMenu();
                    }
                    else
                    {
                        Console.WriteLine("Supervisor ID not found.");
                    }
                    break;
                case UserRole.SeniorTutor:
                    // Call a method to handle senior tutor functionality
                    // SeniorTutorUI.ShowMenu(); // Make sure you implement this
                    break;
                case UserRole.None:
                default:
                    Console.WriteLine("Invalid login credentials.");
                    break;
            }

        }
    }
}

