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
    public class UpisUBazu : IUpisPodataka
    {
        public void WriteToDatabase(List<UlazniPodaci> data, string fileName, DateTime importTime, string importLocation)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement;

            string xmlFilePath = "BazaPodataka.xml";

            if (File.Exists(xmlFilePath))
            {
                xmlDoc.Load(xmlFilePath);
                rootElement = xmlDoc.DocumentElement;
            }
            else
            {
                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));
                rootElement = xmlDoc.CreateElement("Podaci");
                xmlDoc.AppendChild(rootElement);
            }

            string category;

            if (fileName.StartsWith("prog"))
            {
                category = "PrognoziranaPotrosnja";
            }
            else if (fileName.StartsWith("ostv"))
            {
                category = "OstvarenaPotrosnja";
            }
            else
            {
                // Nije moguće prepoznati tip potrošnje iz imena fajla
                // Dodajte odgovarajuću logiku ili obradu prema vašim potrebama
                return;
            }

            // Izvuci datum iz imena fajla
            DateTime fileDate;

            if (DateTime.TryParseExact(fileName.Substring(5, 10), "yyyy_MM_dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fileDate))
            {
                XmlElement dataNode = xmlDoc.CreateElement("PodaciEntry");

                foreach (var podatak in data)
                {
                    XmlElement stavkaNode = xmlDoc.CreateElement("Stavka");

                    // Postavite podatke za svaku stavku
                    stavkaNode.SetAttribute("Sat", podatak.Sat.ToString());
                    stavkaNode.SetAttribute("Potrosnja", podatak.IznosPotrosnje.ToString());
                    stavkaNode.SetAttribute("SifraGeoPodrucja", podatak.SifraGeoPodrucja);

                    dataNode.AppendChild(stavkaNode);
                }

                // Postavite informacije o kategoriji, vremenu uvoza itd.
                dataNode.SetAttribute("Category", category);
                dataNode.SetAttribute("FileName", fileName);
                dataNode.SetAttribute("FileDate", fileDate.ToString("yyyy-MM-dd"));
                dataNode.SetAttribute("ImportTime", importTime.ToString("yyyy-MM-dd HH:mm:ss"));
                dataNode.SetAttribute("ImportLocation", importLocation);

                rootElement.AppendChild(dataNode);

                // Sacuvajte promene u XML fajlu
                xmlDoc.Save(xmlFilePath);
            }
            else
            {
                // Nije moguce parsirati datum iz imena fajla
                // Dodajte odgovarajucu logiku ili obradu prema vasim potrebama
                return;
            }
        }
    }
}
