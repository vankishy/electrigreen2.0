using electrigreen;
using static MenampilkanDaftarPerangkat;

namespace TestMenampilkanPerangkat
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMenampilkan1()
        {
            //Arrange
            EnumNamaPerangkat perangkat = EnumNamaPerangkat.Lenovo_LOQ;
            string expected = "Laptop";

            //Act
            string result = MenampilkanDaftarPerangkat.PerangkatHelper<EnumNamaPerangkat>.GetJenisPerangkat(perangkat);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMenampilkan2()
        {
            //Arrange
            EnumNamaPerangkat perangkat = EnumNamaPerangkat.Xiaomi_13T;
            string expected = "Ponsel";

            //Act
            string result = MenampilkanDaftarPerangkat.PerangkatHelper<EnumNamaPerangkat>.GetJenisPerangkat(perangkat);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMenampilkan3()
        {
            //Arrange
            EnumNamaPerangkat test1 = EnumNamaPerangkat.Bardi_9W;
            string expected = "Lampu";

            //Act
            string result = MenampilkanDaftarPerangkat.PerangkatHelper<EnumNamaPerangkat>.GetJenisPerangkat(test1);

            Assert.AreEqual(expected, result);
        }
    }
}