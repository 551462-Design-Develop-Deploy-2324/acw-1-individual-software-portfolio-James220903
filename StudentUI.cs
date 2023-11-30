using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Monitor_Engagement
{
    public class StudentUI
    {
        public static void ShowMenu()
        {
            Console.WriteLine("Student Menu:");
            Console.WriteLine("1. View Self Reports");
            // Add more options as needed

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    ViewSelfReports();
                    break;
                    // Add more cases for other options
            }
        }

        private static void ViewSelfReports()
        {
            // Implement functionality to view self-reports
        }
    }
}
