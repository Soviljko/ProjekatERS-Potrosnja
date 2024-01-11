using Projekat.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Projekat.Klase
{
    internal class Region : IRegioni
    {
        public void UnesiNovuRegiju(string sifra)
        {
            XmlDocument xmlDoc = new XmlDocument();

            string ime = "Regioni.xml";
            string imeReg;

            try
            {
                xmlDoc.Load(ime);

                XmlElement newRegijaElement = xmlDoc.CreateElement("REGIJA");

                XmlElement sifraElement = xmlDoc.CreateElement("SIFRA");
                sifraElement.InnerText = sifra;
                newRegijaElement.AppendChild(sifraElement);

                Console.WriteLine("Unesite ime regiona:");
                do
                {
                    imeReg = Console.ReadLine();
                } while (string.IsNullOrEmpty(imeReg));

                XmlElement nazivElement = xmlDoc.CreateElement("NAZIV");
                nazivElement.InnerText = imeReg;
                newRegijaElement.AppendChild(nazivElement);

                // Append the new REGIJA element to the root element
                xmlDoc.DocumentElement?.AppendChild(newRegijaElement);

                // Save the modified XML document back to the file
                xmlDoc.Save(ime);

                Console.WriteLine("Data appended to the existing XML file successfully.");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public List<UlazRegiona> UveziRegije(string filePath)
        {
            throw new NotImplementedException();
        }
    }
    
    
}
