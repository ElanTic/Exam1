using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    internal class JuegoDados
    {
        MotorJuegoDados motor;
        public JuegoDados()
        {
            motor = new MotorJuegoDados(2,6,300);
            //prueba();
            principal();
            //prueba();
        }

        public void principal()
        {
            Console.WriteLine("Casino [1]Apostar, [2]Estadisticas, [0] retirarse.");
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "0":
                    retirarse();
                    return;
                case "1":
                    apostar();
                    break;
                case"2":
                    imprimeEstadisticas();
                    break ;
                default:
                    Console.WriteLine("No existe esa opcon");
                    break;
            }
            if (motor.restante > 0)
            {
                principal();
            }
            else
            {
                retirarse();
            }
        }
        public void apostar()
        {
            Console.WriteLine("Apuesta [1]A un numero apuestaX10,  [2]Extremo apuestaX8," +
                " [3]Medio apuestaX4, [4]par o impar apuestaX2, [0] regresar.");
            string opcion = Console.ReadLine();
            int apuesta = 0;
            int tiro=0;
            switch (opcion)
            {
                case "0":
                    return;
                case "1":
                    //nums
                    apuesta  = validaMonto();
                    if (apuesta == 0) break;
                    //pide el numero
                    int numE = pedirUnNum();
                    tiro = tira();
                    //tira el dado
                    resultados(motor.apuestaAlnum(numE,tiro,apuesta));
                    //saca resultado
                    break;
                case "2":
                    //ext
                    apuesta = validaMonto();
                    if (apuesta == 0) break;
                    tiro = tira();
                    resultados(motor.apuestaAlExtremo(tiro, apuesta));
                    break;
                case "3":
                    //med
                    apuesta = validaMonto();
                    if (apuesta == 0) break;
                    tiro = tira();
                    resultados(motor.apuestaAlMedio(tiro, apuesta));
                    break;
                case "4":
                    //par
                    apuesta = validaMonto();
                    if (apuesta == 0) break;
                    bool res  = pedirBool();
                    tiro = tira();
                    resultados(motor.apuestaAlpar(res,tiro, apuesta));
                    break;
                default:
                    Console.WriteLine("No existe esa opcon");
                    break;
            }
            if(motor.restante <= 0)
            {
                return;
            }
            apostar();
        }
        public void retirarse()
        {
            int final = motor.balance();
            if (final < 0)
            {
                Console.WriteLine($"total:{motor.restante}.00$, pero perdiste {final*-1}.00$");
            }
            else Console.WriteLine($"total:{motor.restante}.00$, Ganaste {final}.00$"); ;
            return;
        }

        public void resultados(int valor)
        {
            if (valor < 0)
            {
                Console.WriteLine("Lastima");
            }
            else
            {
                Console.WriteLine($"Felicidades ganaste {valor}.00$");
            }
        }
        public void imprimeEstadisticas()
        {
            Console.WriteLine($"Estadisticas:\nhas ganado {motor.balance()}.00$,\ntiraste los dados {motor.tiradas} veces,\n" +
                $"{motor.masTirado()},\n{motor.menosTirado()},\n{motor.numExtremos()} de los tiros son extremos,\n" +
                $"{motor.numMedios()} de los tiros son medios,\n{motor.pares()} de los tiros son numeros pares,\n" +
                $"{motor.impares()} de los tiros son numeros impares.");
        }

        public int tira()
        {
            Console.WriteLine("tira cuando estes listo [INTRO] tirar");
            Console.ReadLine();
            int tiro = motor.tiraDado();
            Console.WriteLine(tiro);
            return tiro;
        }

        public int validaMonto()
        {
            Console.WriteLine($"Apuesta solo numeros enteros multiplos de 10, disponible {motor.restante}, cancelar[0] : ");
            int resul;
            do
            {
                resul = leerEntero();
            }
            while (resul<0) ;
            if(resul==0)return 0;
            else
            {
                if (!motor.apuestaValida(resul))
                {
                    Console.WriteLine("Monto incompatible o el saldo no es suficiente.");
                    return validaMonto();
                }
                else
                {
                    return resul;
                }
            }
        }
        public int pedirUnNum()
        {
            Console.WriteLine($"A que numero?: ");
            int resul;
            do
            {
                resul = leerEntero();
                if (!motor.enRango(resul))
                {
                    Console.WriteLine("intenta de nuevo.");
                }
                else break;
            }
            while (true);
            return resul;
        }
        public bool pedirBool()
        {
            Console.WriteLine($"[1] impar, [2] par");
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    return false;
                    break;
                case "2":
                    return true;
                    break;
                default:
                    Console.WriteLine("No existe esa opcon");
                    return pedirBool();
            }
        }
        public int leerEntero()
        {
            int input;
            try
            {
                input = int.Parse(Console.ReadLine());
                return input;
            }
            catch { return -1; }
        }
    }
}
