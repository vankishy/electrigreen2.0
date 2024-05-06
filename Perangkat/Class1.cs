namespace PerangkatLibrary
{
    public class PerangkatLibrary
    {
        public static void AddPerangkat(System.Collections.ArrayList PerangkatList, string Nama, string Jenis, string merk, int voltase, bool isSmarthome)
        {
            // Design by contract: Validate input parameters
            if (string.IsNullOrEmpty(Nama))
                throw new ArgumentException("Nama cannot be null or empty.", nameof(Nama));
            if (string.IsNullOrEmpty(Jenis))
                throw new ArgumentException("Jenis cannot be null or empty.", nameof(Jenis));
            if (string.IsNullOrEmpty(merk))
                throw new ArgumentException("Merk cannot be null or empty.", nameof(merk));
            if (voltase <= 0)
                throw new ArgumentException("Voltase must be greater than zero.", nameof(voltase));

            ElectronicsConfig config = new ElectronicsConfig();
            config.config.nama = Nama;
            config.config.isSmarthome = isSmarthome;

            Electronics electronics = new Electronics(config);

            config.config.jenis = Jenis;
            config.config.merk = merk;
            config.config.voltase = voltase;
            PerangkatList.Add(electronics);
        }
    }
}