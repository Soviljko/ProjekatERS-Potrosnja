using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Klase
{
    public class UlazniPodaci
    {
        public int Sat { get; set; }
        public double IznosPotrosnje { get; set; }
        public string? SifraGeoPodrucja { get; set; }

        public int brojStavke { get; set; }

        public UlazniPodaci()
        {
            Sat = 0;
            IznosPotrosnje = 0;
            SifraGeoPodrucja = string.Empty;
        }

        public UlazniPodaci(int sat, double iznospotrosnje, string sgp)
        {
            Sat = sat;
            IznosPotrosnje = iznospotrosnje;
            SifraGeoPodrucja = sgp;
        }

        public override string? ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"{"Stavka"} {brojStavke}");
            result.AppendLine($"{"Sat"}: {Sat}");
            result.AppendLine($"{"Iznos Potrosnje"}: {IznosPotrosnje} mW/h");
            result.AppendLine($"{"Sifra Geografskog Podrucja"}: {SifraGeoPodrucja}");
            result.AppendLine($"{new string('=', 35)}");

            return result.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj is UlazniPodaci podaci &&
                   Sat == podaci.Sat &&
                   IznosPotrosnje == podaci.IznosPotrosnje &&
                   SifraGeoPodrucja == podaci.SifraGeoPodrucja;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sat, IznosPotrosnje, SifraGeoPodrucja);
        }
    }
}
