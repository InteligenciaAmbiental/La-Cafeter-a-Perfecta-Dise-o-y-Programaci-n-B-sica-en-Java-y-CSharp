using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLibros
{
    internal class Principal
    {

        static string[,] productos;

        static string[,] ventas;
        static int posventas = -1;
        static int tamventas = 100;
        static string fecha;

      

    public static string MostrarMenu(string[] opciones)
    {
        string cadena = "";
        foreach (string info in opciones)
        {
            cadena += info + "\n";
        }
        return cadena;
    }

    public static bool EsNumeroEntero(string dato)
    {
        foreach (char c in dato)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }


    public static bool EsNumeroDouble(string dato)
    {
        bool valido = false;

        foreach (char c in dato)
        {
            if (!char.IsDigit(c))
            {
                if (c == '.' && !valido)
                {
                    valido = true;
                }
                else
                {
                    return false;
                }
            }
        }

        return valido;
    }

    public static bool EvaluarNumerico(string dato, int tipo)
    {
        bool valido = false;
        switch (tipo)
        {
            case 1:
                valido = EsNumeroEntero(dato);
                break;
            case 2:
                valido = EsNumeroDouble(dato);
                break;
        }
        return valido;
    }

    public static string Dialogo(string texto)
    {
        Console.Write(texto + " : ");
        return Console.ReadLine();
    }

    public static string Leer(string texto)
    {
        string cadena = "";
        cadena = Dialogo(texto);
        if (!string.IsNullOrEmpty(cadena))
        {
            cadena = cadena.Trim();
            if (string.IsNullOrEmpty(cadena))
                cadena = null;
        }
        return cadena;
    }

        public static string DesplegarMenu(string Titulo1, string[] menu)
        {
            string cadena = Titulo1 + "\n\n" + MostrarMenu(menu) + "\nQue opcion deseas ";
            return Dialogo(cadena);
        }

        public static string RellenarEspacios(string dato, int tamano)
        {
            return dato.PadLeft(tamano);
        }

        public static string Fecha()
    {
        DateTime fecha = DateTime.Now;
        return fecha.ToString("dd-MM-yyyy");
    }

    public static string IdTicketSiguiente(string idTicket)
    {
        string idTicketNext = "";
        int num = int.Parse(idTicket) + 1;

        if (num < 10)
        {
            idTicketNext = "00" + num.ToString().Trim();
        }
        else if (num > 9 && num < 100)
        {
            idTicketNext = "0" + num.ToString().Trim();
        }
        else
        {
            idTicketNext = num.ToString().Trim();
        }
        return idTicketNext;
    }


    

    public static void CargarProductos()
    {
        productos = new string[,] {
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

    public static string MostrarProducto(int pos)
    {
        string codigo = RellenarEspacios(productos[pos, 0], 5);
        string producto = RellenarEspacios(productos[pos, 1], 30);
        string precio = RellenarEspacios(productos[pos, 2], 10);
        string cadena = string.Concat(codigo, producto, precio);
        return cadena;
    }

    public static string MostrarLista()
    {
        string salida = "";
        for (int ciclo = 0; ciclo < 10; ciclo++)
        {
            string cadena = MostrarProducto(ciclo);
            salida = string.Concat(salida, cadena, "\n");
        }
        return salida;
    }

    public static int ExisteProducto(string codigo)
    {
        int enc = -1; // si no se encuentra se entregará el -1
        int pos = 0;
        for (int ciclo = 0; ciclo < 10; ciclo++)
        {
            if (string.Compare(productos[ciclo, 0].Trim(), codigo.Trim()) == 0)
            {
                enc = pos;
            }
            pos++;
        }
        return enc;
    }

    public static void ModificarProducto()
    {
        string codigo, precio;
        int posicion;
        string info = MostrarLista();
        codigo = Leer(info + Environment.NewLine + "Introduce el código del producto a modificar");
        if (!string.IsNullOrEmpty(codigo))
        {
            posicion = ExisteProducto(codigo);
            if (posicion > -1)
            {
                precio = Leer("\nIntroduce el precio de " + MostrarProducto(posicion) + " ");
                if (!string.IsNullOrEmpty(codigo))
                {
                    if (EvaluarNumerico(precio, 1) || EvaluarNumerico(precio, 2))
                        productos[posicion, 2] = precio;
                    else
                        Console.WriteLine("No es un valor numérico");
                }
                else Console.WriteLine("Dato nulo");
            }
            else
                Console.WriteLine("No existe el código");
        }
        else
            Console.WriteLine("Dato nulo");
    }



        public static void CrearVenta()
        {
            ventas = new string[100,5];
        }

        public static string UltimoTicket()
        {
            string idticket = "000";
            if (posventas > -1)
            {
                idticket = ventas[posventas,0];
            }
            return idticket;
        }

        public static int ExisteTicketCodigo(string idticket, string codigo)
        {
            int enc = -1;
            for (int ciclo = 0; ciclo <= posventas; ciclo++)
            {
                if (ventas[ciclo,0].CompareTo(idticket.Trim()) == 0)
                {
                    if (ventas[ciclo,1].CompareTo(codigo.Trim()) == 0)
                    {
                        enc = ciclo;
                    }
                }
            }
            return enc;
        }

        public static bool InsertarTicket(string[] datos)
        {
            bool sucedio = true;
            if (posventas < ventas.GetLength(0))
            {
                if (ExisteTicketCodigo(datos[0], datos[1]) > -1)
                {
                    int v1, v2;
                    v1 = int.Parse(ventas[posventas,4]);
                    v2 = int.Parse(datos[4]);
                    ventas[posventas,4] = (v1 + v2).ToString();
                }
                else
                {
                    posventas++;
                    ventas[posventas,0] = datos[0];
                    ventas[posventas,1] = datos[1];
                    ventas[posventas,2] = datos[2];
                    ventas[posventas,3] = datos[3];
                    ventas[posventas,4] = datos[4];
                }
            }
            else
            {
                sucedio = false;
            }
            return sucedio;
        }

        public static string TotalProducto(string precio, string cantidad)
        {
            double total = double.Parse(precio) * double.Parse(cantidad);
           
            return  total.ToString("000000.00");
        }

        public static string MostrarProductoTicket(int pos)
        {
            string codigo = RellenarEspacios(ventas[pos,1], 5);
            string producto = RellenarEspacios(ventas[pos,2], 30);
            string precio = RellenarEspacios(ventas[pos,3], 10);
            string cantidad = RellenarEspacios(ventas[pos,4], 5);
            string totalproducto = RellenarEspacios(TotalProducto(ventas[pos,3], ventas[pos,4]), 10);
            string cadena = String.Concat(codigo,producto ,precio , cantidad , totalproducto);
            return cadena;
        }

        public static string MostrarTicket(string idticket)
        {
            string salida = "";
            salida = "";
            for (int ciclo = 0; ciclo <= posventas; ciclo++)
            {
                if (ventas[ciclo,0].CompareTo(idticket.Trim()) == 0)
                {
                    salida = string.Concat(salida,MostrarProductoTicket(ciclo) + "\n");
                }
            }
            return salida;
        }

        public static double SubTotalTicket(string idticket)
        {
            double subtotal = 0;
            for (int ciclo = 0; ciclo <= posventas; ciclo++)
            {
                if (ventas[ciclo,0].CompareTo(idticket.Trim()) == 0)
                {
                    String valor = TotalProducto(ventas[ciclo, 3], ventas[ciclo, 4]);
                 Console.WriteLine(valor);
                    subtotal = subtotal+double.Parse(valor);
                }
            }
            return subtotal;
        }

        public static double IvaTicket(string idticket)
        {
            double subtotal = SubTotalTicket(idticket);
            if (subtotal > 0)
            {
                subtotal = 0.16 * subtotal;
            }
            else
            {
                subtotal = -1;
            }
            return subtotal;
        }

        public static double TotalTicket(string idticket)
        {
            double total = SubTotalTicket(idticket);
            if (total > 0)
            {
                total += IvaTicket(idticket);
            }
            return total;
        }

        public static string MostrarTicketVenta(string idticket)
        {
            string salida = "";
            string subtotal = string.Format("{0:0.00}", SubTotalTicket(idticket));
            string iva = string.Format("{0:0.00}", IvaTicket(idticket));
            string total = string.Format("{0:0.00}", TotalTicket(idticket));
            salida = "Fecha " + fecha + " Ticket No." + idticket;
            salida += "\n" + MostrarTicket(idticket);
            salida += "\n \n El total sin iva " + subtotal;
            salida += "\n el iva total es " + iva;
            salida += "\n el total de la venta fue " + total;
            return salida;
        }

        public static void CapturaVentaProducto(string idticket)
        {
            string codigo, info;
            info = MostrarLista();
            codigo = Leer(info + "\nIntroduce el codigo del producto");
            if (codigo != null)
            {
                int pos = ExisteProducto(codigo.Trim());
                if (pos > -1)
                {
                    System.Console.WriteLine(MostrarProducto(pos));
                    string[] venta = new string[5];
                    venta[0] = idticket;
                    venta[1] = productos[pos,0];
                    venta[2] = productos[pos,1];
                    venta[3] = productos[pos,2];
                    venta[4] = "1";
                    if (!InsertarTicket(venta))
                    {
                        System.Console.WriteLine("el Arreglo esta lleno \n");
                    }
                }
                else
                {
                    System.Console.WriteLine("el codigo no existe no se puede agregar\n");
                }
            }
            else
            {
                System.Console.WriteLine("dato nulo\n");
            }
        }

        public static bool RemoverFilaVentas(string id)
        {
            bool borrado = false;
            string[,] copia = ventas;
            string idticket, codigo, producto, precio;
            for (int filas = 0; filas < posventas; filas++)
            {
                if (copia[filas,0].CompareTo(id) == 0)
                {
                    if (filas < tamventas - 1)
                    {
                        idticket = copia[filas + 1,0];
                        codigo = copia[filas + 1,1];
                        producto = copia[filas + 1,2];
                        precio = copia[filas + 1,3];
                        copia[filas + 1,0] = id;
                    }
                    else
                    {
                        idticket = "";
                        codigo = "";
                        producto = "";
                        precio = "";
                    }

                    copia[filas,0] = idticket;
                    copia[filas,1] = codigo;
                    copia[filas,2] = producto;
                    copia[filas,3] = precio;
                    borrado = true;
                }
            }

            if (borrado)
            {
                posventas--;
                ventas = copia;
            }

            return borrado;
        }

        public static void EliminarFilasVentas(string id)
        {
            bool accion = true;
            while (accion)
            {
                accion = RemoverFilaVentas(id);
            }
        }

        public static void RemoverProductoVentas(int pos)
        {
            string[,] copia = ventas;
            string idticket, codigo, producto, precio;
            for (int filas = pos; filas < posventas; filas++)
            {
                if (filas < tamventas - 1)
                {
                    idticket = copia[filas + 1,0];
                    codigo = copia[filas + 1,1];
                    producto = copia[filas + 1,2];
                    precio = copia[filas + 1,3];
                }
                else
                {
                    idticket = "";
                    codigo = "";
                    producto = "";
                    precio = "";
                }

                copia[filas,0] = idticket;
                copia[filas,1] = codigo;
                copia[filas,2] = producto;
                copia[filas,3] = precio;
            }

            posventas--;
            ventas = copia;
        }


        public static void Eliminar(string idticket)
        {
            string codigo, info;
            info = MostrarTicket(idticket);
            codigo = Leer(info + "\nIntroduce el codigo del producto");
            if (codigo != null)
            {
                int pos = ExisteTicketCodigo(idticket, codigo);
                if (pos > -1)
                {
                    RemoverProductoVentas(pos);
                }
            }
            else
            {
                System.Console.WriteLine("dato nulo");
            }
        }

        public static void Listado(string idticket)
        {
            System.Console.WriteLine(MostrarTicketVenta(idticket));
        }

        public static void Pagar(string idticket)
        {
            System.Console.WriteLine(MostrarTicketVenta(idticket));
        }

        public static void MenuPuntoVenta(string idticket)
        {   string opcion, membrete;
            bool pago = false;
            idticket = IdTicketSiguiente(idticket);
            string fechadia = fecha;
            do
            {
                membrete = "Fecha del Dia " + fechadia + " Ticket No " + idticket;
                membrete += "\n-----------------------------------------------------\n";
                if (!MostrarTicket(idticket).Trim().Equals(""))
                {
                    membrete += "\n" + MostrarTicket(idticket) + "\n";
                }
                string[] datosmenu = { "1.-Agregar  ", "2.-Eliminar ", "3.-Listado ", "4.-Pagar ", "5.-Salida " };
                opcion = DesplegarMenu(membrete + "\n Menu de Punto de Venta", datosmenu);
                if (opcion == null)
                {
                    System.Console.WriteLine("dato incorrecto introducido");
                }
                else
                {
                    switch (opcion)
                    {
                        case "1":
                            CapturaVentaProducto(idticket);
                            break;
                        case "2":
                            Eliminar(idticket);
                            break;
                        case "3":
                            Listado(idticket);
                            break;
                        case "4":
                            System.Console.WriteLine("Pagar Ticket y salir");
                            Pagar(idticket);
                            pago = true;
                            opcion = "5";
                            break;
                        case "5":
                            System.Console.WriteLine("Salida del Ventas ");
                            if (!pago)
                            {
                                System.Console.WriteLine("No pago el ticket ");
                                System.Console.WriteLine("eliminando tickte" + idticket);
                                EliminarFilasVentas(idticket);
                            }
                            break;
                        default:
                            System.Console.WriteLine("No existe esta opcion");
                            break;
                    }
                }
            } while (opcion.CompareTo("5") != 0);
        }

        public static void MenuProductos()
    {
        string[] datosmenuproductos = { "1.-Modificar", "2.-Listado", "3.-Salida " };
        string opcion = "0";
        do
        {
            opcion = DesplegarMenu("Opciones de Productos ", datosmenuproductos);
            if (opcion == null)
            {
                Console.WriteLine("Opcion incorrecta ");
            }
            else
            {
                switch (opcion)
                {
                    case "1":
                        ModificarProducto();
                        break;
                    case "2":
                        Console.WriteLine(MostrarLista());
                        break;
                    case "3":
                        Console.WriteLine("Salida del Sistema ");
                        break;
                    default:
                        Console.WriteLine("No existe esta opcion ");
                        break;
                }
            }
        } while (opcion.CompareTo("3") != 0);
    }

        public static void MenuPrincipal()
        {
            string[] datosmenuprincipal = { "1.-Productos", "2.-Punto de Venta", "5.-Salida" };
            string opcion = "0";

            do
            {
                opcion = DesplegarMenu("Menu de Punto de Cafeteria Don Juan", datosmenuprincipal);
                if (opcion == null)
                {
                    System.Console.WriteLine("opcion incorrecta");
                }
                else
                {
                    switch (opcion)
                    {
                        case "1":
                            MenuProductos();
                            break;
                        case "2":
                            MenuPuntoVenta(UltimoTicket());
                            break;
                        case "5":
                            System.Console.WriteLine("Salida del Sistema");
                            break;
                        default:
                            System.Console.WriteLine("No existe esta opcion");
                            break;
                    }
                }
            } while (opcion.CompareTo("5") != 0);
        }


        static void Main(string[] args)
    {
            fecha = Fecha();
            CrearVenta();

            CargarProductos();
        MenuPrincipal();
    }
}	

}
