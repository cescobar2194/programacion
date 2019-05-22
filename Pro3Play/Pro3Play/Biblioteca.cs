using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro3Play
{
    class Biblioteca
    {
        string codigo;
        string nombre;
        string direccion;
        string portada;

        public string Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }

        public string Portada
        {
            get
            {
                return portada;
            }

            set
            {
                portada = value;
            }
        }
    }
}
