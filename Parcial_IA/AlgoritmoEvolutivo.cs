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
        public Double[,] Genotipo = new Double[45, 9];
        private int Factor_Cruce, Factor_Mutacion;
        Random azar = new Random();

        public AlgoritmoEvolutivo()
        {

        }

        //Inicializar Varialbles
        public AlgoritmoEvolutivo(Double factor_cruce)
        {
            this.Factor_Cruce = Ciclo(factor_cruce);
            Factor_Mutacion = Solucion.Length - Factor_Cruce;
            for (int i = 0; i < Solucion.Length; i++)
            {
                if (i == 0)
                {
                    Solucion[i] = 3;
                }
                else
                {
                    Solucion[i] = Solucion[i - 1] + 0.1;
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
            if (number > 44)
            {
                number = 44;
            }
            return number;
        }

        //Evolucionador 
        public string Evolucionador()
        {
            Generar_Genotipo();

            Double Ant_promedio = 0, promedio = Promedio();
            while (promedio - Ant_promedio != 0)
            {
                Reiniciar();
                //cruce
                for (int i = 0; i < Factor_Cruce/2; i++)
                {
                    cruce();
                }

                //Mutacion
                for (int i = 0; i < Factor_Mutacion; i++)
                {
                    Mutacion();
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
            return (suma / cont);
        }

        //Reiniciar
        public void Reiniciar()
        {
            for (int i = 0; i < Solucion.Length; i++)
            {
                Genotipo[i, 0] = 0;
            }
        }

        //Metodo para la mutación genetica
        public void Mutacion()
        {
            int col_azar, col_cant = azar.Next(1, 4), cant = 0, sel = seleccion(), bin = 0;
            double[] padre = new double[6], hijo = new double[6];
            padre[0] = Genotipo[sel, 1]; padre[1] = Genotipo[sel, 2]; padre[2] = Genotipo[sel, 3]; padre[3] = Genotipo[sel, 4]; padre[4] = Genotipo[sel, 5]; padre[5] = Genotipo[sel, 6];

            do
            {
                hijo = padre;
                while (cant < col_cant)
                {
                    col_azar = azar.Next(0, 6);
                    if (padre[col_azar] == 0) { hijo[col_azar] = 1; } else { hijo[col_azar] = 0; }
                    cant++;
                }
                bin = BinToDec(hijo[0] + "" + hijo[1] + "" + hijo[2] + "" + hijo[3] + "" + hijo[4] + "" + hijo[5]);
                cant =0;
            } while (bin >= 45);

            if (min(Genotipo[sel, 8], Genotipo[bin, 8]) == false)
            {
                Genotipo[sel, 1] = hijo[0]; Genotipo[sel, 2] = hijo[1]; Genotipo[sel, 3] = hijo[2]; Genotipo[sel, 4] = hijo[3]; Genotipo[sel, 5] = hijo[4]; Genotipo[sel, 6] = hijo[5];
                Genotipo[sel, 8] = Genotipo[bin, 8];
            }

            Genotipo[sel, 0] = 1;
        }

        //Metodo para el cruce Genetico
        public void cruce()
        {
            int[] sel = new int[2], bin = new int[2];
            sel[0] = seleccion();
            do { sel[1] = seleccion(); } while (sel[0] == sel[1]);

            int col_azar, col_cant = azar.Next(1, 3), cant = 0;
            double[,] padre = new double[2, 6], hijo = new double[2, 6];

            for (int i = 0; i < sel.Length; i++)
            {
                padre[i, 0] = Genotipo[sel[i], 1];
                padre[i, 1] = Genotipo[sel[i], 2];
                padre[i, 2] = Genotipo[sel[i], 3];
                padre[i, 3] = Genotipo[sel[i], 4];
                padre[i, 4] = Genotipo[sel[i], 5];
                padre[i, 5] = Genotipo[sel[i], 6];
            }

            do
            {
                hijo = padre;
                while (cant < col_cant)
                {
                    col_azar = azar.Next(0, 6);
                    hijo[1, col_azar] = padre[0, col_azar];
                    hijo[0, col_azar] = padre[1, col_azar];
                    cant++;
                }
                for (int i = 0; i < bin.Length; i++)
                {
                    bin[i] = BinToDec(hijo[i, 0] + "" + hijo[i,1] + "" + hijo[i,2] + "" + hijo[i,3] + "" + hijo[i,4] + "" + hijo[i,5]);
                }
                cant = 0;
            } while (bin[0] >45 || bin[1] >45);

            if (bin[0] != 45 && bin[1] != 45)
            {
                for (int i = 0; i < sel.Length; i++)
                {
                    if (min(Genotipo[sel[i], 8], Genotipo[bin[i], 8]) == false)
                    {
                        Genotipo[sel[i], 1] = hijo[i, 0]; Genotipo[sel[i], 2] = hijo[i, 1]; Genotipo[sel[i], 3] = hijo[i, 2]; Genotipo[sel[i], 4] = hijo[i, 3]; Genotipo[sel[i], 5] = hijo[i, 4]; Genotipo[sel[i], 6] = hijo[i, 5];
                    }
                    Genotipo[sel[i], 0] = 1;
                }
            }
        }


        //Seleccion ruleta
        public int seleccion()
        {
            bool valor = true;
            int n = -1;
            while (valor)
            {
                n = azar.Next(0, 45);
                if (Genotipo[n, 0] == 0)
                {
                    valor = false;
                }
            }
            return n;
        }

        //Binario a decimal
        public int BinToDec(string binary)
        {
            int exponente = binary.Length - 1;
            int num_decimal = 0;

            for (int i = 0; i < binary.Length; i++)
            {
                if (int.Parse(binary.Substring(i, 1)) == 1)
                {
                    num_decimal = num_decimal + int.Parse(System.Math.Pow(2, double.Parse(exponente.ToString())).ToString());
                }
                exponente--;
            }
            return num_decimal;
        }

        //Metodo Min
        public bool min(double padre, double hijo)
        {
            if (padre < hijo)
            {
                return true;
            }
            return false;
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
