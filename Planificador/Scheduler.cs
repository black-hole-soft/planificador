/*
 * Creado por SharpDevelop.
 * Usuario: ggonzalez
 * Fecha: 27/10/2008
 * Hora: 07:41 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Planificador
{
	public class Scheduler
	{
		int j=0,q=20,time=300;
		Proceso[] nuevos=new Proceso[100];
		Queue listos=new Queue();
		Queue bloqueados=new Queue();
		Queue terminados=new Queue();
		Proceso ax,ejecucion=null;
		String[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));
		public void mostrarNuevos()
		{
			for(int i=0;i<j;i++)
			{
				Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor),colorNames[nuevos[i].color]);
				Console.SetCursorPosition(3,i+3);
				Console.Write(nuevos[i].nombre+"("+nuevos[i].lineas+")");
			}
		}
		public void borrarNuevos()
		{
			for(int i=0;i<j;i++)
			{
				Console.SetCursorPosition(3,i+3);
                Console.Write("               ");
			}
		}
		public void mostrarListos()
		{
			int i=0;
			foreach(Proceso x in listos)
			{
				Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor),colorNames[x.color]);
				Console.SetCursorPosition(18,i+3);
				Console.Write(x.nombre+"("+x.lineas+")");
				i++;
			}
		}
        public void borrarListos()
        {
            int i = 0;
            foreach (Proceso x in listos)
            {
                Console.SetCursorPosition(18, i + 3);
                Console.Write("               ");
                i++;
            }
        }
        public void mostrarBloqueados()
        {
            int i = 0;
            foreach (Proceso x in bloqueados)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorNames[x.color]);
                Console.SetCursorPosition(48, i + 3);
                Console.Write(x.nombre + "(" + x.lineas + ")");
                i++;
            }
        }
        public void borrarBloqueados()
        {
            int i = 0;
            foreach (Proceso x in bloqueados)
            {
                Console.SetCursorPosition(48, i + 3);
                Console.Write("               ");
                i++;
            }
        }
        public void mostrarTerminados()
        {
            int i = 0;
            foreach (Proceso x in terminados)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorNames[x.color]);
                Console.SetCursorPosition(63, i + 3);
                Console.Write(x.nombre + "(" + x.lineas + ")");
                i++;
            }
        }
        public void mostrarEjecucion()
        {
            if (ejecucion != null)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorNames[ejecucion.color]);
                Console.SetCursorPosition(33, 3);
                Console.Write(ejecucion.nombre + "(" + ejecucion.lineas + ")");
            }
        }
        public void borrarEjecucion()
        {
            Console.SetCursorPosition(33, 3);
            Console.Write("               ");
        }
        public void tick()
        {
            Thread.Sleep(time);
            if (bloqueados.Count > 0)
            {
                bool hd = true;
                foreach (Proceso x in bloqueados)
                {
                    if (x.cblok > 0)
                        x.cblok--;
                    if (hd)
                    {
                        ax = x;
                        hd = false;
                    }
                }
                if(ax.cblok==0)
                {
                    borrarBloqueados();
                    borrarListos();
                    ax = (Proceso)bloqueados.Dequeue();
                    ax.blok = 0;
                    listos.Enqueue(ax);
                    mostrarBloqueados();
                    mostrarListos();
                }
            }
            if (ejecucion == null)
            {
                if (listos.Count > 0)
                {
                    borrarListos();
                    ejecucion = (Proceso)listos.Dequeue();
                    mostrarListos();
                    mostrarEjecucion();
                }
            }
            else 
            {
                if (ejecucion.lineas > 0)
                {
                    if (q > 0)
                        ejecutar();
                    else
                    {
                        q = 20;
                        if (listos.Count > 0)
                        {
                            borrarListos();
                            borrarEjecucion();
                            listos.Enqueue(ejecucion);
                            //No se si es trancision completa o debo esperar al siguiente tick
                            if (listos.Count > 0)
                                ejecucion = (Proceso)listos.Dequeue();
                            else
                                ejecucion = null;
                            mostrarListos();
                            mostrarEjecucion();
                        }
                        else
                            ejecutar();
                    }
                }
                else
                    terminar();
            }
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "White");
            Console.SetCursorPosition(1, 22);
            Console.Write("           ");
            Console.SetCursorPosition(1, 22);
            Console.Write("Quantum: ");
            Console.Write((20-q));
        }
        public void ejecutar() 
        {
            if (ejecucion.lineas == ejecucion.err)
                terminar();
            else
            {
                if (ejecucion.lineas == ejecucion.blok)
                    bloquear();
                else
                {
                    ejecucion.lineas--;
                    q--;
                    borrarEjecucion();
                    mostrarEjecucion();
                }
            }
        }
        public void terminar()
        {
            q = 20;
            borrarListos();
            borrarEjecucion();
            terminados.Enqueue(ejecucion);
            if (listos.Count > 0)
                ejecucion = (Proceso)listos.Dequeue();
            else
                ejecucion = null;
            mostrarListos();
            mostrarEjecucion();
            mostrarTerminados();
        }
        public void bloquear() 
        {
            q = 20;
            borrarListos();
            borrarEjecucion();
            ejecucion.bblok = true;
            ejecucion.cblok = 10;
            //ejecucion.lineas--;
            bloqueados.Enqueue(ejecucion);
            if (listos.Count > 0)
                ejecucion = (Proceso)listos.Dequeue();
            else
                ejecucion = null;
            mostrarEjecucion();
            mostrarListos();
            borrarBloqueados();
            mostrarBloqueados();
        }
		public Scheduler(int mtd)
		{
			Console.Clear();
			if(archivos())
			{
				Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor),"Green");
                Console.WriteLine();
                Console.WriteLine("   Nuevos         Listos         Ejecucion      Bloqueados     Terminados");
				mostrarNuevos();
				Thread.Sleep(time);
				borrarNuevos();
				if(mtd==0)
					loadListosFCFS();
				if(mtd==1)
					loadListosSJF();
				if(mtd==2)
					loadListosPrioridades();
				mostrarListos();
                
                while(j!=terminados.Count)
                    tick();
			}
			else
				Console.WriteLine("Se encontraron errores en la definicion de los procesos");
			//Console.WriteLine(ax.lineas);
		}
        public void loadListosFCFS()
        {
            for (int i = 0; i < j; i++)
                listos.Enqueue(nuevos[i]);
        }
        public void loadListosSJF()
        {
            int x, y;
            for (x = 0; x < j - 1; x++)
                for (y = 0; y < j - x - 1; y++)
                    if (nuevos[y].lineas > nuevos[y + 1].lineas)
                    {
                        ax = nuevos[y];
                        nuevos[y] = nuevos[y + 1];
                        nuevos[y + 1] = ax;
                    }
            for (x = 0; x < j; x++)
                listos.Enqueue(nuevos[x]);
        }
        public void loadListosPrioridades()
        {
            int x, y;
            for (x = 0; x < j - 1; x++)
                for (y = 0; y < j - x - 1; y++)
                    if (nuevos[y].prior > nuevos[y + 1].prior)
                    {
                        ax = nuevos[y];
                        nuevos[y] = nuevos[y + 1];
                        nuevos[y + 1] = ax;
                    }
            for (x = 0; x < j; x++)
                listos.Enqueue(nuevos[x]);
        }
        public bool archivos()
        {
            bool correcto = true;
            String ruta;
            String archivo;
            String[] p;
            int i = 0;
            bool open = true;
            while (open)
            {
                try
                {
                    ruta = "p" + i + ".txt";
                    i++;
                    archivo = File.ReadAllText(ruta);
                    p = archivo.Split(new char[] { '\n', '\r' });
                    ax = new Proceso(p[0], int.Parse(p[2]), int.Parse(p[4]), int.Parse(p[6]), int.Parse(p[8]), int.Parse(p[10]));
                    if (ax.nombre == "error tipo")
                    {
                        Console.WriteLine("Existen parametros erroneos en {0}", ruta);
                        correcto = false;
                    }
                    else
                    {
                        nuevos[j] = ax;
                        j++;
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Los parametros deben ser enteros, solo el nombre no");
                    correcto = false;
                }
                catch (Exception e)
                {
                    open = false;
                }
            }
            return correcto;
        }
	}
}
