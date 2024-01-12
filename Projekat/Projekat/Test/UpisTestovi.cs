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
    public class UpisTestovi
    {
        [Test]
        public void WriteToDatabase_NevalidniPodaci_LosDatum()
        {
            // Kreiraj privremeni XML fajl
            string xmlPutanja = "prog_20200507xml";
            string xmlSadrzaj = "<ROOT><STAVKA><SAT>1</SAT><LOAD>10.5</LOAD><OBLAST>123</OBLAST></STAVKA></ROOT>";
            File.WriteAllText(xmlPutanja, xmlSadrzaj);

            try
            {
                // Kreiraj instancu klase za testiranje
                UpisUBazu upisUBazu = new UpisUBazu();

                // Kreiraj listu sa validnim podacima
                List<UlazniPodaci> validniPodaci = new List<UlazniPodaci>
                {
                    new UlazniPodaci
                    {
                        Sat = 1,
                        IznosPotrosnje = 10.5,
                        SifraGeoPodrucja = "123"
                    }
                };

                // Pozovi metodu sa validnim podacima
                int rezultat = upisUBazu.WriteToDatabase(validniPodaci, xmlPutanja, DateTime.Now, "Lokacija");

                // Proveri rezultat
                Assert.That(rezultat, Is.EqualTo(-1));
            }
            finally
            {
                // Obriši privremeni fajl
                File.Delete(xmlPutanja);
            }
        }

        [Test]
        public void WriteToDatabase_NevalidanPodaci_LosTipPotrosnje()
        {
            // Kreiraj privremeni XML fajl
            string xmlPutanja = "TempTestFile.xml";
            string xmlSadrzaj = "<ROOT><STAVKA><SAT>1</SAT><LOAD>10.5</LOAD><OBLAST>123</OBLAST></STAVKA></ROOT>";
            File.WriteAllText(xmlPutanja, xmlSadrzaj);

            try
            {
                // Kreiraj instancu klase za testiranje
                UpisUBazu upisUBazu = new UpisUBazu();

                // Kreiraj listu sa validnim podacima
                List<UlazniPodaci> validniPodaci = new List<UlazniPodaci>
                {
                    new UlazniPodaci
                    {
                        Sat = 1,
                        IznosPotrosnje = 10.5,
                        SifraGeoPodrucja = "123"
                    }
                };

                // Pozovi metodu sa nevalidnim datumom
                int rezultat = upisUBazu.WriteToDatabase(validniPodaci, xmlPutanja, DateTime.Now, "Lokacija");

                // Proveri rezultat
                Assert.That(rezultat, Is.EqualTo(-1));
            }
            finally
            {
                // Obriši privremeni fajl
                File.Delete(xmlPutanja);
            }
        }

        [Test]
        public void WriteToDatabase_NevalidniPodaci_VisestrukiUnos()
        {
            List<UlazniPodaci> validniPodaci = new List<UlazniPodaci>();

            for (int sat = 1; sat <= 24; sat++)
            {
                // Prilagodite vrednosti prema potrebama vašeg testa
                validniPodaci.Add(new UlazniPodaci { Sat = sat, IznosPotrosnje = 10.0 + sat, SifraGeoPodrucja = "BNT" });
            }

            string fileName = "prog_2022_01_01.xml"; // Prilagodite ime fajla prema potrebama
            DateTime importTime = DateTime.Now;
            string importLocation = "TestLokacija";

            UpisUBazu upisUBazu = new UpisUBazu();

            int rezultat = upisUBazu.WriteToDatabase(validniPodaci, fileName, importTime, importLocation);

            Assert.That(rezultat, Is.EqualTo(-1));
        }

        [Test]
        public void WriteToDatabase_ValidniPodaci_Vraca0() // Bug sa vec unetom bazom?
        {
            List<UlazniPodaci> validniPodaci = new List<UlazniPodaci>();

            for (int sat = 1; sat <= 24; sat++)
            {
                // Prilagodite vrednosti prema potrebama vašeg testa
                validniPodaci.Add(new UlazniPodaci { Sat = sat, IznosPotrosnje = 10.0 + sat, SifraGeoPodrucja = "123" });
            }

            string fileName = "prog_2022_05_01.xml"; // Prilagodite ime fajla prema potrebama
            DateTime importTime = DateTime.Now;
            string importLocation = "TestLokacija";

            UpisUBazu upisUBazu = new UpisUBazu();

            int rezultat = upisUBazu.WriteToDatabase(validniPodaci, fileName, importTime, importLocation);

            Assert.That(rezultat, Is.EqualTo(0));
        }
    }
}
