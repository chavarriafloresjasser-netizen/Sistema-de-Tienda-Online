public class Program
{
    public static void Main(string[] args)
    {
        /*Objetos creados necesarios para el funcionamiento del programa*/
        UsuariosAlmacenados usuariosAlmacenados = new UsuariosAlmacenados();
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        UserInterface userInterface = new UserInterface(usuariosAlmacenados);
        CuentaEmpresarial cuentaEmpresarial = new CuentaEmpresarial();
        EmpresarialInterface empresarialInterface = new EmpresarialInterface();

        //Inicio del programa
        diseñosGenerales.RecuadroPrincipal($"Bienvenido a la Tienda Online de {cuentaEmpresarial.Nombre}");
        diseñosGenerales.RemarcarTexto("¿Como desea continuar?");
        Console.WriteLine("1. Como usuario");
        Console.WriteLine("2. Como administrador");
        int opcion = 0;
        bool Validate = false;
        do
        {
            do
            {
                try
                {
                    opcion = Convert.ToInt32(Console.ReadLine());
                    Validate = true;
                }
                catch
                {
                    Console.WriteLine("Opcion no valida, intente de nuevo.");
                }
            } while (Validate == false);
            switch (opcion)
            {
                case 1:
                    UserInterface.Inicio(usuariosAlmacenados);
                    break;
                case 2:
                    empresarialInterface.IniciarSesion(cuentaEmpresarial);
                    break;
                default:
                    Console.WriteLine("Opcion no valida, intente de nuevo.");
                    break;
            }
        } while (opcion != 1 && opcion != 2);
    }
}