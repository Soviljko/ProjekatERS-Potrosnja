using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;
using Projekat.Klase;
using Moq;
using Projekat.Interfejsi;

namespace Projekat.Test
{
    [TestFixture]
    public class RegionTest
    {
        [Test]
        public void UnesiNovuRegiju_PozitivanTest()
        {
            // Zapamti trenutnu konzolu
            var originalConsoleOut = Console.Out;

            try
            {
                // Postavi virtuelnu konzolu
                using (var consoleOutput = new StringWriter())
                {
                    Console.SetOut(consoleOutput);
                    
                    IRegioni region = new Region();

                    // Simuliraj unos sa tastature
                    using (var consoleInput = new StringReader("NazivRegiona"))
                    {
                        Console.SetIn(consoleInput);

                        region.UnesiNovuRegiju("BMK");

                        // Proveri izlaz na konzoli
                        var consoleOutputText = consoleOutput.ToString();
                        Assert.That(consoleOutputText, Does.Contain("uspesno upisani podaci za region u XML fajl."));
                    }
                }
            }
            finally
            {
                Console.SetOut(originalConsoleOut);
            }
        }
    }
}
