/*
 * Creado por SharpDevelop.
 * Usuario: ggonzalez
 * Fecha: 27/10/2008
 * Hora: 07:42 p.m.
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
	public class Proceso  
	{
		public String nombre;
		public int lineas,color,prior,err,blok;
		public int cblok;
		public bool bblok;
		public Proceso sig;
		public bool letra(char ltr) 
        {
            if (ltr >= 'a' && ltr <= 'z')
                return true;
            if (ltr >= 'A' && ltr <= 'Z')
                return true;
            return false;
        }
		public Proceso(String n,int l,int c,int p,int e,int b)
		{
			bool good=true;
			bool v=true;
			for(int i=0;i<n.Length-1&&v==true;i++)
				if(!letra(n[i]))
					v=false;
			if(v)
				nombre=n;
			else{
				good=false;
				Console.WriteLine("El nombre del proceso no es válido");
			}
			if(l>0)
				lineas=l;
			else{
				good=false;
				Console.WriteLine("El # de lineas debe ser mayor a 0");
			}
			if(c>0&&c<15)
				color=c;
			else{
				good=false;
				Console.WriteLine("El color debe ser de 1 a 14");
			}
			if(p>=0)
				prior=p;
			else{
				good=false;
				Console.WriteLine("La prioridad debe ser mayor o igual a 0");
			}
			if(e>=0&&e<=lineas)
				err=e;
			else{
				good=false;
				Console.WriteLine("El error debe ser de 0 a {0}",lineas);
			}
			if(b>=0&&b<=lineas)
				blok=b;
			else{
				good=false;
				Console.WriteLine("El bloqueo debe ser de 0 a {0}",lineas);
			}
			if(!good)
				nombre="error tipo";
			cblok=0;
			bblok=false;
		}
	}
}
