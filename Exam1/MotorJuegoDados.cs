using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    internal class MotorJuegoDados
 
    {
        private int tiros;
        private int[] numeros;
        private int dados;
        private int caras;
        private int min, max;
        private int dinenos;
        private int saldo;
        private Random rand;


        public MotorJuegoDados(int dados, int caras, int dineros)
        {
            this.dados = dados;
            this.caras = caras; 
            this.dinenos = dineros;
            saldo = dinenos;
            tiros = 0;
            rand = new Random();
            min = dados - 1;
            max = dados * caras;
            numeros = new int[max-min];

        }
        private void numerosUpdate(int numerillo)
        {
            if (!enRango( numerillo)) return;//linea innecesaria
            numeros[numerillo - dados]++;
        }

        public int tiraDado()
        {
            int tiro = rand.Next(dados, max+1);
            numerosUpdate(tiro);
            tiros++;
            return tiro;
        }

        // validaciones
        public bool enRango(int numero) 
        {
            return (numero > min && numero <= max);
        }
        public bool apuestaValida(int apuesta)
        {

            return (apuesta <= saldo && apuesta%10 ==0);
        }
        private bool isExtremo(int numerillo)
        {
            return (numerillo <= dados + 2) || (numerillo >= max - 2);
        }

        // estadisticas
        public int impares()//regresa la cantidad de tiradas que dieron a un numero impar
        {
            int sumatoria = 0;
            for (int i = dados; i <= max; i++)
            {
                if (i%2 != 0)
                {
                    sumatoria+= numeros[i-dados];
                }
            }
            return sumatoria;
        }
        public int pares()//regresa la cantidad de tiradas que dieron numer par
        {
            int sumatoria = 0;
            for (int i = dados; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    sumatoria += numeros[i - dados];
                }
            }
            return sumatoria;
        }
        public int tiradas { get { return tiros; } }
        public string menosTirado()
        {
            int menor= numeros[0];
            for (int i = 0; i< numeros.Length; i++)
            {
                if (numeros[i] < numeros[menor])
                {
                    menor = i;
                }
            }
            return $"Solo se ha tirado {menor+dados} unas {numeros[menor]} veces";
        }
        public string masTirado()
        {
            int mayor = numeros[0];
            for (int i = 0; i < numeros.Length; i++)
            {
                if (numeros[i] > numeros[mayor])
                {
                    mayor = i;
                }
            }
            return $"Se ha tirado mas veces el {mayor + dados} con unas {numeros[mayor]} veces";
        }
        public int numExtremos()
        {
             int sumatoria = 0;
            for (int i = dados; i <= max; i++)
            {
                if (isExtremo(i))
                {
                    sumatoria += numeros[i - dados];
                }
            }
            return sumatoria;
        }
        public int numMedios()
        {
            int sumatoria = 0;
            for (int i = dados; i <= max; i++)
            {
                if (!isExtremo(i))
                {
                    sumatoria += numeros[i - dados];
                }
            }
            return sumatoria;
        }

        public int restante { get { return saldo; } }
        public int balance() {
            return saldo - dinenos;
        }

        //  Apuestas
        public int apuestaAlpar(bool esPar,int  numerillo, int apuesta)
        {
            int multiplicador = 2;
            if ((numerillo % 2 == 0) == esPar) //verdadero si salio par y se aposto a que esPar==true o si salio impar y se aposto a que esPar==false.
            {
                apuesta *= multiplicador;
               
            }
            else
            {
             
                apuesta *= -1;
            }
            saldo += apuesta;
            return apuesta;
        }
        public int apuestaAlnum(int esperado, int respuesta, int apuesta)
        {
            int multiplicador =10;
            if(esperado == respuesta)
            {
                apuesta *= multiplicador;
            }
            else
            {
                apuesta *= -1;
            }
            saldo += apuesta;
            return apuesta;
        }
       
        public int apuestaAlExtremo(int numerillo, int apuesta)
        {
            int multiplicador = 8;
            if (isExtremo(numerillo))
            {
                apuesta *= multiplicador;
            }
            else
            {
                apuesta *= -1;
            }
            saldo += apuesta;
            return apuesta;

        }
        public int apuestaAlMedio(int numerillo, int apuesta)
        {
            int multiplicador = 4;
            if (!isExtremo(numerillo))
            {
                apuesta *= multiplicador;
            }
            else
            {
                apuesta *= -1;
            }
            saldo += apuesta;
            return apuesta;

        }
    }
}
