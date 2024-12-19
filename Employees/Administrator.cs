namespace Project_1_OOP_Wojciech_Dabrowski.Employees
{
    public class Administrator : Employee
    {
        public Administrator(string name, string surname, int pesel, string username, string password, Role userRole)
            : base(name, surname, pesel, username, password, userRole)
        {
        }
    }
}
