package Fundamentos;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Principal {

static	String[][] productos;

static String ventas[][]; 
static int posventas = -1; 
static int tamventas = 100; 
static String fecha; 

	
	public static String RellenarEspacios(String dato, int tamano)
	 { 
	return String.format("%1$-" + tamano + "s", dato); 
	}

	public static String MostrarMenu(String[] opciones) 
	{             
	String cadena = ""; 
	for (String info : opciones)
	 { 
	cadena = cadena + info + "\n"; 
	         }
	return cadena; 
	}

	public static boolean EsNumeroEntero(String dato) {
	    for (char c : dato.toCharArray()) {
	        if (!Character.isDigit(c)) {
	            return false;
	        }
	    }
	    return true;
	}

	public static boolean EsNumeroDouble(String dato) {
	    boolean valido = false;

	    for (char c : dato.toCharArray()) {
	        if (!Character.isDigit(c)) {
	            if (c == '.' && !valido) {
	                valido = true;
	            } else {
	                return false;
	            }
	        }
	    }

	    return valido;
	}

	
	public static boolean EvaluarNumerico(String dato, int tipo) 
	{ boolean valido=false;
		switch (tipo) {
        case 1: {
            valido = EsNumeroEntero(dato);
            break;
        }
        case 2: {
            valido = EsNumeroDouble(dato);
            break;
        }
		}
		return valido; 
	}

	public static String Dialogo(String texto) throws IOException 
	{ 
	String cadena; 
	System.out.println(texto + " : "); 
	BufferedReader lectura = new BufferedReader(new InputStreamReader(System.in)); 
	cadena = lectura.readLine(); 
	return cadena; 
	}

	public static String Leer(String texto) throws IOException
	 { 
	String cadena = ""; 
	cadena = Dialogo(texto); 
	if (cadena != null) 
	{ 
		cadena = cadena.trim(); 
		if (cadena.isEmpty())
			cadena=null;
	}
	else
		cadena = null; 
	return cadena; 
	} 

	public static String Fecha() { 
		Date fecha = new Date(); 
		SimpleDateFormat formatodia = new SimpleDateFormat("dd-MM-yyyy"); 
		return formatodia.format(fecha); 
		} 
	
		public static String IdticketSiguiente(String idticket) { 
		String idticketnext = ""; 
		int num = Integer.parseInt(idticket) + 1; 
		if (num < 10) { 
		idticketnext = "00" + String.valueOf(num).trim(); 
		} else if ((num > 9) && (num < 100)) { 
		idticketnext = "0" + String.valueOf(num).trim(); 
		} else { 
		idticketnext = String.valueOf(num).trim(); 
		} 
		return idticketnext; 
		}
	

		

		
		
	public static String DesplegarMenu(String Titulo1, String[] menu) throws IOException 
	{ 
	String cadena; 
	       cadena=Titulo1 + "\n" + "\n"; 
	cadena=cadena+MostrarMenu(menu);
	cadena = cadena +"\n Que opcion deseas "; 
	return  cadena = Dialogo(cadena);
	}

	
	public static void CargarProductos()
	{
		 productos = new String[][] {
			    { "001", "Café late grande", "70" },
			    { "002", "Café late chico", "50" },
			    { "003", "Café Capuchino grande", "70" },
			    { "004", "Café Capuchino chico", "50" },
			    { "005", "Baguete Clásico", "90" },
			    { "006", "Bagel con Avellana", "95" },
			    { "007", "Café moka frapuchino", "56" },
			    { "008", "Café expreso", "40" },
			    { "009", "Baguette italiano", "110" },
			    { "010", "Aranciata Natural", "45" }
			};
	}
		
	public static String MostrarProducto(int pos) { 
		String codigo = RellenarEspacios(productos[pos][0], 5); 
		String producto = RellenarEspacios(productos[pos][1], 30); 
		String precio = RellenarEspacios(productos[pos][2], 10); 
		String cadena = codigo.concat(producto+precio); 
		return cadena; 
		} 

	
	
	public static String MostrarLista() {
	    String salida = "";

	    for (int ciclo = 0; ciclo < 10; ciclo++) {
	        String cadena = MostrarProducto(ciclo);
	        salida = salida.concat(cadena + "\n");
	    }
	    return salida;
	}
	
	
	
	
	public static int ExisteProducto(String codigo) { 
		int enc = -1; // si no se encuentra se entregara el -1 
		int pos = 0; 
		for (int ciclo = 0; ciclo < 10; ciclo++) { 
		if (productos[ciclo][0].compareTo(codigo.trim()) == 0) { 
		enc = pos; 
		} 
		pos++; 
		} 
		return enc; 
		} 

	public static void ModificarProducto() throws IOException { 
		String codigo, precio; 
		int posicion; 
		String info = MostrarLista(); 
		codigo = Leer(info + "\nIntroduce el codigo del producto a modificar");
		if (codigo!=null) 
		{	posicion = ExisteProducto(codigo); 
			if (posicion > -1) { 
				precio = Leer( "\nIntroduce el precio de  "+MostrarProducto(posicion)+"  " ); 
				if (precio!=null)
				{if (EvaluarNumerico(precio,2)||EvaluarNumerico(precio,1)) 
					productos[posicion][2] = precio; 
				else
					System.out.println("no es un valor numerico");
				} 
				else 
					System.out.println(" dato nulo");
			} 
			else  
				System.out.println("no existe el codigo"); 
		}
		else 
			System.out.println(" dato nulo"); 
		} 
	
	
	public static void CrearVenta() { 
		ventas = new String[tamventas][5]; 
	}
	
	public static String UltimoTicket() { 
		String idticket = "000"; 
		if (posventas > -1) { 
		idticket = ventas[posventas][0]; 
		} 
		return idticket; 
		} 


public static int ExisteTicketCodigo(String idticket,String codigo) { 
int enc = -1; 
for (int ciclo = 0; ciclo <= posventas; ciclo++) { 
if  (ventas[ciclo][0].compareTo(idticket.trim()) == 0)
	if (ventas[ciclo][1].compareTo(codigo.trim()) == 0)
	{ 
			enc = ciclo; 
	} 
} 
return enc; 
} 

public static boolean InsertarTicket(String[] datos)
{
boolean sucedio=true;	
if (posventas < tamventas) {
if (ExisteTicketCodigo(datos[0],datos[1])>-1)
{ int v1,v2;
	v1=Integer.parseInt(ventas[posventas][4]);
	v2 =Integer.parseInt(datos[4]);
	ventas[posventas][4]=String.valueOf(v1+v2);
}
else {
		posventas++;
		ventas[posventas][0]=datos[0];
		ventas[posventas][1]=datos[1];
		ventas[posventas][2]=datos[2];
		ventas[posventas][3]=datos[3];
		ventas[posventas][4]=datos[4];
	 }	
}
else
	sucedio=false;
return sucedio;
}

public static String TotalProducto(String precio,String cantidad)
{
	double total = Double.parseDouble(precio)*Double.parseDouble(cantidad);
	return String.format("%.2f", total);

}


public static String MostrarProductoTicket(int pos) { 
String codigo = RellenarEspacios(ventas[pos][1], 5); 
String producto = RellenarEspacios(ventas[pos][2], 30); 
String precio = RellenarEspacios(ventas[pos][3], 10);
String cantidad = RellenarEspacios(ventas[pos][4], 5);
String totalproducto = RellenarEspacios( TotalProducto(ventas[pos][3],ventas[pos][4]), 10);
String cadena = codigo.concat(producto+precio+cantidad+totalproducto);; 
return cadena; 
}


public static String MostrarTicket(String idticket) { 
String salida = ""; 
salida = ""; 
for (int ciclo = 0; ciclo <= posventas; ciclo++)  
if (ventas[ciclo][0].compareTo(idticket.trim()) == 0)  
salida = salida.concat(MostrarProductoTicket(ciclo)+"\n");     
return salida; 
} 


public static double SubTotalTicket(String idticket) { 
double subtotal = 0; 
for (int ciclo = 0; ciclo <= posventas; ciclo++)  
if (ventas[ciclo][0].compareTo(idticket.trim()) == 0)	
subtotal = subtotal + Double.parseDouble(TotalProducto(ventas[ciclo][3],ventas[ciclo][4])); 
return subtotal; 
}

public static double IvaTicket(String idticket) { 
double subtotal = SubTotalTicket(idticket);
if (subtotal>0)
subtotal = 0.16 * subtotal;
else
	subtotal=-1;
return subtotal; 
} 
public static double TotalTicket(String idticket) { 
double total = SubTotalTicket(idticket); 
if (total>0)
total = IvaTicket(idticket) + total;  
return total; 
} 

public static String MostrarTicketVenta(String idticket) { 
String salida = "";
String subtotal=String.format("%.2f",SubTotalTicket(idticket));
String iva=String.format("%.2f",IvaTicket(idticket));
String total=String.format("%.2f",TotalTicket(idticket));
salida = "Fecha " + fecha + "Ticket No." + idticket; 
salida = salida + "\n" + MostrarTicket(idticket); 
salida = salida + "\n \n El total sin iva " + subtotal; 
salida = salida + "\n el iva total es " + iva; 
salida = salida + "\n el total de la venta fue " +total ; 
return salida; 
}

public static void CapturaVentaProducto(String idticket) throws IOException { 
String codigo, info; 
info = MostrarLista(); 
codigo = Leer(info + "\nIntroduce el codigo del producto");
if (codigo!=null)
{int pos = ExisteProducto(codigo.trim()); 
if (pos > -1) { 
System.out.println(MostrarProducto(pos)); 
String[] venta = new String[5];
venta[0] = idticket; 
venta[1] = productos[pos][0]; 
venta[2] = productos[pos][1]; 
venta[3] = productos[pos][2]; 
venta[4] = "1";
if (!InsertarTicket(venta) )
System.out.println("el Arreglo esta lleno \n" );
 } else { 
System.out.println("el codigo no existe no se puede agregar\n "); 
}
} else System.out.println("dato nulo\n "); 

}


public static boolean RemoverFilaVentas(String id) { 
	boolean borrado=false;
	String[][] copia = ventas; 
	String idticket, codigo, producto, precio; 
	for (int filas = 0; filas < posventas; filas++) { 
	if (copia[filas][0].compareTo(id) == 0) { 
	if (filas < tamventas-1) { 
	idticket = copia[filas + 1][0]; 
	codigo = copia[filas + 1][1]; 
	producto = copia[filas + 1][2]; 
	precio = copia[filas + 1][3]; 
	copia[filas + 1][0] = id; 
	} else { 
	idticket = ""; 
	codigo = ""; 
	producto = ""; 
	precio = ""; 
	} 
	copia[filas][0] = idticket; 
	copia[filas][1] = codigo; 
	copia[filas][2] = producto; 
	copia[filas][3] = precio;
	borrado=true;
	} 
	} 
	if (borrado)
	posventas--; 
	ventas = copia;
return borrado ;
}

public static void  EliminarFilasVentas(String id)
{
boolean accion=true;
while (accion)
{
	accion=RemoverFilaVentas(id);
}
}

public static void RemoverProductoVentas(int pos ) { 
	String[][] copia = ventas; 
	String idticket, codigo, producto, precio; 
	for (int filas = pos; filas < posventas; filas++) { 
		if (filas < tamventas-1) { 
			idticket = copia[filas + 1][0]; 
			codigo = copia[filas + 1][1]; 
			producto = copia[filas + 1][2]; 
			precio = copia[filas + 1][3]; 
		} else { 
				idticket = ""; 
				codigo = ""; 
				producto = ""; 
				precio = ""; 
				} 
		copia[filas][0] = idticket; 
		copia[filas][1] = codigo; 
		copia[filas][2] = producto; 
		copia[filas][3] = precio;
		} 
	posventas--; 
	ventas = copia;
}


public static void Eliminar(String idticket) throws IOException { 
String codigo, info; 
info = MostrarTicket(idticket); 
codigo = Leer(info + "\nIntroduce el codigo del producto"); 
if (codigo!=null)
{
	int pos = ExisteTicketCodigo(idticket, codigo); 
	if (pos > -1)  
	RemoverProductoVentas(pos);
}
else
	System.out.println("dato nulo");
} 

public static void Listado(String idticket) 
{ 
System.out.println(MostrarTicketVenta(idticket)); 
}

public static void Pagar(String idticket) 
{ 
System.out.println(MostrarTicketVenta(idticket)); 
}


	public static void MenuPuntoVenta(String idticket) throws IOException 
	{ 
	String opcion, membrete; 
	Boolean pago = false; 
		idticket = IdticketSiguiente(idticket); 
	 	String fechadia = fecha; 
	
 	do 
	{ 
	membrete = "Fecha del Dia " + fechadia + " Ticket No " + 
	idticket; 
	membrete = membrete + 
	"\n-----------------------------------------------------\n"; 
	
	
	if (!MostrarTicket(idticket).trim().isEmpty())
	membrete = membrete + "\n" + MostrarTicket(idticket) + "\n"; 
	
	String[] datosmenu = { "1.-Agregar  ", "2.-Eliminar ", "3.-Listado ", "4.-Pagar ", "5.-Salida " }; 
	opcion = DesplegarMenu(membrete + "\n Menu de Punto de Venta",datosmenu); 
	if (opcion == null) 
	System.out.println("dato incorrecto introducido"); 
	else 
	switch (opcion) 
	{ 
	case "1": CapturaVentaProducto(idticket); break; 
	case "2": Eliminar(idticket); break; 
	case "3": Listado(idticket); break; 
	case "4": 
		System.out.println("Pagar Ticket y salir");
		Pagar(idticket);
		pago = true;
		opcion = "5"; 
		break;
	case "5": 
	System.out.println("Salida del Ventas "); 
	if (!pago) 
	{ 
		System.out.println("No pago el ticket ");
	System.out.println("eliminando tickte" +idticket);
	EliminarFilasVentas(idticket);
	}
	
	break; 
	default: 
	System.out.println("No existe esta opcion"); break; 
	} 
	} while (opcion.compareTo("5") != 0); 
	} 
	
	
	
	public  static void MenuProductos() throws IOException 
	{ 
	String[] datosmenuproductos = { "1.-Modificar", "2.-Listado",  "3.-Salida " }; 
	String opcion = "0"; 
	do 
	{ 
	opcion = DesplegarMenu("Opciones de Productos ", datosmenuproductos); 
	if (opcion == null) 
	System.out.println("opcion incorrecta "); 
	else 
	switch (opcion) 
	{ 
	case "1": ModificarProducto(); break; 
	case "2": System.out.println(MostrarLista()); break; 
	case "3": 
	System.out.println("Salida del Sistema "); break; 
	default: System.out.println("No existe esta opcion "); break; 
	} 
	} 
	while (opcion.compareTo("3") != 0); 
	} 
	
	
	public  static void MenuPrincipal() throws IOException 
	{ 
	
	String[] datosmenuprincipal = { "1.-Productos ", "2.-Punto de Venta ",  "5.-Salida " }; 
	String opcion = "0"; 
	do 
	{ 
	opcion = DesplegarMenu("Menu de Punto de Cafeteria Don Juan", datosmenuprincipal); 
	if (opcion == null) 
	System.out.println("opcion incorrecta "); 
	else 
	switch (opcion) 
	{ 
	case "1": MenuProductos(); break; 
	case "2": MenuPuntoVenta(UltimoTicket()); 
		break; 
	case "5": 
	System.out.println("Salida del Sistema "); break; 
	default: System.out.println("No existe esta opcion "); break; 
	} 
	} 
	while (opcion.compareTo("5") != 0); 
	} 
	
	public static void main(String[] args) throws IOException {
	fecha = Fecha(); 
	CrearVenta();
	CargarProductos();
	MenuPrincipal();
	}

}