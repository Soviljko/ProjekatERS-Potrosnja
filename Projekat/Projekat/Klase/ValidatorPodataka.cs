using Projekat.Interfejsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat.Klase
{
    public class ValidatorPodataka : IProveraPodataka
    {

        public bool Validacija(List<UlazniPodaci> podaci, string imeFajla, string lokacija)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(imeFajla); // Učitava XML fajl

            // Brojanje broja čvorova u XML fajlu
            int brojRedova = xmlDoc.DocumentElement!.ChildNodes.Count;

            foreach (var grupa in podaci.GroupBy(entry => entry.SifraGeoPodrucja))
            {
                int brojSati = 24; // Očekivan broj sati u danu

                // Proveri broj sati za svaku geografsku oblast
                if (grupa.Count() != brojSati && grupa.Count() != brojSati - 1 && grupa.Count() != brojSati + 1)
                {
                    // Ako broj sati ne odgovara očekivanom, evidentiraj podatke u XML fajlu
                    SacuvajXML(imeFajla, lokacija, brojRedova);
                    Console.WriteLine("Pronadjeni su nevalidni podaci,fajl se odbacuje...");
                    return false; // Pronađeni su nevalidni podaci
                }
            }

            return true; // Nema nevalidnih podataka
        }

        private void SacuvajXML(string imefajla, string lokacija, int brojredova)
        {
            // Putanja do XML fajla
            string xmlPutanja = "Nevalidni_Podaci.xml"; // Prilagodite putanju prema potrebama

            // Kreiraj novi XML dokument ili otvori postojeći
            XmlDocument xmlDoc = new XmlDocument();

            // Ako XML fajl ne postoji, postavi korenski element
            if (!File.Exists(xmlPutanja))
            {
                XmlElement rootElement = xmlDoc.CreateElement("NevalidniPodaci");
                xmlDoc.AppendChild(rootElement);
            }
            else
            {
                // Ako XML fajl postoji, učitaj ga
                xmlDoc.Load(xmlPutanja);
            }

            // Kreiraj novi čvor za nevalidne podatke
            XmlElement invalidDataNode = xmlDoc.CreateElement("NevalidniPodatak");

            // Dodaj atribute
            invalidDataNode.SetAttribute("VremeUcitavanja", DateTime.Now.ToString());
            invalidDataNode.SetAttribute("ImeFajla", imefajla);
            invalidDataNode.SetAttribute("LokacijaUcitavanja", lokacija);
            invalidDataNode.SetAttribute("BrojRedova", brojredova.ToString());

            // Dodaj čvor u XML dokument
            xmlDoc.DocumentElement?.AppendChild(invalidDataNode);

            // Sacuvaj XML dokument
            xmlDoc.Save(xmlPutanja);
        }


    }
}
