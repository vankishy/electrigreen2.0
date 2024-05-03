using electrigreen;
using System.Net;
using System.Text;

interface IMenuState
{
    public void HandleOutput(MenuContext context);
}

class MenuContext
{
    public IMenuState currentState;
    public MenuContext(IMenuState currentstate)
    {
        this.currentState = currentstate;
    }

    public void ChangeState(IMenuState newState)
    {
        currentState = newState;
        currentState.HandleOutput(this);
    }

    public void HandleOutput()
    {
        currentState.HandleOutput(this);
    }
}

class AuthScreen : IMenuState
{
    public void HandleOutput(MenuContext context)
    {
        try
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("=            Electrigreen Console App            =");
            Console.WriteLine("=             Created by Kelompok 8              =");
            Console.WriteLine("=  Tugas Besar CLO 2 Konstruksi Perangkat Lunak  =");
            Console.WriteLine("==================================================");
            Console.WriteLine("=                  Authentikasi                  =");
            Console.WriteLine("= 1. Login                                       =");
            Console.WriteLine("= 2. Register                                    =");
            Console.WriteLine("= 0. Exit                                        =");
            Console.WriteLine("==================================================");
            Console.Write("Masukkan Menu: ");

            int inputCmd = int.Parse(Console.ReadLine());

            if (inputCmd == 0)
            {
                context.ChangeState(new ExitMenuState());
            } 
            else if (inputCmd == 2)
            {
                context.ChangeState(new Register());
            }
        }
        catch (Exception e) { }
    }
}

class InitialMenuState : IMenuState
{
    public void HandleOutput(MenuContext context)
    {
        try
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("=            Electrigreen Console App            =");
            Console.WriteLine("=             Created by Kelompok 8              =");
            Console.WriteLine("=  Tugas Besar CLO 2 Konstruksi Perangkat Lunak  =");
            Console.WriteLine("==================================================");
            Console.WriteLine("=                      Menu                      =");
            Console.WriteLine("= 1. Tambahkan perangkat elektronik              =");
            Console.WriteLine("= 2. Tampilkan perangkat elektronik terhubung    =");
            Console.WriteLine("= 3. Tampilkan achievement                       =");
            Console.WriteLine("= 4. Buka Artikel                                =");
            Console.WriteLine("= 5. Pengaturan Akun                             =");
            Console.WriteLine("= 0. Exit                                        =");
            Console.WriteLine("==================================================");
            Console.Write("Masukkan Menu: ");

            int inputCmd = int.Parse(Console.ReadLine());

            if (inputCmd == 0)
            {
                context.ChangeState(new ExitMenuState());
            }
        }
        catch (Exception e) { }
    }
}

class ExitMenuState : IMenuState
{
    public void HandleOutput(MenuContext context)
    {
        Console.WriteLine("Menutup aplikasi...");
        Environment.Exit(0);
    }
}

class Register : IMenuState
{
    public void HandleOutput(MenuContext context)
    {
        //Register<RegAccount> register = new Register<RegAccount>();

        Console.Write("Nama: ");
        string nama = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        RegAccount fieldAccount = new RegAccount(nama, email, password);

        Console.Write("Konfirmasi Password: ");
        string passConfirm = Console.ReadLine();
        while (password != passConfirm)
        {
            Console.WriteLine("Try Again!");
        }
        string postData = $"nama={WebUtility.UrlEncode(nama)}&email={WebUtility.UrlEncode(email)}&password={WebUtility.UrlEncode(password)}";

        using (WebClient client = new WebClient())
        {
            client.Headers[HttpRequestHeader.ContentType] = "application/api";

            byte[] responseBytes= client.UploadData("http://localhost:5107/api/Register", "POST", Encoding.UTF8.GetBytes(postData));
            string response = Encoding.UTF8.GetString(responseBytes);

            Console.WriteLine(response);
        }
        //register.RegisterNewAccount(fieldAccount, password, passConfirm);

        //Console.Clear();
        //context.ChangeState(new InitialMenuState());
    }
}

class Program
{
    private static void Main(string[] args)
    {
        try
        {
            MenuContext context = new MenuContext(new AuthScreen());

            while (context.currentState is not ExitMenuState)
            {
                context.HandleOutput();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Sepertinya ada masalah: {0}", e.Message);
        }
        

        /*Console.WriteLine("\nAkun Terdaftar:");
        register.DisplayAccounts();*/
    }
}