using electrigreen;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using electrigreenAPI.Authentication;
using electrigreenAPI.Models;
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
                
            } else if (inputCmd == 1)
            {
                context.ChangeState(new Login(new AuthManager(new List<RegisterModel>())));
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
    public async void HandleOutput(MenuContext context)
    {
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5107");

        Console.Write("Nama: ");
        string nama = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();


        Console.Write("Konfirmasi Password: ");
        string passConfirm = Console.ReadLine();

        while (password != passConfirm)
        {
            Console.WriteLine("Try Again!");
        }

        User createUser = new User { Nama = nama, Email = email, Password = password };
        HttpResponseMessage resMessage = await httpClient.PostAsJsonAsync("api/Register/register", createUser);

        if (resMessage.IsSuccessStatusCode)
        {
            string resBodyRegister = await resMessage.Content.ReadAsStringAsync();
            Console.WriteLine("Response Register : " + resBodyRegister);
        }
        else if (resMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            string errMessage = await resMessage.Content.ReadAsStringAsync();
            Console.WriteLine("Failed to Register, Error : " + errMessage);
        }
        else
        {
            string errMessage = await resMessage.Content.ReadAsStringAsync();
            if (errMessage.Contains("hasBeenUsed"))
            {
                Console.WriteLine("Email sudah terdaftar, Coba lagi!");               
            }
            else
            {
                Console.WriteLine("Failed to Register, Error : " + resMessage.StatusCode);
            }
            Console.Clear();
            context.ChangeState(new AuthScreen());
        }


        context.ChangeState(new AuthScreen());
        //register.RegisterNewAccount(fieldAccount, password, passConfirm);

        //Console.Clear();
        //context.ChangeState(new InitialMenuState());
    }
}

class Login : IMenuState
{
    private readonly AuthManager _authManager;

    public Login(AuthManager authManager)
    {
        _authManager = authManager;
    }

    public async void HandleOutput(MenuContext context)
    {
        using (HttpClient httpClient = new HttpClient())
        {
           
            httpClient.BaseAddress = new Uri("http://localhost:5107");

            try
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                var isAuthenticated = await AuthenticateWithAPI(email, password);

                if (isAuthenticated)
                {
                    Console.WriteLine("Welcome: " + email);
                }
                else
                {
                    Console.WriteLine("Failed to Login, Email or Password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        context.ChangeState(new InitialMenuState());
    }
    private async Task<bool> AuthenticateWithAPI(string email, string password)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("http://localhost:5107");

            var loginUser = new LoginModel { email = email, password = password };

            HttpResponseMessage resMessage = await httpClient.PostAsJsonAsync("api/Register/login", loginUser);

            if (resMessage.IsSuccessStatusCode)
            {
                return true;
            }
            else if (resMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
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