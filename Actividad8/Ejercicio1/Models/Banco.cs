using System;
using System.Collections.Generic;

namespace Ejercicio1.Models
{
    [Serializable]
    public class Banco
    {
        public List<Cuenta> cuentas=new List<Cuenta>();
        public List<Persona> clientes = new List<Persona>();

        public int CantidadCuentas 
        {
            get { return cuentas.Count;  }
        }

        public Cuenta AgregarCuenta(int dni, string nombre, int numeroCuenta)
        {
            Persona cliente = VerClientePorDNI(dni);

            if (cliente == null)
            {
                cliente = new Persona(dni, nombre);
                clientes.Add(cliente);
            }

            Cuenta nueva=new Cuenta(numeroCuenta, cliente);
            cuentas.Add(nueva);

            return nueva;
        }

        public Cuenta VerCuenta(int idx)
        {
            if (idx >= 0 && idx< CantidadCuentas)
                return cuentas[idx];
            return null;
        }

        public Cuenta VerCuentaPorNumero(int numeroCuenta)
        {
            Cuenta cuenta=null;
            int idx = cuentas.BinarySearch(new Cuenta(numeroCuenta,null));
            if (idx >=0) cuenta = cuentas[idx];
            return null;
        }

        public Persona VerClientePorDNI(int dni)
        {
            clientes.Sort();
            Persona cliente = null;
            int idx = clientes.BinarySearch(new Persona(dni, ""));
            if (idx >= 0) cliente = clientes[idx];
            return null;
        }
    }
}
