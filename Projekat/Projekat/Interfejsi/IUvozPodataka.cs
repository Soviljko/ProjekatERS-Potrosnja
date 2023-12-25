using Projekat.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Interfejsi
{
    // Interfejs za uvoz podataka
    public interface IUvozPodataka
    {
        List<UlazniPodaci> Uvezi(string filePath);
    }
}
