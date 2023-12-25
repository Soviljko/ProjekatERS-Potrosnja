using Projekat.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Interfejsi
{
    // Interfejs za upisivanje u bazu podataka 
    public interface IUpisPodataka
    {
        void WriteToDatabase(List<UlazniPodaci> data, string fileType, DateTime importTime, string location);
    }
}
