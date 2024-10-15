

using System;

namespace Ejercicio1.Models
{
    [Serializable]
    public class Persona:IComparable
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }

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
    }
}
