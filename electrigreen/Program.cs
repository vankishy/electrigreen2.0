using electrigreen;
class Program
{
    private static void Main(string[] args)
    {
        Register<Account> register = new Register<Account>();

        Console.Write("Nama: ");
        string nama = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        Account johnAccount = new Account(nama, email, password);

        Console.Write("Konfirmasi Password: ");
        string passConfirm = Console.ReadLine();
        register.RegisterNewAccount(johnAccount, password, passConfirm);

        Console.WriteLine("\nAkun Terdaftar:");
        register.DisplayAccounts();
    }
}