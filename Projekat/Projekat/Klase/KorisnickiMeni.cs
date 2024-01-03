using Projekat.Interfejsi;
using System;
using System.Collections.Generic;
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
        public void HandleKorisnickiMeni()
        {
            String answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite zeljenu opciju:");
                Console.WriteLine("1 - Uvoz i provera podataka");
                Console.WriteLine("2 - Ispis podataka");
                Console.WriteLine("3 - Treca tacka");
                Console.WriteLine("X - Izlazak iz menija");

                answer = Console.ReadLine()!;

                switch (answer)
                {
                    case "1":
                        PrikazSvih();
                        break;
                    case "2":
                        ProveraPodataka();
                        break;
                    case "3":
                        // HandleSingleInsert();
                        break;
                    case "4":
                        // HandleMultipleInserts();
                        break;
                    case "5":
                        // HandleUpdate();
                        break;
                    case "6":
                        //HandleDelete();
                        break;

                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void ProveraPodataka()
        {
            throw new NotImplementedException();
        }

        private void PrikazSvih()
        {
            Console.WriteLine("Unesite zeljeni naziv fajla koje zelite da uvezete: ");
            String nazivFajla = Console.ReadLine()!;
            String putanja = Path.Combine(Environment.CurrentDirectory, nazivFajla);
            if (ValidnoIme(nazivFajla))
            {
                List<UlazniPodaci> podaci = uvoz.Uvezi(putanja);
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
