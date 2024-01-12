using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Projekat.Interfejsi;
using Projekat.Klase;

namespace Projekat.Test
{
    [TestFixture]
    public class UvozRegionaTest
    {
        private const string XmlFilePath = "Regioni.xml";

        [Test]
        public void Uvezi_ValidanXml_VracaListuPodataka()
        {
            IUvozRegiona uvozRegiona = new UvozXMLReg();

            var uvezeniPodaci = uvozRegiona.Uvezi(XmlFilePath);

            Assert.That(uvezeniPodaci, Is.Not.Empty);
            Assert.That(uvezeniPodaci.Count, Is.EqualTo(5));

            Assert.That(uvezeniPodaci[0].SifraRegiona, Is.EqualTo("BGD"));
            Assert.That(uvezeniPodaci[0].NazivRegiona, Is.EqualTo("Beograd"));
        }
    }
}
