using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Klase
{
    internal class UlazRegiona
    {
       
        public string? SifraRegiona { get; set; }

        public string NazivRegiona { get; set; }

        

        public UlazRegiona()
        {
            
            SifraRegiona = string.Empty;
            NazivRegiona = string.Empty;
        }

        public UlazRegiona( string sgp,string nazReg)
        {
            
            SifraRegiona = sgp;
            NazivRegiona = nazReg;
        }

        public override string? ToString()
        {
            StringBuilder result = new StringBuilder();

            
            result.AppendLine($"{"Sifra Geografskog Podrucja"}: {SifraRegiona}");
            result.AppendLine($"{"Naziv Geografskog podrucja"}: {NazivRegiona}");
            result.AppendLine($"{new string('=', 35)}");

            return result.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj is UlazRegiona podaci &&
                   
                   SifraRegiona == podaci.SifraRegiona &&
                   NazivRegiona == podaci.NazivRegiona;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SifraRegiona,NazivRegiona);
        }
    }
}
