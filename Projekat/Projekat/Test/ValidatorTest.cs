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
    public class ValidatorTest
    {
        [Test]
        public void Validacija_ValidniPodaci_VracaTrue()
        {
            // Kreiraj privremeni XML fajl sa tačno 24 unosa za svaku geografsku oblast
            string xmlPutanja = "TempTestFile.xml";
            StringBuilder xmlBuilder = new StringBuilder("<ROOT>");

            for (int sat = 1; sat <= 24; sat++)
            {
                xmlBuilder.Append($"<STAVKA><SAT>{sat}</SAT><LOAD>10.5</LOAD><OBLAST>123</OBLAST></STAVKA>");
            }

            xmlBuilder.Append("</ROOT>");
            File.WriteAllText(xmlPutanja, xmlBuilder.ToString());

            try
            {
                // Kreiraj instancu klase za testiranje
                ValidatorPodataka validator = new ValidatorPodataka();

                // Kreiraj listu sa tačno 24 unosa za svaku geografsku oblast
                List<UlazniPodaci> validniPodaci = new List<UlazniPodaci>();

                for (int sat = 1; sat <= 24; sat++)
                {
                    validniPodaci.Add(new UlazniPodaci
                    {
                        Sat = sat,
                        IznosPotrosnje = 10.5,
                        SifraGeoPodrucja = "123"
                    });
                }

                // Pozovi metodu sa validnim podacima
                bool rezultat = validator.Validacija(validniPodaci, xmlPutanja, "Lokacija");

                // Proveri rezultat
                Assert.That(rezultat, Is.True);
            }
            finally
            {
                // Obriši privremeni fajl
                File.Delete(xmlPutanja);
            }
        }

        [Test]
        public void Validacija_NevalidniPodaci_VracaFalse()
        {
            // Kreiraj privremeni XML fajl sa manje od 24 unosa za neku geografsku oblast
            string xmlPutanja = "TempTestFile.xml";
            string xmlSadrzaj = "<ROOT><STAVKA><SAT>1</SAT><LOAD>10.5</LOAD><OBLAST>123</OBLAST></STAVKA></ROOT>";
            File.WriteAllText(xmlPutanja, xmlSadrzaj);

            try
            {
                // Kreiraj instancu klase za testiranje
                ValidatorPodataka validator = new ValidatorPodataka();

                // Kreiraj listu sa manje od 24 unosa za neku geografsku oblast
                List<UlazniPodaci> nevalidniPodaci = new List<UlazniPodaci>
                {
                    new UlazniPodaci
                    {
                        Sat = 1,
                        IznosPotrosnje = 10.5,
                        SifraGeoPodrucja = "123"
                    }
                    // Dodajte samo jedan unos umesto 24 za ovu ilustraciju
                };

                // Pozovi metodu sa nevalidnim podacima
                bool rezultat = validator.Validacija(nevalidniPodaci, xmlPutanja, "Lokacija");

                // Proveri rezultat
                Assert.That(rezultat, Is.False);
            }
            finally
            {
                // Obriši privremeni fajl
                File.Delete(xmlPutanja);
            }
        }
    }
}
