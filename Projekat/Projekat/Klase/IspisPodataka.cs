using Projekat.Interfejsi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat.Klase
{
    public class IspisPodataka : IIspisPodataka
    {
        public void PrintConsumptionData(DateTime selectedDate, string selectedGeoArea)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            String nazivProgFajla = "Baza_prog_" + selectedDate.ToString("yyyy_MM_dd") + ".xml";
            String nazivOstvFajla = "Baza_ostv_" + selectedDate.ToString("yyyy_MM_dd") + ".xml";
            string progFilePath = Path.Combine(currentDirectory, nazivProgFajla);
            string ostvFilePath = Path.Combine(currentDirectory, nazivOstvFajla);
            string csvFilePath = $"Izvestaj_{selectedDate.ToString("yyyy_MM_dd")}.csv";

            if (!File.Exists(progFilePath) || !File.Exists(ostvFilePath))
            {
                Console.WriteLine($"Baza podataka za izabrani datum ne postoji.");
                return;
            }

            // Učitaj XML baze podataka
            XmlDocument progXmlDoc = new XmlDocument();
            progXmlDoc.Load(progFilePath);

            XmlDocument ostvXmlDoc = new XmlDocument();
            ostvXmlDoc.Load(ostvFilePath);

            // Pronađi odgovarajuće podatke za izabrani datum i geografsku oblast u prognoziranoj potrošnji
            XmlNodeList progEntries = progXmlDoc.SelectNodes($"/Podaci/PodaciEntry/Stavka[@SifraGeoPodrucja='{selectedGeoArea}']");

            // Pronađi odgovarajuće podatke za izabrani datum i geografsku oblast u ostvarenoj potrošnji
            XmlNodeList ostvEntries = ostvXmlDoc.SelectNodes($"/Podaci/PodaciEntry/Stavka[@SifraGeoPodrucja='{selectedGeoArea}']");

            // Ispisi rezultate
            Console.WriteLine("Sat".PadRight(5) + "Prognozirana Potrošnja".PadRight(25) + "Ostvarena Potrošnja".PadRight(25) + "Rel. Proc. Odstupanje");

            for (int i = 0; i < progEntries.Count; i++)
            {
                int sat = int.Parse(progEntries[i].Attributes["Sat"].Value);
                double prognoza = double.Parse(progEntries[i].Attributes["Potrosnja"].Value);
                double ostvareno = double.Parse(ostvEntries[i].Attributes["Potrosnja"].Value);

                double odstupanje = Math.Round(Math.Abs(((ostvareno - prognoza) / ostvareno) * 100),5);

                // Ispisi rezultat za svaki sat
                Console.WriteLine($"{sat}".PadRight(5) + $"{prognoza}".PadRight(25) + $"{ostvareno}".PadRight(25) + $"{odstupanje}%");
            }

            // Kreiraj CSV fajl
            // Kreiraj CSV fajl
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                // Header
                writer.WriteLine("Sat,Prognozirana Potrošnja,Ostvarena Potrošnja,Rel. Proc. Odstupanje");

                // Podaci
                for (int i = 0; i < progEntries.Count; i++)
                {
                    int sat = int.Parse(progEntries[i].Attributes["Sat"].Value);
                    double prognoza = double.Parse(progEntries[i].Attributes["Potrosnja"].Value);
                    double ostvareno = double.Parse(ostvEntries[i].Attributes["Potrosnja"].Value);

                    double odstupanje = ((ostvareno - prognoza) / ostvareno) * 100;

                    // Upisivanje reda u CSV fajl
                    writer.WriteLine($"{sat},{prognoza},{ostvareno},{odstupanje.ToString(CultureInfo.InvariantCulture)}%");
                }
            }

            Console.WriteLine($"Podaci su uspešno izvezeni u CSV fajl: {csvFilePath}");

        }
    }
}
