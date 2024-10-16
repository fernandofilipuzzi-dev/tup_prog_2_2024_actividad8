
using System;
using System.Security.Permissions;

namespace Ejercicio1.Models
{
    [Serializable]
    public class Cuenta :IComparable
    {
        public int Numero { get; set; }
        public double Saldo { get; set; }
        public DateTime Fecha { get; set; }
        public Persona Titular { get; set; }

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
    }
}
