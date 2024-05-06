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
using electrigreenAPI.Models;
using System.Diagnostics.Contracts;
using LoginLibrary;
using System.Text.RegularExpressions;
using Spectre.Console;
using System.Collections;

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
                context.ChangeState(new RegisterState());
                
            } else if (inputCmd == 1)
            {
                context.ChangeState(new Login(new AuthManager(new List<RegisterModel>())));
            }
        }
        catch (Exception e) {
            Console.WriteLine("Input hrus berupa angka!");
            Console.Clear();
            
        }
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
            else if (inputCmd == 1)
            {
                context.ChangeState(new tambahPerangkat());
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

class RegisterState : IMenuState
{
    public void HandleOutput(MenuContext context)
    {
        Register();
        Console.Clear();
        context.ChangeState(new AuthScreen());
    }

    public static bool isValidName(string nama)
    {
        Regex check = new Regex("^[a-zA-Z]+$");
        return check.IsMatch(nama);
    }
    public async void Register()
    {
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5107");

        Console.Write("Nama: ");
        string nama = Console.ReadLine();
        while (!isValidName(nama))
        {
            Console.WriteLine("Nama hanya terdiri dari huruf");
            Console.Write("Nama: ");
            nama = Console.ReadLine();
        }
        Contract.Requires(!string.IsNullOrWhiteSpace(nama), "Nama tidak boleh kosong");

        Console.Write("Email: ");
        string email = Console.ReadLine();
        Contract.Requires(!string.IsNullOrWhiteSpace(email), "Email tidak boleh kosong");

        string password = AnsiConsole.Prompt(new TextPrompt<string>("Password: ")
            .Secret());
        Contract.Requires(!string.IsNullOrWhiteSpace(password), "Password tidak boleh kosong");


        string passConfirm = AnsiConsole.Prompt(new TextPrompt<string>("Konfirmasi Password: ")
            .Secret());
        Contract.Requires(!string.IsNullOrWhiteSpace(passConfirm), "Konfirmasi Password tidak boleh kosong");

        while (password != passConfirm)
        {
            Console.WriteLine("Password salah! Coba lagi.");
            Console.Write("Konfirmasi Password: ");
            passConfirm = Console.ReadLine();
            Contract.Requires(!string.IsNullOrWhiteSpace(passConfirm), "Konfirmasi Password tidak boleh kosong");
        }

        User createUser = new User { Nama = nama, Email = email, Password = password };
        HttpResponseMessage resMessage = await httpClient.PostAsJsonAsync("api/Register/register", createUser);

        if (resMessage.IsSuccessStatusCode)
        {
            string resBodyRegister = await resMessage.Content.ReadAsStringAsync();
            Console.WriteLine("Response Register : " + resBodyRegister);
        }
        else if (resMessage.StatusCode == HttpStatusCode.BadRequest)
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
        }
    }
}


class Login : IMenuState
{
    private readonly AuthManager _authManager;

    public Login(AuthManager authManager)
    {
        _authManager = authManager;
    }
    public void HandleOutput(MenuContext context)
    {
        loginAccount();
        Console.Clear();
        context.ChangeState(new InitialMenuState());
    }
    public async void loginAccount()
    {
        using (HttpClient httpClient = new HttpClient())
        {
           
            httpClient.BaseAddress = new Uri("http://localhost:5107");

            try
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();
                string password = AnsiConsole.Prompt(new TextPrompt<string>("Password: ")
                    .Secret());

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
    }
    public async Task<bool> AuthenticateWithAPI(string email, string password)
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
class tambahPerangkat : IMenuState
{
    public void HandleOutput(MenuContext context)
    {
        ArrayList perangkatElektronik = new ArrayList();

        while (true)
        {
            Console.WriteLine("Add a new electronic device:");
            Console.Write("Nama: ");
            string nama = Console.ReadLine();
            Console.Write("Jenis: ");
            string jenis = Console.ReadLine();
            Console.Write("Merk: ");
            string merk = Console.ReadLine();
            Console.Write("Voltase: ");
            int voltase = Convert.ToInt32(Console.ReadLine());
            bool isSmarthome = false;

            ElectronicsConfig config = new ElectronicsConfig();
            Electronics electronics = new Electronics(config);

            int input = -1;
            while (input != 0)
            {
                if (electronics.currentState == electronicState.BelumDitambahNonSmarthome)
                {
                    Console.WriteLine("1. Centang Sebagai Perangkat Smarthome: ");
                }
                else
                {
                    Console.WriteLine("1. Centang Sebagai Perangkat Non-Smarthome: ");
                }
                Console.WriteLine("0. Tambahkan Perangkat");
                input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    if (electronics.currentState == electronicState.BelumDitambahNonSmarthome)
                    {
                        electronics.ActivateTrigger(Trigger.SmarthomeTercentang);
                        isSmarthome = true;
                    }
                    else
                    {
                        electronics.ActivateTrigger(Trigger.SmarthomeTidakTercentang);
                        isSmarthome = false;
                    }
                }
                else if (input == 0)
                {
                    PerangkatLibrary.PerangkatLibrary.AddPerangkat(perangkatElektronik, nama, jenis, merk, voltase, isSmarthome);
                    break;
                }
            }

            Console.Write("Add another device? (yes/no): ");
            string addAnother = Console.ReadLine().ToLower();
            if (addAnother != "yes" && addAnother != "y")
                break;
        }

        context.ChangeState(new InitialMenuState());
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
    }
}