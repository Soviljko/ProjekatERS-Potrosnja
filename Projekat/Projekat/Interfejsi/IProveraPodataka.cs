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
        bool Validacija(List<UlazniPodaci> podaci, string imeFajla, string lokacija);
    }
}
