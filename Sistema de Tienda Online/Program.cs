public class Program
{
    public static void Main(string[] args)
    {
        /*Objetos creados necesarios para el funcionamiento del programa*/
        string rutaUsuariosSinID = "UsuariosSinID.xml";
        string rutaProductosSinID = "ProductosSinID.xml";
        string rutaUsuariosConID = "UsuariosConID.xml";
        string rutaProductosConID = "ProductosConID.xml";
        string rutaCarrito = "Carritos.xml";
        ManejoCarrito carritos = new ManejoCarrito();
        UsuariosAlmacenados usuariosAlmacenados = new UsuariosAlmacenados();
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        UserInterface userInterface = new UserInterface();
        CuentaEmpresarial cuentaEmpresarial = new CuentaEmpresarial();
        ManejoDeProductos manejoDeProductos = new ManejoDeProductos();
        EmpresarialInterface empresarialInterface = new EmpresarialInterface();
        Repository repository = new Repository();
        SistemaDeArchivado sistemaDeArchivado = new SistemaDeArchivado();

        //Revisar si los archivos existen, si no existen se crean posteriormente, si existen se cargan los datos
        if (File.Exists(rutaUsuariosSinID) || File.Exists(rutaProductosSinID))
        {
            try
            {
                repository.CargarDatosSinID(usuariosAlmacenados, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        if (File.Exists(rutaUsuariosConID) || File.Exists(rutaProductosConID))
        {
            try
            {
                repository.CargarDatosConID(usuariosAlmacenados, manejoDeProductos, rutaUsuariosConID, rutaProductosConID);
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        if(File.Exists(rutaCarrito))
        {
            try
            {
                repository.CargarCarritos(carritos, rutaCarrito);
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

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
                    UserInterface.Inicio(usuariosAlmacenados, cuentaEmpresarial, sistemaDeArchivado, manejoDeProductos, 
                        repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID, rutaCarrito, carritos);
                    break;
                case 2:
                    empresarialInterface.IniciarSesion(cuentaEmpresarial, usuariosAlmacenados, sistemaDeArchivado, manejoDeProductos, repository
                        , rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID);
                    break;
                default:
                    Console.WriteLine("Opcion no valida, intente de nuevo.");
                    break;
            }
        } while (opcion != 1 && opcion != 2);
    }
}