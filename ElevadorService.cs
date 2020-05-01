using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ProvaAdmissionalCSharpApisul
{

    public class ElevadorService : IElevadorService
    {

        private List<RespostaPesquisa> pesquisa;
        private int andares;
        private List<char> elevadores;

        public ElevadorService(string arquivo, int andares, List<char> elevadores)
        {
            string json = File.ReadAllText(arquivo);
            pesquisa = JsonConvert.DeserializeObject<List<RespostaPesquisa>>(json);
            this.andares = andares;
            this.elevadores = new List<char>(elevadores);
        }

        public List<int> andarMenosUtilizado()
        {
            //array para guardar o numero de visitas a cada andar
            int[] arrAndares = new int[andares];

            foreach(RespostaPesquisa r in pesquisa)
            {
                arrAndares[r.andar] += 1;
            }

            int min = arrAndares.Min();

            List<int> resposta = new List<int>();
            for(int i = 0; i<andares; i++)
            {
                if(arrAndares[i] == min)
                {
                    resposta.Add(i);
                }
            }

            return resposta;
        }
        
        public List<char> elevadorMaisFrequentado()
        {
            return funcaoGenericaDeFrequencia( array => array.Max() );
        }

        public List<char> elevadorMenosFrequentado()
        {
            return funcaoGenericaDeFrequencia( array => array.Min() );
        }

        private List<char> funcaoGenericaDeFrequencia(Func<IEnumerable<int>, int> lambda )
        {
            //Dicionario para guardar o numero de utilizacoes de cada elevador
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach(char e in elevadores)
            {
                dict.Add(e, 0);
            }

            foreach(RespostaPesquisa r in pesquisa)
            {
                dict[r.elevador] = dict[r.elevador] + 1;
            }

            int maiorOuMenor = lambda(dict.Values.ToArray());

            List<char> resposta = new List<char>();

            foreach(var keyValuePair in dict)
            {
                if(keyValuePair.Value == maiorOuMenor)
                {
                    resposta.Add(keyValuePair.Key);
                }
            }

            return resposta;
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            return funcaoGenericaDeFluxo(elevadorMenosFrequentado(), arr => arr.Min() );
        }

        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            return funcaoGenericaDeFluxo(elevadorMaisFrequentado(), arr => arr.Max() );
        }
        
        private List<char> funcaoGenericaDeFluxo(List<char> elevadoresRelevantes, Func<IEnumerable<int>, int> lambda)
        {
            char[] turnos = {'M', 'V', 'N'};
            
            //Dicionario para guardar o numero de utilizacoes de cada elevador em cada turno
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach(char t in turnos)
            {
                dict.Add(t, 0);
            }

            foreach(RespostaPesquisa rp in pesquisa)
            {
                if(elevadoresRelevantes.Contains(rp.elevador))
                {
                    dict[rp.turno] = dict[rp.turno] + 1;
                }
            }

            int maiorOuMenor = lambda(dict.Values.ToArray());

            List<char> resposta = new List<char>();
            foreach(var keyValuePair in dict)
            {
                if(keyValuePair.Value == maiorOuMenor)
                {
                    resposta.Add(keyValuePair.Key);
                }
            }
            return resposta;
        }

        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            char[] turnos = {'M', 'V', 'N'};
            
            //Dicionario para guardar o numero de utilizacoes de cada elevador em cada turno
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach(char t in turnos)
            {
                dict.Add(t, 0);
            }

            foreach(RespostaPesquisa rp in pesquisa)
            {
                dict[rp.turno] = dict[rp.turno] + 1;
            }

            int max = dict.Values.ToArray().Max();

            List<char> resposta = new List<char>();
            foreach(var keyValuePair in dict)
            {
                if(keyValuePair.Value == max)
                {
                    resposta.Add(keyValuePair.Key);
                }
            }
            return resposta;
        }

        public float percentualDeUsoElevadorA()
        {
            return percentualDeUsoElevadorX('A');
        }

        public float percentualDeUsoElevadorB()
        {
            return percentualDeUsoElevadorX('B');
        }

        public float percentualDeUsoElevadorC()
        {
            return percentualDeUsoElevadorX('C');
        }

        public float percentualDeUsoElevadorD()
        {
            return percentualDeUsoElevadorX('D');
        }

        public float percentualDeUsoElevadorE()
        {
            return percentualDeUsoElevadorX('E');
        }

        private float percentualDeUsoElevadorX(char elevador)
        {
            int usosTotais = pesquisa.Count();
            int usosElevador = pesquisa.Aggregate( 0,
                (accum, resposta) => resposta.elevador == elevador ? accum+1 : accum );
                
            return (float)usosElevador/usosTotais*100.0F;
        }

    }

}