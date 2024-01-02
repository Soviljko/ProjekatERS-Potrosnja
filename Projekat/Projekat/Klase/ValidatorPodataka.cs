using Projekat.Interfejsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Klase
{
    public class ValidatorPodataka : IProveraPodataka
    {
        public class InvalidDataInfo
        {
            public DateTime AttemptTime { get; set; }
            public string FileName { get; set; }
            public string Location { get; set; }
            public int RowCount { get; set; }
        }

        private List<InvalidDataInfo> invalidDataInfoList = new List<InvalidDataInfo>();

        public bool ValidateData(List<UlazniPodaci> data, string fileName, string location)
        {
            foreach (var group in data.GroupBy(entry => entry.SifraGeoPodrucja))
            {
                int expectedHourCount = 24; // Očekivan broj sati u danu

                // Proveri broj sati za svaku geografsku oblast
                if (group.Count() != expectedHourCount && group.Count() != expectedHourCount - 1 && group.Count() != expectedHourCount + 1)
                {
                    // Ako broj sati ne odgovara očekivanom, evidentiraj podatke u audit tabeli
                    LogInvalidData(fileName, location, data.Count);
                    return false; // Pronađeni su nevalidni podaci
                }
            }

            return true; // Nema nevalidnih podataka
        }

        private void LogInvalidData(string fileName, string location, int rowCount)
        {
            // Ako želiš da koristiš ovu listu kasnije, možeš je koristiti kao izvor informacija o nevalidnim podacima
            var invalidDataInfo = new InvalidDataInfo
            {
                AttemptTime = DateTime.Now,
                FileName = fileName,
                Location = location,
                RowCount = rowCount
            };
            invalidDataInfoList.Add(invalidDataInfo);

            // Prikaz poruke o nevalidnim podacima (za testiranje)
            Console.WriteLine($"Detektovani nevalidni podaci za fajl {fileName}\nSa lokacije {location}\nBroj redova: {rowCount}");
        }

        // Dodatna metoda koja omogućava pristup informacijama o nevalidnim podacima
        public List<InvalidDataInfo> GetInvalidDataInfo()
        {
            return invalidDataInfoList;
        }
    }
}
