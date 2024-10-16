
using System;

namespace Ejercicio2.Models
{
    [Serializable]
    public class Cuenta :IComparable, IExportable
    {
        public int Numero { get; set; }
        public double Saldo { get; set; }
        public DateTime Fecha { get; set; }
        public Persona Titular { get; set; }

        public Cuenta() { }

        public Cuenta(int numero, Persona cliente) 
        {
            Numero = numero;
            Fecha = DateTime.Today;
            Saldo = 0;
            Titular = cliente;
        }

        public Cuenta(int numero, Persona cliente, DateTime fecha, double saldo)
        {
            Numero = numero;
            Fecha = fecha;
            Saldo = saldo;
            Titular = cliente;
        }

        public int CompareTo(object obj)
        { 
            Cuenta cuenta = obj as Cuenta;

            if (cuenta != null)
                return cuenta.Numero.CompareTo(Numero);

            return 1;
        }

        public void Leer(string linea)
        {
            string[] campos=linea.Split(';');
            
            Numero =Convert.ToInt32( campos[0].Trim() );
            Saldo = Convert.ToDouble( campos[3].Trim() );
        }

        public string Escribir()
        {
            string linea=$"{Titular.Escribir()};{Numero};{Saldo:f2}";
            return linea;
        }
    }
}
