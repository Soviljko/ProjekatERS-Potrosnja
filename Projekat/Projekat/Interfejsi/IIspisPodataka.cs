using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Interfejsi
{
    public interface IIspisPodataka
    {
        void PrintConsumptionData(DateTime selectedDate, string selectedGeoArea);
    }
}
