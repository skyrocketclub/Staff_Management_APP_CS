using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Management_App
{
    internal class Employee
    {
        private string fName;
        private string lName;
        private string sex;
        private string role;
        private double salary;
        public int id;

        private List<string> inbox;

        public Employee(string fName, string lName, string sex, string role, double salary)
        {
            this.fName = fName;
            this.lName = lName;
            this.sex = sex;
            this.role = role;
            this.salary = salary;
        }

        public Employee(string fName, string lName, string sex)
        {
            this.fName = fName;
            this.lName = lName;
            this.sex = sex;
            this.role = "unkwown";
            this.salary = 0.00;
        }

        //defining the getters.......
        public string getfName() { return fName; }
        public string getlName() { return lName; }  
        public string getsex() { return sex;}
        public string getrole() { return role;}
        public double getSalary() { return salary; }


        //defining the setters..............
        public void setfName(string fName) { this.fName = fName; }  
        public void setlName(string lName) { this.lName = lName; }
        public void setsex(string sex) { this.sex = sex; }
        public void setrole(string role) { this.role = role; }
        public void setSalary(double salary) { this.salary =salary; }



        public void addToInbox(string message)
        {

        }

        public void viewInbox()
        {

        }

        public void clearInbox()
        {

        }
        
    }
}
