using Projekat.Interfejsi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projekat.Klase
{
    public class KorisnickiMeni
    {
        private static readonly IUvozPodataka uvoz = new XMLUvozPodataka();
        private static readonly IProveraPodataka provera = new ValidatorPodataka();
        private static readonly IUpisPodataka upis = new UpisUBazu();
        private static readonly IIspisPodataka ispis = new IspisPodataka();
        private static readonly IUvozRegiona uvozRegiona = new UvozXMLReg();
        private static readonly IRegioni regioni = new Region();
        public void HandleKorisnickiMeni()
        {
            String answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite zeljenu opciju:");
                Console.WriteLine("1 - Validacija,uvoz i upis podataka");
                Console.WriteLine("2 - Ispis podataka");
                Console.WriteLine("X - Izlazak iz menija");

                answer = Console.ReadLine()!;

                switch (answer)
                {
                    case "1":
                        PrikazSvih();
                        break;
                    case "2":
                        IspisPodataka();
                        break;
                    case "x":
                        break;
                    case "X":
                        break;
                    default:
                        Console.WriteLine("\nUnesite opciju iz menija!");
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void IspisPodataka()
        {
            DateTime selectedDate;
            string selectedGeoArea;

            Console.Write("Unesite datum (YYYY-MM-DD): ");
            string inputDate = Console.ReadLine();

            Console.Write("Unesite šifru geo-područja (npr. VOJ): ");
            selectedGeoArea = Console.ReadLine();

            if (DateTime.TryParseExact(inputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
            {
                // Poziv metode za ispis podataka
                ispis.PrintConsumptionData(selectedDate, selectedGeoArea);
            }
            else
            {
                Console.WriteLine("Neispravan format datuma. Molimo vas da unesete datum u formatu YYYY-MM-DD.");
            }
        }

        private void PrikazSvih()
        {
            Console.WriteLine("Unesite zeljeni naziv fajla koje zelite da uvezete: ");
            String nazivFajla = Console.ReadLine()!;
            String putanja = Path.Combine(Environment.CurrentDirectory, nazivFajla);
            if (ValidnoIme(nazivFajla))
            {
                List<UlazniPodaci> podaci = uvoz.Uvezi(putanja);
                List<UlazRegiona> regije = uvozRegiona.Uvezi("Regioni.xml");
                if (provera.Validacija(podaci, nazivFajla, Environment.CurrentDirectory))
                {
                    Console.WriteLine("Podaci su uspesno uvezeni.Ispis:\n");
                    int brojS = 1;

                    Console.WriteLine("Uvezeni podaci:");
                    Console.WriteLine();
                    foreach (UlazniPodaci podatak in podaci)
                    {
                        podatak.brojStavke = brojS;
                        Console.WriteLine(podatak);
                        brojS++;
                    }

                    if(upis.WriteToDatabase(podaci, nazivFajla, DateTime.Now, Environment.CurrentDirectory) == 0)
                    {
                        Console.WriteLine("Podaci uspesno upisanu u bazu.");
                    }
                    else
                    {
                        Console.WriteLine("Greska pri upisa podataka u bazu!");
                    }

                    Console.WriteLine("***************************");
                    
                    foreach(UlazniPodaci pod in podaci)
                    {
                        bool flag = true;
                        foreach (UlazRegiona reg in regije) { 
                            if(reg.SifraRegiona == pod.SifraGeoPodrucja) { flag = false; break; }
                        }
                        if (flag)
                        {
                            Console.WriteLine("Sifra regije:{0}", pod.SifraGeoPodrucja);
                            regioni.UnesiNovuRegiju(pod.SifraGeoPodrucja);
                            regije = uvozRegiona.Uvezi("Regioni.xml");
                        }
                    }
                    {

                        
                    }
                    

                    Console.WriteLine("**********************");
                    foreach (UlazRegiona reg in regije) {
                        Console.WriteLine("Sifra:{0}", reg.SifraRegiona);
                        Console.WriteLine("Naziv:{0}", reg.NazivRegiona);
                    }

                }
            } 
            else
            {
                Console.WriteLine("Nevalidan naziv fajla. Proverite format naziva fajla.");
            }
        }

        private static bool ValidnoIme(string imeFajla)
        {
            string sablon = @"^(prog|ostv)_(\d{4}_\d{2}_\d{2})\.xml$";
            return Regex.IsMatch(imeFajla, sablon);
        }
    }
}
