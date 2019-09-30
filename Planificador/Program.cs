/*
 * Creado por SharpDevelop.
 * Usuario: ggonzalez
 * Fecha: 15/10/2008
 * Hora: 08:56 p.m.
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
	class Program
	{
		public static void Main(string[] args)
		{
			String opc;
			Scheduler sch;
			bool ciclar=true;
			Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor),"Cyan");
			Console.SetCursorPosition(34,10);
			Console.WriteLine("PLANIFICADOR");
			Console.ReadKey();
			Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor),"Blue");
			while(ciclar)
			{
                Console.Clear();
				Console.Write("\n\n   Elige metodo de planificacion\n\n" +
				              "   1.- FCFS\n" +
				              "   2.- SJF\n" +
				              "   3.- Prioridades\n\n" +
				              "   Metodo: ");
				opc=Console.ReadLine();
				switch(opc){
					case "1":sch=new Scheduler(0);ciclar=false;
							break;
					case "2":sch=new Scheduler(1);ciclar=false;
							break;
					case "3":sch=new Scheduler(2);ciclar=false;
							break;
				}
			}
			Console.ReadKey();
		}
	}
}