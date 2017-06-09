using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_IA
{
    class Program
    {

        static void Main(string[] args)
        {
            int Fac_Cruce;
            do
            {
                try
                {
                    Console.WriteLine("Ingresar el porcentaje del cruce 0 a 100");
                    Fac_Cruce = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Fac_Cruce = -1;
                    Console.WriteLine("Solo numero de 0 a 100");
                }
            } while (Fac_Cruce<0||Fac_Cruce>100);

            //Console.WriteLine(new AlgoritmoEvolutivo().BinToDec("111111"));
            
            AlgoritmoEvolutivo Evl = new AlgoritmoEvolutivo(Fac_Cruce);
            Evl.Evolucionador();
            Console.ReadKey();
        }

    }
}
