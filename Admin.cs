using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Management_App
{
    internal class Admin
    {
        public string fName, lName, role,pin;


        Utils util = new Utils();
        public List<Employee> employees;
        public List<Admin> admins;
        public Admin()
        {
             this.employees = new List<Employee>();
            this.admins = new List<Admin>();
        }

        public Admin(string fName, string lName, string role, string pin)
        {
            this.fName = fName;
            this.lName = lName;
            this.role = role;
            this.pin = pin;
        }

        public void AddAdmin(Admin admin)
        {

            string fName, lName;
            int roleChoice;

            Console.WriteLine("Enter your details\n");

            Console.Write("First Name: ");
            fName = Console.ReadLine();
            admin.fName = fName;

            Console.Write("Last Name: ");
            lName = Console.ReadLine();
            admin.lName = lName;

            Console.Write("1 - MD\n2 - HR 1\n3 - HR 2\n4 - CTO\nOption: ");
            roleChoice = int.Parse(Console.ReadLine());

            switch (roleChoice)
            {
                case 1:
                    admin.role = "MD";
                    break;
                case 2:
                    admin.role = "HR 1";
                    break;
                case 3:
                    admin.role = "HR 2";
                    break;
                case 4:
                    admin.role = "CTO";
                    break;
                default:
                    Console.WriteLine("Kindly enter a valid input...");
                    break;
            }
            Console.Write("Enter a 4 digit pin: ");
            admin.pin = Console.ReadLine();

            Console.WriteLine("Admin Successfully Added...");
            util.AddadmintoDB(admin);
        }
      
        public void addEmployee() {
            //Ask for the first name, last name, sex
            string fName, lName, role, sex_;
            int sex;
            int nextID;

            Console.Write("Employee First Name: ");
            fName = Console.ReadLine();
            Console.Write("Employee Last Name: ");
            lName = Console.ReadLine();
            Console.WriteLine("1 - Male\n2 - Female\nOption: ");
            sex = int.Parse(Console.ReadLine());
            if (sex == 1)
            {
                sex_ = "Male";
            }
            else if (sex == 2)
            {
                sex_ = "Female";
            }
            else
            {
                sex_ = "undefined";
            }
            Console.Write("Role: ");
            role = Console.ReadLine();

            nextID = employees.Count() + 1;

            Employee employee = new Employee(fName, lName, sex_,role,0.00);
            employee.id= nextID;

            employees.Add(employee);

        }

        public void removeEmployee()
        {
            int ID; 
            viewAllEmployees();
            Console.WriteLine("\n\n");
            Console.Write("Enter the ID of the Employee you wish to delete: ");
            ID = int.Parse(Console.ReadLine());

            Employee employee = employees.SingleOrDefault(x=>x.id==ID);
            if(employee!= null)
            {
                employees.Remove(employee);
            }
            else
            {
                Console.WriteLine("Invalid ID ...");
            }
        }

        public void viewAllEmployees()
        {
            // Do text formatting here...
            for (int i = 0; i < employees.Count(); i++)
            {
                Console.WriteLine(i + ": ", employees[i].id, employees[i].getfName(), employees[i].getlName());
            }
        }

        public void editEmployee()
        {

        }

        public void messageEmployee()
        {

        }

        public int adminSize() { return admins.Count(); }

        public bool verifyPassword(string password)
        {
            return util.VerifyPassWord(this, password);
        }

        public bool checkAdmin()
        {
            return util.checkAdminInDB(this);
        }
    }
}
