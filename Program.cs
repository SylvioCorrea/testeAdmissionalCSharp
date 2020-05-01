using System;
using System.Collections.Generic;

namespace ProvaAdmissionalCSharpApisul
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> elevadores = new List<char>(new char[]{'A', 'B', 'C', 'D', 'E'});
            ElevadorService es = new ElevadorService("input.json", 16, elevadores);
            
            Console.WriteLine("Andar menos utilizado");
            foreach(int a in es.andarMenosUtilizado())
            {
                Console.Write(a);
            }
            Console.WriteLine("\n\nElevador menos frequentado");
            Console.WriteLine(es.elevadorMenosFrequentado().ToArray());
            Console.WriteLine("\nElevador mais frequentado");
            Console.WriteLine(es.elevadorMaisFrequentado().ToArray());
            Console.WriteLine("\nFluxo menos");
            Console.WriteLine(es.periodoMenorFluxoElevadorMenosFrequentado().ToArray());
            Console.WriteLine("\nFluxo mais");
            Console.WriteLine(es.periodoMaiorFluxoElevadorMaisFrequentado().ToArray());
            Console.WriteLine("\nPeriodo maior utilizacao conjunto");
            Console.WriteLine(es.periodoMaiorUtilizacaoConjuntoElevadores().ToArray());
            Console.WriteLine("\nA%");
            Console.WriteLine(es.percentualDeUsoElevadorA());
            Console.WriteLine("\nB%");
            Console.WriteLine(es.percentualDeUsoElevadorB());
            Console.WriteLine("\nC%");
            Console.WriteLine(es.percentualDeUsoElevadorC());
            Console.WriteLine("\nD%");
            Console.WriteLine(es.percentualDeUsoElevadorD());
            Console.WriteLine("\nE%");
            Console.WriteLine(es.percentualDeUsoElevadorE());
            Console.WriteLine();
            Console.WriteLine(
                es.percentualDeUsoElevadorA() +
                es.percentualDeUsoElevadorB() +
                es.percentualDeUsoElevadorC() +
                es.percentualDeUsoElevadorD() +
                es.percentualDeUsoElevadorE()
            );
        }

        
    }
}
