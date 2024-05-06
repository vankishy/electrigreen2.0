using electrigreen;
using System;
using static MenampilkanAchievement;
class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string Achievement1 = AchievementHelper<int>.GetJenisAchievement(1);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        // Uji Defensive Programming 2
        try
        {
            EnumNamaAchievement Achievement2 = (EnumNamaAchievement)99;
            string jenisAchievement2 = AchievementHelper<EnumNamaAchievement>.GetJenisAchievement(Achievement2);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        EnumNamaAchievement Achievement = EnumNamaAchievement.PENGHARGAAN_ENERGI_RAMAH_LINGKUNGAN;
        string jenisAchievement = AchievementHelper<EnumNamaAchievement>.GetJenisAchievement(Achievement);
        Console.WriteLine(jenisAchievement);
    }
}