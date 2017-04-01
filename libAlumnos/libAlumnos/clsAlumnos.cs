using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace libAlumnos
{
    public class clsAlumnos : clsListaMaterias
    {   //Propiedades
        public string strNumeroControl;
        public string strNombre;
        public string strCarrera;

        //Descripción: Agrega los datos del alumno al final del archivo "Datos.txt"
        public void Insertar()
        {
            //Abre el archivo para agregar texto al final del mismo (escritura), 
            //en caso de que el archivo no exista lo Crea 
            StreamWriter sw = new StreamWriter("Datos.txt",true);
            //Inserta datos alumno (renglón comienza con A)
            //Ejemplo: A,is14110007,James Bond,ISC
            string strAux;
            strAux = "A," + this.strNumeroControl + ',' + this.strNombre + ',' + this.strCarrera;
            sw.WriteLine(strAux);
            //Inserta datos de las materias del alumno (renglón comienza con M)
            //Ejemplo: M,001,Tópicos Avanzados Prog.,8,3
            foreach (clsMaterias mat in lstMaterias)
            {
                strAux = "M," + mat.strClave + "," + mat.strNombre
                        + "," + mat.intCreditos + "," + mat.intSemestre;
                sw.WriteLine(strAux);
            }
            //Cierra el archivo
            sw.Close();
        }

        //Descripción: Busca un Alumno por su número de control en el archivo de texto
        //             en caso de que lo encuentre carga los datos del alumno
        //             y los datos de sus materias
        public bool Cargar(string pNumControl)
        {
            //Abre el archivo para lectura y lo carga en 
            //en memoria en la variable sr
            StreamReader sr;
            sr = new StreamReader("Datos.txt");
            //lee cada linea en busca del alumno
            string strLinea;
            bool blnBuscandoAlumno = true;
            bool blnEncontrado = false;
            do{
                strLinea = sr.ReadLine();
                if (strLinea == null)
                {
                    //está al final del archivo o no tiene datos
                    //no hace nada
                }
                else
                {
                    string[] datos = strLinea.Split(',');
                    if (blnBuscandoAlumno)
                    {
                        //esta buscando al Alumno y si lo encuentra carga sus datos
                        //revisa si la linea tiene datos del alumno buscado
                        if (datos[0] == "A" && pNumControl.Equals(datos[1]))
                        {
                            strNumeroControl = datos[0];
                            strNombre = datos[1];
                            strCarrera = datos[2];
                            blnBuscandoAlumno = false;
                            blnEncontrado = true;
                        }
                    }
                    else
                    {
                        //esta cargando las materias del alumno
                        if (datos[0] == "M")
                        {
                            clsMaterias objM = new clsMaterias();
                            objM.strClave = datos[1];
                            objM.strNombre = datos[2];
                            objM.intCreditos = int.Parse(datos[3]);
                            objM.intSemestre = int.Parse(datos[4]);
                            this.Add(objM);
                        }
                        else
                        {
                            //comienza con "A", son datos de otro alumno y se sale
                            break;
                        }
                    }
                }
            } while (strLinea != null);
            sr.Close();
            return blnEncontrado;
        }

        //Descripción: Busca un alumno por su número de control y si lo encuentra lo borrar
        public bool Borrar(string pNumControl)
        {
            //Abre el archivo para lectura y lo carga en 
            //en memoria en la variable sr
            StreamReader sr;
            sr = new StreamReader("Datos.txt");
            //lee cada linea en busca del alumno que va a borrar
            string strLinea;
            bool blnBuscandoAlumno = true;
            bool blnBorrado = false;

            //Crea archivo temporal donde irá insertando los datos de los alumnos
            //excepto los datos del alumno que se está borrando
            StreamWriter sw = new StreamWriter("Temp.txt", false);
            do
            {
                strLinea = sr.ReadLine();
                if (strLinea == null)
                {
                    //está al final del archivo o no tiene datos
                    //no hace nada
                }
                else
                {
                    string[] datos = strLinea.Split(',');
                    bool blnEscribirLinea = true;
                    if (blnBuscandoAlumno)
                    {
                        //esta buscando al Alumno
                        //revisa si la linea tiene datos del alumno buscado
                        if (datos[0] == "A" && pNumControl.Equals(datos[1]))
                        {
                            blnBuscandoAlumno = false;
                            blnBorrado = true;
                            blnEscribirLinea = false;
                        }
                    }
                    else
                    {
                        //esta cargando las materias del alumno borrado
                        if (datos[0] == "M")
                        {
                            blnEscribirLinea = false;
                        }
                        else
                        {
                            //comienza con "A", son datos de otro alumno y se sale
                            blnBuscandoAlumno = true;
                        }
                    }
                    if (blnEscribirLinea)
                    {
                        sw.WriteLine(strLinea);
                    }
                }
            } while (strLinea != null);
            sw.Close();
            sr.Close();

            //Borra archivo que contenia todos los alumnos
            //y lo reemplaza por el que no tiene al alumno borrado
            File.Delete("Datos.txt");
            File.Move("Temp.txt", "Datos.txt");
            return blnBorrado;
        }

        //Descripción: Borra al alumno a modificar y lo inserta
        //             con la nueva información
        public void Modificar() {
            this.Borrar(this.strNumeroControl);
            this.Insertar();
        }
    }
}
