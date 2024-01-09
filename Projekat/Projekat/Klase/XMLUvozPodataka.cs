using Projekat.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat.Klase
{
    public class XMLUvozPodataka : IUvozPodataka
    {
        public List<UlazniPodaci> Uvezi(string putanja)
        {
            if (putanja == null)
            {
                throw new ArgumentNullException(nameof(putanja), "File path cannot be null.");
            }

            if (!File.Exists(putanja))
            {
                Console.WriteLine("Nije pronadjen fajl sa zadatim imenom");
            }

            List<UlazniPodaci> uvezeniPodaci = new List<UlazniPodaci>();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(putanja);

                XmlNodeList listaStavki = xmlDocument.SelectNodes("//STAVKA")!;

                if (listaStavki == null || listaStavki.Count == 0)
                {
                    throw new XmlException("Invalid XML format. Missing or empty 'STAVKA' elements.");
                }

                foreach (XmlNode stavkaNode in listaStavki)
                {
                    UlazniPodaci podatak = new UlazniPodaci
                    {
                        Sat = int.Parse(stavkaNode.SelectSingleNode("SAT")?.InnerText!),
                        IznosPotrosnje = double.Parse(stavkaNode.SelectSingleNode("LOAD")?.InnerText!),
                        SifraGeoPodrucja = stavkaNode.SelectSingleNode("OBLAST")?.InnerText
                    };

                    uvezeniPodaci.Add(podatak);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greska prilikom uvoza XML-a: {ex.Message}");
            }

            return uvezeniPodaci;


        }
    }
}
