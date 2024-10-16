using System;
using System.Collections.Generic;
using System.Net;

namespace Ejercicio2.Models
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
        public int CantidadClientes
        {
            get { return clientes.Count; }
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

        public Persona VerCliente(int idx)
        {
            if (idx >= 0 && idx < CantidadClientes)
                return clientes[idx];
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

        public bool ImportarCuenta(int numero, double saldo, DateTime fecha, Persona persona)
        {
            Persona cliente = VerClientePorDNI(persona.DNI);//lo tengo que hacer porque el banco maneja sus clientes.
            //en el momento que incorpora la persona es un cliente, antes no.

            if (cliente == null)
            {
                cliente = new Persona(persona.DNI, persona.Nombre);
                clientes.Add(cliente);
            }

            //verifico que no exista la cuenta-si existe es porque esta duplicada en el archivo
            Cuenta nueva = VerCuentaPorNumero(numero);
            if (nueva != null) return false;
            
            nueva=new Cuenta(numero, cliente, fecha, saldo); //fecha y saldo no lo maneja el banco , porque tengo inyectarlo en el contexto con ese estado
            cuentas.Add(nueva);

            return true;
        }
    }
}
