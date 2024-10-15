

using System;

namespace Ejercicio2.Models
{
    [Serializable]
    public class Persona:IComparable, IExportable
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }

        public Persona() { }

        public Persona(int dni, string nombre) 
        {
            DNI = dni;
            Nombre = nombre;
        }

        public int CompareTo(object obj)
        { 
            Persona otro= obj as Persona;   
            if(otro != null) return this.DNI.CompareTo(otro.DNI);
            return 1;
        }

        public void Leer(string linea)
        {
            string[] campos = linea.Split(';');
            DNI = Convert.ToInt32(campos[0].Trim());
            Nombre = campos[1].Trim();
        }

        public string Escribir()
        {
            return $"{DNI};{Nombre}";
        }
    }
}
