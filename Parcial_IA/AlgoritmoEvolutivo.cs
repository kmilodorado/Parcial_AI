using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_IA
{
    class AlgoritmoEvolutivo
    {
        private Double[] Solucion = new Double[45];
        private Double[] Contradominio = new Double[45];
        private Double[,] Genotipo = new Double[45, 9];
        private int Factor_Cruce, Factor_Mutacion;

        //Inicializar Varialbles
        public AlgoritmoEvolutivo(Double factor_cruce)
        {
            this.Factor_Cruce = Ciclo(factor_cruce);
            Factor_Mutacion=Solucion.Length- Factor_Cruce;
            for (int i = 0; i <Solucion.Length; i++)
            {
                if (i==0)
                {
                    Solucion[i] = 3;
                }
                else
                {
                    Solucion[i] = Solucion[i-1]+0.1;
                }
                Contradominio[i] = Formula(Solucion[i]);
            }
        }

        //Formula 
        private Double Formula(Double Valor)
        {
            return Math.Pow(Valor, 3) - (12 * Math.Pow(Valor, 2)) + (45 * Valor) - 30;
        }

        //Regla de tres simple para los ciclo de mutacion
        public int Ciclo(double Fac_Cruce)
        {
            double regla = (45 * Fac_Cruce) / 100;
            int number = 0;
            if (regla % 2 != 0)
            {
                regla++;
            }
            number = (int)Math.Round(regla, 0);
            if (number>44)
            {
                number = 44;
            }
            return number;
        }

        //Evolucionador 
        public string Evolucionador()
        {
            Generar_Genotipo();
            
            Double Ant_promedio=0,promedio =Promedio();
            while (promedio-Ant_promedio==0)
            {
                Reiniciar();
                //cruce
                for (int i = 0; i < Factor_Cruce; i++)
                {

                }

                //Mutacion
                for (int i = 0; i < Factor_Mutacion; i++)
                {

                }

                Ant_promedio = promedio;
                promedio = Promedio();
            }
            return "";
        }


        public void Generar_Genotipo()
        {
            for (int i = 0; i < Solucion.Length; i++)
            {
                String cadena = System.Convert.ToString((i + 1), 2);
                if (cadena.Length < 6)
                {
                    for (int j = cadena.Length - 1; j < 5; j++)
                    {
                        cadena = "0" + cadena;
                    }
                }
                var chars = cadena.ToCharArray();
                Genotipo[i, 0] = 0;
                Genotipo[i, 1] = double.Parse(chars[0] + "");
                Genotipo[i, 2] = double.Parse(chars[1] + "");
                Genotipo[i, 3] = double.Parse(chars[2] + "");
                Genotipo[i, 4] = double.Parse(chars[3] + "");
                Genotipo[i, 5] = double.Parse(chars[4] + "");
                Genotipo[i, 6] = double.Parse(chars[5] + "");
                Genotipo[i, 7] = Solucion[i];
                Genotipo[i, 8] = Contradominio[i];
            }
        }

        //Promedio
        public double Promedio()
        {
            int cont = 0;
            double suma = 0;
            for (int i = 0; i < Solucion.Length; i++)
            {
                suma = suma + Genotipo[i, 8];
                cont++;
            }
            return (suma/cont);
        }

        //Reiniciar
        public void Reiniciar()
        {
            for (int i = 0; i < Solucion.Length; i++)
            {
                Genotipo[i, 0] = 0;
            }
        }

        public void Mutacion()
        {
            Random azar = new Random();
            int col_azar,col_cant= azar.Next(1, 4), cant=0,sel=seleccion();
            
            while (cant<col_cant)
            {
                if (true)
                {

                }
            }
            int col= azar.Next(1, 7);
        }

        public int seleccion()
        {
            Random azar = new Random();
            bool valor = true;
            int n=-1;
            while (valor)
            {
                n = azar.Next(0, 45);
                if (Genotipo[n, 0]==0)
                {
                    valor=false;
                }
            }
            return n;
        }

        public Double[] MostrarSolucion()
        {
            return Solucion;
        }

        public Double[] Mostrar()
        {
            return Contradominio;
        }
    }
}
