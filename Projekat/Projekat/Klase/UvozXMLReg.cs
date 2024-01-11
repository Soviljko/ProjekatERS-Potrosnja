using Projekat.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat.Klase
{
    internal class UvozXMLReg : IUvozRegiona
    {
        public List<UlazRegiona> Uvezi(string filePath)
        {
            List<UlazRegiona> uvezeniPodaci = new List<UlazRegiona>();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);

                XmlNodeList listaRegiona = xmlDocument.SelectNodes("//REGIJA")!;

                if (listaRegiona == null || listaRegiona.Count == 0)
                {
                    throw new XmlException("Invalid XML format. Missing or empty 'REGIJA' elements.");
                }

                foreach (XmlNode regionNode in listaRegiona)
                {
                    UlazRegiona podatak = new UlazRegiona
                    {
                        SifraRegiona = regionNode.SelectSingleNode("SIFRA")?.InnerText,
                        NazivRegiona = regionNode.SelectSingleNode("NAZIV")?.InnerText

                        /*Sat = int.Parse(stavkaNode.SelectSingleNode("SAT")?.InnerText!),
                        IznosPotrosnje = double.Parse(stavkaNode.SelectSingleNode("LOAD")?.InnerText!),
                        SifraGeoPodrucja = stavkaNode.SelectSingleNode("OBLAST")?.InnerText*/
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
