using Projekat.Interfejsi;
using Projekat.Klase;
using System.Text.RegularExpressions;

public class Program
{
    private static readonly KorisnickiMeni meni = new KorisnickiMeni();
    private static void Main(string[] args)
    {
        meni.HandleKorisnickiMeni();
    }

    
}

