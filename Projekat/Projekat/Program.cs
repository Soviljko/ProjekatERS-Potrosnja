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
                // Create an XmlDocument object
                XmlDocument xmlDoc = new XmlDocument();

                // Create the root element
                XmlElement regioniElement = xmlDoc.CreateElement("REGIONI");

                // Create a regija element
                XmlElement regijaElement = xmlDoc.CreateElement("REGIJA");

                // Create and append child elements to the regija element
                XmlElement nazivElement = xmlDoc.CreateElement("NAZIV");
                nazivElement.InnerText = "Beograd";
                regijaElement.AppendChild(nazivElement);

                XmlElement sifraElement = xmlDoc.CreateElement("SIFRA");
                sifraElement.InnerText = "BGD";
                regijaElement.AppendChild(sifraElement);

                // Append the regija element to the root element
                regioniElement.AppendChild(regijaElement);

                // Append the root element to the XmlDocument
                xmlDoc.AppendChild(regioniElement);

                // Save the XmlDocument to a file
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

