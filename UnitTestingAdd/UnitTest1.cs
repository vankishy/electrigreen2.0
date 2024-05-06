using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using PerangkatLibrary;

namespace YourTestProjectNamespace
{
    [TestClass]
    public class PerangkatLibraryTests
    {
        [TestMethod]
        public void AddPerangkat_AddsDeviceToList()
        {

            ArrayList perangkatElektronik = new ArrayList();
            string nama = "DeviceName";
            string jenis = "DeviceType";
            string merk = "DeviceBrand";
            int voltase = 220;
            bool isSmarthome = true;

            ElectronicsConfig config = new ElectronicsConfig();
            Electronics electronics = new Electronics(config);

            if (isSmarthome)
            {
                electronics.ActivateTrigger(Trigger.SmarthomeTercentang);
            }
            electronics.ActivateTrigger(Trigger.TombolAdd);

            PerangkatLibrary.PerangkatLibrary.AddPerangkat(perangkatElektronik, nama, jenis, merk, voltase, isSmarthome);

            Assert.AreEqual(1, perangkatElektronik.Count, "Device count is not as expected");
        }
        [TestMethod]
        public void ActivateTrigger_SmarthomeTercentang_ChangesStateToSmarthome()
        {
            // Arrange
            ElectronicsConfig config = new ElectronicsConfig();
            Electronics electronics = new Electronics(config);

            // Act
            electronics.ActivateTrigger(Trigger.SmarthomeTercentang);

            // Assert
            Assert.AreEqual(electronicState.BelumDitambahSmarthome, electronics.currentState, "State should be Smarthome");
        }

        [TestMethod]
        public void ActivateTrigger_SmarthomeTidakTercentang_ChangesStateToNonSmarthome()
        {
            // Arrange
            ElectronicsConfig config = new ElectronicsConfig();
            Electronics electronics = new Electronics(config);

            // Act
            electronics.ActivateTrigger(Trigger.SmarthomeTidakTercentang);

            // Assert
            Assert.AreEqual(electronicState.BelumDitambahNonSmarthome, electronics.currentState, "State should be Non-Smarthome");
        }
    }
}

