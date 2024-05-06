using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum electronicState { BelumDitambahSmarthome, BelumDitambahNonSmarthome, TelahDitambahSmarhome, TelahDitambahNonSmarthome }
public enum Trigger { TombolAdd, SmarthomeTercentang, SmarthomeTidakTercentang }
public class Electronics
{
    private bool tombolAddTrigered = false;
    public ElectronicsConfig config;

    public Electronics(ElectronicsConfig config)
    {
        this.config = config;
    }


    public class kondisiElectronics
    {
        public electronicState stateAwal;
        public electronicState stateAkhir;
        public Trigger trigger;

        public kondisiElectronics(electronicState stateAwal, electronicState stateAkhir, Trigger trigger)
        {
            this.stateAwal = stateAwal;
            this.stateAkhir = stateAkhir;
            this.trigger = trigger;
        }
    }
    kondisiElectronics[] transisi =
    {
        new kondisiElectronics(electronicState.BelumDitambahNonSmarthome, electronicState.BelumDitambahSmarthome, Trigger.SmarthomeTercentang),
        new kondisiElectronics(electronicState.BelumDitambahNonSmarthome, electronicState.TelahDitambahNonSmarthome, Trigger.TombolAdd),
        new kondisiElectronics(electronicState.BelumDitambahSmarthome, electronicState.TelahDitambahSmarhome,Trigger.TombolAdd),
        new kondisiElectronics(electronicState.BelumDitambahSmarthome, electronicState.BelumDitambahNonSmarthome,Trigger.SmarthomeTidakTercentang),
        new kondisiElectronics(electronicState.TelahDitambahNonSmarthome, electronicState.TelahDitambahSmarhome,Trigger.SmarthomeTercentang),
        new kondisiElectronics(electronicState.TelahDitambahSmarhome, electronicState.TelahDitambahNonSmarthome, Trigger.SmarthomeTidakTercentang)
    };
    public electronicState currentState = electronicState.BelumDitambahNonSmarthome;

    public electronicState GetNextState(electronicState stateAwal, Trigger trigger)
    {
        electronicState stateAkhir = stateAwal;
        for (int i = 0; i < transisi.Length; i++)
        {
            kondisiElectronics perubahan = transisi[i];
            if (stateAwal == perubahan.stateAwal && trigger == perubahan.trigger)
            {
                stateAkhir = perubahan.stateAkhir;
            }
        }
        return stateAkhir;
    }
    public void ActivateTrigger(Trigger trigger)
    {
        //Design By Contract, setelah sudah ter add tidak bisa mengubah status smarthome lagi 
        if ((trigger == Trigger.SmarthomeTercentang || trigger == Trigger.SmarthomeTidakTercentang) && tombolAddTrigered)
        {
            Console.WriteLine("Tombol Add telah tertekan, anda tidak bisa mengubah status smarthome lagi.");
            return;
        }

        if (trigger == Trigger.TombolAdd)
        {
            tombolAddTrigered = true;
        }
        currentState = GetNextState(currentState, trigger);

        if (currentState == electronicState.TelahDitambahNonSmarthome)
        {
            Console.WriteLine("Perangkat " + config.config.jenis + "Dengan Nama " + config.config.nama + " dan Voltase " + config.config.voltase + " berhasil ditambahkan");
        }
        else if (currentState == electronicState.TelahDitambahSmarhome)
        {
            Console.WriteLine(config.config.nama + " berhasil ditambahkan sebagai perangkat Smarthome");
        }
        else if (currentState == electronicState.BelumDitambahNonSmarthome)
        {
            Console.WriteLine(config.config.nama + " tidak terhubung ke Smarthome");
            config.config.isSmarthome = false;
        }
        else if (currentState == electronicState.BelumDitambahSmarthome)
        {
            Console.WriteLine(config.config.nama + "terhubung ke Smarthome");
            config.config.isSmarthome = true;
        }
        Console.WriteLine("Status perangkat ini: " + currentState);
    }

}
