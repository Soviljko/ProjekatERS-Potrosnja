using Projekat.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Interfejsi
{
    // Interfejs za validaciju podataka
    public interface IProveraPodataka
    {
        bool ValidateData(List<UlazniPodaci> data, string fileName, string location);
    }
}
