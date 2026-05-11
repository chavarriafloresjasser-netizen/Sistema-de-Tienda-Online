public class Program
{
    public static void Main(string[] args)
    {
        bool excepcion = false;
        int opcion = 0;
        //Frontend
        Console.WriteLine("Sea bienvenido al sistema de tienda online");
        Console.WriteLine("Iniciar sesión en");
        Console.WriteLine("1. Comprador");
        Console.WriteLine("2. Vendedor");
        do
        {
            try
            {
                 opcion = int.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                excepcion = false;
            }

            if (opcion == 1 || opcion == 2)
            {
                excepcion = true;
            }
            else
            {
                Console.WriteLine("No existe opción " + opcion);
            }
        } while(excepcion == false);

        IniciarSesion(opcion);
    }

    public static void Comprador()
    {
        bool excepcion = false;
        string? correo = string.Empty;
        string contraseña = string.Empty;
        Console.WriteLine("Correo");
        do
        {
            try
            {
                correo = Console.ReadLine();

            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                excepcion = false;
            }
            
            if (string .IsNullOrEmpty(correo))
            {
                Console.WriteLine("Correo no puede estar vacio");
                excepcion = false;
            }
            if(correo.Contains("@") && correo.Contains(".com"))
            {
                excepcion = true;
            }
            else
            {
                Console.WriteLine("Correo no valido");
            }
        } while (excepcion == false);

        Console.WriteLine("Contraseña");
        do
        {
            try
            {
                contraseña = Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                excepcion = false;
            }
        } while(excepcion == false);
    }

    public static void IniciarSesion(int opcion)
    {
        if (opcion == 1)
        {
            Comprador();
        }
        else if (opcion == 2)
        {
            Vendedor();
        }
    }
    public static void Vendedor()
    {
        for(int i = 0; i < 5; i++)
        {
            Console.WriteLine("Nigga");
        }
    }
}