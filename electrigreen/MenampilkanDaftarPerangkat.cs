public class MenampilkanDaftarPerangkat
{
    public enum EnumNamaPerangkat
    {
        Lenovo_LOQ,
        Changhong_U75F8T,
        Xiaomi_13T,
        Bardi_9W,
        Macbook_Air,
        Phillips_LED,
        Panasonic_Socket,
        Huawei_AX3,
        Sanken_05DL,
        Dyson_TP10
    }

    public static class PerangkatHelper<T>
    {
        public static string GetJenisPerangkat(T inputPerangkat)
        {
            // Memeriksa apakah T adalah Enum
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T harus merupakan tipe Enum.");
            }

            // Memeriksa apakah nilai inputPerangkat valid dalam enum
            if (!Enum.IsDefined(typeof(T), inputPerangkat))
            {
                throw new ArgumentException("Nilai inputPerangkat tidak valid dalam enum.");
            }

            string[] outputJenisPerangkat = { "Laptop", "Televisi", "Ponsel", "Lampu", "Laptop", "Lampu", "Stop Kontak", "Router", "Pendingin Ruangan", "Pembersih Udara" };
            return outputJenisPerangkat[Convert.ToInt32(inputPerangkat)];
        }
    }
}

