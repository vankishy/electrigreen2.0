using electrigreen;
using static MenampilkanAchievement;

namespace TestMenampilkanAchievement
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMenampilkan1()
        {
            //Arrange
            EnumNamaAchievement Achievement = EnumNamaAchievement.ENERGI_HIJAU_1000;
            string expected = "menghemat 1000 unit energi dalam penggunaan perangkat elektroni";

            //Act
            string result = MenampilkanAchievement.AchievementHelper<EnumNamaAchievement>.GetJenisAchievement(Achievement);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMenampilkan2()
        {
            //Arrange
            EnumNamaAchievement Achievement = EnumNamaAchievement.PENGHARGAAN_ENERGI_RAMAH_LINGKUNGAN;
            string expected = "menginspirasi pengguna untuk menghemat energi dengan mengshare artikel";

            //Act
            string result = MenampilkanAchievement.AchievementHelper<EnumNamaAchievement>.GetJenisAchievement(Achievement);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMenampilkan3()
        {
            //Arrange
            EnumNamaAchievement Achievement = EnumNamaAchievement.KEMITRAAN_PRODUSEN_TERKEMUKA;
            string expected = "mengurangi jejak karbon selama 5 hari";

            //Act
            string result = MenampilkanAchievement.AchievementHelper<EnumNamaAchievement>.GetJenisAchievement(Achievement);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMenampilkan4()
        {
            //Arrange
            EnumNamaAchievement Achievement = EnumNamaAchievement.EDUKASI_ENERGI_BERKELANJUTAN;
            string expected = "Membaca artikel, panduan praktis, dan webinar interaktif";

            //Act
            string result = MenampilkanAchievement.AchievementHelper<EnumNamaAchievement>.GetJenisAchievement(Achievement);

            Assert.AreEqual(expected, result);
        }
    }
}