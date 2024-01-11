using Projekat.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Interfejsi
{
    internal interface IRegioni
    {
        List<UlazRegiona> UveziRegije(string filePath);

        void UnesiNovuRegiju(string sifra);
    }
}
