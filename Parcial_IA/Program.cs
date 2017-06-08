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
            int Fac_Cruce=0;
            do
            {
                try
                {
                    Console.WriteLine("Ingresar el porcentaje del cruce");
                    Fac_Cruce = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Fac_Cruce = -1;
                    Console.WriteLine("Solo numero de 0 a 100");
                }
            } while (Fac_Cruce<0||Fac_Cruce>100);

            
            //AlgoritmoEvolutivo Evl = new AlgoritmoEvolutivo(Fac_Cruce/100);
            Console.ReadKey();
        }

    }
}
