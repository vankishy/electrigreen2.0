public class MenampilkanAchievement
{
    public enum EnumNamaAchievement
    {
        ENERGI_HIJAU_1000,
        PENGHARGAAN_ENERGI_RAMAH_LINGKUNGAN,
        KEMITRAAN_PRODUSEN_TERKEMUKA,
        EDUKASI_ENERGI_BERKELANJUTAN,
    }

    public static class AchievementHelper<T>
    {
        public static string GetJenisAchievement(T inputAchievement)
        {
            // Memeriksa apakah T adalah Enum
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T harus merupakan tipe Enum.");
            }

            // Memeriksa apakah nilai inputAchievement valid dalam enum
            if (!Enum.IsDefined(typeof(T), inputAchievement))
            {
                throw new ArgumentException("Nilai inputAchievement tidak valid dalam enum.");
            }

            string[] outputJenisAchievement = { "menghemat 1000 unit energi dalam penggunaan perangkat elektronik", "menginspirasi pengguna untuk menghemat energi dengan mengshare artikel", "mengurangi jejak karbon selama 5 hari", "Membaca artikel, panduan praktis, dan webinar interaktif" };
            return outputJenisAchievement[Convert.ToInt32(inputAchievement)];
        }
    }
}