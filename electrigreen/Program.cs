using electrigreen;
using System;
using static MenampilkanDaftarPerangkat;
class Program
{
    private static void Main(string[] args)
    {
        //Uji Defensive Programming 1
        try
        {
            string perangkat1 = PerangkatHelper<int>.GetJenisPerangkat(1);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        // Uji Defensive Programming 2
        try
        {
            EnumNamaPerangkat perangkat2 = (EnumNamaPerangkat)99;
            string jenisPerangkat2 = PerangkatHelper<EnumNamaPerangkat>.GetJenisPerangkat(perangkat2);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        // Uji valid
        EnumNamaPerangkat perangkat = EnumNamaPerangkat.Macbook_Air;
        string jenisPerangkat = PerangkatHelper<EnumNamaPerangkat>.GetJenisPerangkat(perangkat);
        Console.WriteLine(jenisPerangkat);
    }
}
