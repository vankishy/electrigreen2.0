using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ElectronicsAddConfig
{
    public string nama { get; set; }
    public string jenis { get; set; }
    public string merk { get; set; }
    public int voltase { get; set; }
    public bool isSmarthome { get; set; }

    public ElectronicsAddConfig(string nama, string jenis, string merk, int voltase, bool isSmarthome)
    {
        this.nama = nama;
        this.jenis = jenis;
        this.merk = merk;
        this.voltase = voltase;
        this.isSmarthome = isSmarthome;
    }
}

