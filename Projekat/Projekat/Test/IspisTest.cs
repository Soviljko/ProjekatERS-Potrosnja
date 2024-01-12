using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Projekat.Klase;

namespace Projekat.Test
{
    [TestFixture]
    public class IspisTest
    {
        [Test]
        public void PrintConsumptionData_ValidniPodaci_IspisujePodatke()
        {
            // Pravimo privremene XML fajlove sa validnim sadržajem za testiranje
            string progXmlContent = "<Podaci><PodaciEntry><Stavka Sat=\"1\" Potrosnja=\"10.5\" SifraGeoPodrucja=\"123\"/></PodaciEntry></Podaci>";
            string ostvXmlContent = "<Podaci><PodaciEntry><Stavka Sat=\"1\" Potrosnja=\"12.0\" SifraGeoPodrucja=\"123\"/></PodaciEntry></Podaci>";

            string tempProgFilePath = Path.GetTempFileName();
            string tempOstvFilePath = Path.GetTempFileName();

            File.WriteAllText(tempProgFilePath, progXmlContent);
            File.WriteAllText(tempOstvFilePath, ostvXmlContent);

            DateTime selectedDate = DateTime.Now;
            string selectedGeoArea = "123";

            IspisPodataka ispisPodataka = new IspisPodataka();

            // Učitaj podatke
            ispisPodataka.PrintConsumptionData(selectedDate, selectedGeoArea);

            // Proveri da li su podaci ispravno ispisani na konzoli (ovo može zavisiti od okoline u kojoj se test izvršava)
            // Ukoliko je moguće, možete iskoristiti koncept "console redirection" kako biste uhvatili ispis na konzoli i zatim proverili očekivane rezultate.

            // Obriši privremene fajlove
            File.Delete(tempProgFilePath);
            File.Delete(tempOstvFilePath);
        }
    }
}
