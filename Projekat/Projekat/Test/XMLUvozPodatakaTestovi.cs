using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat.Klase;
using NUnit.Framework;
using System.Xml;

namespace Projekat.Test
{
    [TestFixture]
    public class XMLUvozPodatakaTestovi
    {
        [Test]
        public void Uvezi_ValidanXml_VracaListuPodataka()
        {
            // Pravi privremorni XML fajl sa validnim sadržajem za testiranje
            string xmlContent = "<ROOT><STAVKA><SAT>1</SAT><LOAD>10</LOAD><OBLAST>123</OBLAST></STAVKA></ROOT>";
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, xmlContent);

            // Kreiraj instancu klase za testiranje
            XMLUvozPodataka uvozPodataka = new XMLUvozPodataka();

            // Izvrši metodu koju želiš testirati
            List<UlazniPodaci> rezultat = uvozPodataka.Uvezi(tempFilePath);

            // Proveri rezultat
            Assert.That(rezultat.Count, Is.EqualTo(1));
            Assert.That(rezultat[0].Sat,Is.EqualTo(1));
            Assert.That(rezultat[0].IznosPotrosnje, Is.EqualTo(10).Within(0.001));
            Assert.That(rezultat[0].SifraGeoPodrucja,Is.EqualTo("123"));

            // Obriši privremeni fajl
            File.Delete(tempFilePath);
        }

        [Test]
        public void Uvezi_NullPutanja_ThrowArgumentNullException()
        {
            // Kreiraj instancu klase za testiranje
            XMLUvozPodataka uvozPodataka = new XMLUvozPodataka();

            // Proveri da li metoda baca ArgumentNullException kada se poziva sa null putanjom
            Assert.Throws<ArgumentNullException>(() => uvozPodataka.Uvezi(null));
        }

        [Test]
        public void Uvezi_NepostojecaPutanja_ThrowFileNotFoundException()
        {
            // Kreiraj instancu klase za testiranje
            XMLUvozPodataka uvozPodataka = new XMLUvozPodataka();

            // Pozovi metodu sa nepostojećom putanjom
            Assert.Throws<FileNotFoundException>(() => uvozPodataka.Uvezi("NepostojeciFajl.xml"));
        }

        [Test]
        public void Uvezi_NevalidanXml_FormatException()
        {
            // Kreiraj privremeni XML fajl sa neispravnim formatom (bez STAVKA elementa)
            string xmlPutanja = Path.GetTempFileName();
            string xmlSadrzaj = @"<ROOT></ROOT>";

            File.WriteAllText(xmlPutanja, xmlSadrzaj);

            try
            {
                // Kreiraj instancu klase za testiranje
                XMLUvozPodataka uvozPodataka = new XMLUvozPodataka();

                // Pozovi metodu sa nevalidnim XML formatom
                Assert.Throws<XmlException>(() => uvozPodataka.Uvezi(xmlPutanja));
            }
            finally
            {
                // Obriši privremeni fajl
                File.Delete(xmlPutanja);
            }
        }

    }
}
