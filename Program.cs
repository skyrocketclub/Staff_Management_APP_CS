using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Management_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            Utils util = new Utils();

            int mode;
            Console.WriteLine("Welcome to the Staff Management App");
            Console.WriteLine("1 - ADMIN");
            Console.WriteLine("2 - EMPLOYEE");
            Console.Write("Option: ");

            mode = int.Parse(Console.ReadLine());
            while (true)
            {
                if (mode == 1)
                {
                    //Already Existing Admin.....
                    Console.WriteLine("Welcome Admin\n");

                    Console.Write("1 - Exisiting Admin\n2 - New Admin\n3 - View all Admins\n4 - Quit\nOption: ");
                    int option = int.Parse(Console.ReadLine());

                    if(option == 1)
                    {
                        //Exisitng Admin
                        Console.Write("Enter your first Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Enter your last Name: ");
                        string lastName = Console.ReadLine();

                        //Check if the Admin is in the 
                        Admin a = admin.admins.FirstOrDefault( x=> x.fName == firstName && x.lName == lastName );
                        if(a != null)
                        {
                            Console.WriteLine("Welcome " + a.fName);
                            Console.WriteLine("What operation would you like to carry out?: ");
                            Console.Write("1 - Add Employee\n2 - View all Employees\n3 - Edit Employee details\n4 - Delete Employee\n5 - Send Message to Employee\nOption: ");
                            option = int.Parse(Console.ReadLine());
                            switch (option)
                            {
                                case 1:
                                    admin.addEmployee();
                                    break;
                                case 2: 
                                    admin.viewAllEmployees();
                                    break;
                                case 3:
                                    admin.editEmployee();
                                    break;
                                case 4:
                                    admin.removeEmployee();
                                    break;


                            }
                  
                        }
                        else
                        {
                            Console.WriteLine("Invalid User...\n");
                        }

                    }else if(option == 2)
                    {
                        //New Admin...
                        admin.AddAdmin(admin);
                    }
                    else if(option == 3)
                    {
                        //View all admins..........
                    }
                    else if(option == 4)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Kindly Enter a Valid Input...");
                    }

                    
                   
                }
                else if (mode == 2){
                    
                
                    break;
                }
                else
                {
                    //An invalid option has been entered...
                    Console.WriteLine("Kindly Enter a valid option ...");
                    Console.Write("Option: ");
                    mode = int.Parse(Console.ReadLine());
                }
            }
        }
    }
}
