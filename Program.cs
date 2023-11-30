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

            switch (role)
            {
                case UserRole.Student:
                    // Call a method to handle student functionality
                    StudentUI.ShowMenu();
                    break;
                case UserRole.PersonalSupervisor:
                    // Call a method to handle personal supervisor functionality
                    PersonalSupervisorUI.ShowMenu();
                    break;
                case UserRole.SeniorTutor:
                    // Call a method to handle senior tutor functionality
                    SeniorTutorUI.ShowMenu();
                    break;
                case UserRole.None:
                default:
                    Console.WriteLine("Invalid login credentials.");
                    break;
            }

        }
    }
}

