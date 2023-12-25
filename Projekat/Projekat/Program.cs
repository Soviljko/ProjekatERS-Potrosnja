using Projekat.Interfejsi;
using Projekat.Klase;

public class Program
{
    private static void Main(string[] args)
    {
        string relativnaPutanja = Path.Combine("prog_2020_05_07.xml");

        string trenutniDirektorijum = Directory.GetCurrentDirectory();

        string putanjaFajla = Path.Combine(trenutniDirektorijum, relativnaPutanja);

        // Console.WriteLine(putanjaFajla);

        IUvozPodataka XMLUvozac = new XMLUvozPodataka();
        List<UlazniPodaci> uvezeno = XMLUvozac.Uvezi(putanjaFajla);
        int brojS = 1;

        Console.WriteLine("Uvezeni podaci:");
        Console.WriteLine();
        foreach(UlazniPodaci podatak in uvezeno)
        {
            podatak.brojStavke = brojS;
            Console.WriteLine(podatak);
            brojS++;
        }
    }
}