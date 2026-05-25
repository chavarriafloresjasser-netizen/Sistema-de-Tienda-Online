
public class Program
{
    public static void Main(string[] args)
    {
        Inicio();
    }

    public static void Inicio()
    {
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Glados Shoping");
        bool excepcion = false;
        //Frontend
        Console.WriteLine("Sea bienvenido al sistema de tienda online");
        diseñosGenerales.RemarcarTexto("¿Tiene una cuenta ya registrada?");
        diseñosGenerales.Si_o_No();
        string? opcion;
        do
        {
            do
            {
                opcion = Console.ReadLine();
            
                if (opcion == null)
                {
                    Console.WriteLine("No puede quedar nulo el campo");
                    excepcion = false;
                }
                excepcion = true;
            } while (excepcion == false);

            opcion = opcion.Trim().ToUpper();

            if (opcion != "SI" && opcion != "NO")
            {
                Console.WriteLine("Opción no válida, por favor ingrese 'Si' o 'No'");
                excepcion = false;
            }
        } while (excepcion == false);

        switch (opcion)
        {
            case "SI":
                Console.WriteLine("Iniciando sesión...");
                break;
            case "NO":
                CrearNuevaCuentaDeUsuario();
                break;
        }
    }

    public static void CrearNuevaCuentaDeUsuario()
    {
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Registrar nueva cuenta");
        CrearCuentaUsuario? nuevoUsuario = null;
        bool opcion = false;
        string? PrimerNombre = "";
        string? SegundoNombre = "";
        string? PrimerApellido = "";
        string? SegundoApellido = "";
        string? CorreoElectronico = "";
        string? Contraseña = "";
        int Telefono = 0;
        do
        {
            diseñosGenerales.RemarcarTexto("Primer Nombre:");
            PrimerNombre = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Segundo Nombre:");
            SegundoNombre = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Primer Apellido:");
            PrimerApellido = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Segundo Apellido:");
            SegundoApellido = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Correo Electrónico:");
            CorreoElectronico = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Contraseña:");
            Contraseña = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Teléfono:");
            do
            {
                try
                {
                    Telefono = Convert.ToInt32(Console.ReadLine());
                    opcion = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Por favor ingrese un número válido para el teléfono.");
                    opcion = false;
                }
            } while (opcion == false);

            try
            {
                nuevoUsuario = new CrearCuentaUsuario(PrimerNombre, CorreoElectronico, Contraseña, Telefono, SegundoNombre, PrimerApellido, SegundoApellido);
                opcion = true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                opcion = false;
            }

        } while (opcion == false);
    }
}