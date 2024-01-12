using Projekat.Interfejsi;
using Projekat.Klase;
using System.Text.RegularExpressions;
using System.Xml;


public class Program
{
    private static readonly KorisnickiMeni meni = new KorisnickiMeni();
    private static void Main(string[] args)
    {
        string filePath = "Regioni.xml";

        

        if (!File.Exists(filePath))
        {
           
            
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlElement regioniElement = xmlDoc.CreateElement("REGIONI");

                XmlElement regijaElement = xmlDoc.CreateElement("REGIJA");

                XmlElement nazivElement = xmlDoc.CreateElement("NAZIV");
                nazivElement.InnerText = "Beograd";
                regijaElement.AppendChild(nazivElement);

                XmlElement sifraElement = xmlDoc.CreateElement("SIFRA");
                sifraElement.InnerText = "BGD";
                regijaElement.AppendChild(sifraElement);

                regioniElement.AppendChild(regijaElement);

                xmlDoc.AppendChild(regioniElement);

                xmlDoc.Save("Regioni.xml");

                Console.WriteLine("XML file created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        meni.HandleKorisnickiMeni();
    }

    
}

