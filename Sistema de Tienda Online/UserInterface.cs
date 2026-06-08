public class UserInterface
{
    public UserInterface()
    {
    }
    /// <summary>
    /// Inicio de todo el programa, donde le pide al usuario que inicie sesion
    /// si ya posee una, de lo contrario que cree una nueva
    /// </summary>
    public static void Inicio(UsuariosAlmacenados usuarios, CuentaEmpresarial cuentaEmpresarial, SistemaDeArchivado sistemaDeArchivado,
        ManejoDeProductos manejoDeProductos, Repository repository,
        string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        Console.Clear();
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
                IniciarSesionEn(usuarios, cuentaEmpresarial, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID);
                break;
            case "NO":
                CrearNuevaCuentaDeUsuario(usuarios, cuentaEmpresarial, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID);
                break;
        }
    }

    /// <summary>
    /// Realiza el proceso de creación de una nueva cuenta de usuario,
    /// en el cual todos los campos son obligatorios, si dado caso el
    /// usuario ingresa un dato no válido, se le indicará el error y 
    /// se le solicitará que vuelva a ingresar la información hasta que sea correcta.
    /// </summary>
    public static void CrearNuevaCuentaDeUsuario(UsuariosAlmacenados usuarios, CuentaEmpresarial cuentaEmpresarial, SistemaDeArchivado sistemaDeArchivado, 
        ManejoDeProductos manejoDeProductos,Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        Console.Clear();
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Registrar nueva cuenta");
        crearCuentaUsuario? nuevoUsuario = null;
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
            diseñosGenerales.RemarcarTexto("\nSegundo Nombre:");
            SegundoNombre = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("\nPrimer Apellido:");
            PrimerApellido = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("\nSegundo Apellido:");
            SegundoApellido = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("\nCorreo Electrónico:");
            CorreoElectronico = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("\nContraseña:");
            Contraseña = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("\nTeléfono:");
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
                nuevoUsuario = new crearCuentaUsuario(PrimerNombre, CorreoElectronico, Contraseña, Telefono, SegundoNombre, PrimerApellido, SegundoApellido);
                try
                {
                    usuarios.AgregarUsuario(nuevoUsuario);
                    opcion = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                opcion = false;
            }
        } while (opcion == false);
        Console.WriteLine("La cuenta ha sido creada exitosamente.");
        Console.WriteLine("Redirigiendo a la página de inicio...");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(1000);
        }
        sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
        sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
        Inicio(usuarios, cuentaEmpresarial, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID);
    }

    /// <summary>
    /// CLase hecha para que el usuario que ya posea una cuenta
    /// que pueda iniciar sesión, en el cual se le solicitará 
    /// su correo electrónico y contraseña para poder acceder a 
    /// dicha cuenta
    /// </summary>
    public static void IniciarSesionEn(UsuariosAlmacenados usuarios, CuentaEmpresarial cuentaEmpresarial, SistemaDeArchivado sistemaDeArchivado, ManejoDeProductos manejoDeProductos,
        Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        Console.Clear();
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Iniciar sesión");
        string? correo = "";
        string? contraseña = "";
        do
        {
            diseñosGenerales.RemarcarTexto("\nCorreo Electrónico:");
            correo = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("\nContraseña:");
            contraseña = Console.ReadLine();
            try
            {
                usuarios.VerificarInicioDeSecion(correo, contraseña);
                Console.WriteLine("Inicio de sesión exitoso.");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Redirigiendo a la página de inicio...");
                    Thread.Sleep(1000);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (correo == "" || contraseña == "");
        InicioUsuario(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID);
    }

    public static void InicioUsuario(CuentaEmpresarial cuentaEmpresarial, UsuariosAlmacenados usuarios, string correo, SistemaDeArchivado sistemaDeArchivado, ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        Console.Clear();
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal(cuentaEmpresarial.Nombre);
        diseños.RemarcarTexto($"Sea bienvenido a su cuenta.  que desea hacer?");
        Console.WriteLine("1 - Buscar productos");
        Console.WriteLine("2 - Ver Carrito de compras");
        Console.WriteLine("3 - Ver Perfil");
        Console.WriteLine("4 - Salir");
        int opcion = 0;
        bool none = false;
        do
        {
            do
            {
                try
                {
                    opcion = Convert.ToInt32(Console.ReadLine());
                    none = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Por favor ingrese un número válido para la opción.");
                    none = false;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    none = false;
                }
            } while (none == false);

            if(opcion < 1 || opcion > 4)
            {
                Console.WriteLine("Opción no válida, por favor ingrese un número entre 1 y 4.");
                none = false;
            }
        } while (none == false);

        switch (opcion)
        {
            case 1:
                Console.WriteLine("Funcionalidad de comprar productos aún no implementada.");
                break;
            case 2:
                Console.WriteLine("Funcionalidad de ver carrito de compras aún no implementada.");
                break;
            case 3:
                VerPerfil(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID);
                break;
            case 4:
                Console.WriteLine("Saliendo de la tienda...");
                Thread.Sleep(1000);
                sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                Console.ReadKey();
                break;
        }
    }

    public static void CarritoDeCompras(CuentaEmpresarial cuentaEmpresarial, UsuariosAlmacenados usuarios, string correo, SistemaDeArchivado sistemaDeArchivado, 
        ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        Console.WriteLine("Funcionalidad de carrito de compras aún no implementada.");
    }
    public static void VerPerfil(CuentaEmpresarial cuentaEmpresarial, UsuariosAlmacenados usuarios, string correo, SistemaDeArchivado sistemaDeArchivado, 
        ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        Console.Clear();
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        var usuario = usuarios.UsuariosConID.FirstOrDefault(u => u.Value.Correo == correo);
        diseñosGenerales.RecuadroPrincipal("Perfil de Usuario");
        diseñosGenerales.RemarcarTexto("ID: " + usuario.Key);
        diseñosGenerales.RemarcarTexto($"Nombre completo: {usuario.Value.Nombre} {usuario.Value.SegundoNombre} {usuario.Value.PrimerApellido} {usuario.Value.SegundoApellido}");
        diseñosGenerales.RemarcarTexto("Correo: " + usuario.Value.Correo);
        diseñosGenerales.RemarcarTexto("Telefono: " + usuario.Value.Telefono);

        Console.WriteLine("1 - Salir");
        do
        { 
        }while(Console.ReadLine() != "1");
        InicioUsuario(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID);
    }

    public static void BuscarProductos(CuentaEmpresarial cuentaEmpresarial, UsuariosAlmacenados usuarios, string correo, SistemaDeArchivado sistemaDeArchivado, 
        ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        Console.Clear();
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Buscar Productos");
        Console.WriteLine("Ingrese el nombre del producto que desea buscar:");
        string? nombreProducto = Console.ReadLine();
        bool none = false;
        do
        {
            if (nombreProducto != null)
                Console.WriteLine("No puede buscar productos sin ingresar un nombre.");
            else
                none = true;
        } while(none == false);
        var productosEncontrados = manejoDeProductos.ProductosConID.Values.Where(p => p.NombreProducto.Contains(nombreProducto, StringComparison.OrdinalIgnoreCase)).ToList();
        if (productosEncontrados.Count == 0)
        {
            Console.WriteLine("No se encontraron productos con ese nombre.");
        }
        else
        {
            Console.WriteLine("Productos encontrados:");
            foreach (var producto in productosEncontrados)
            {
                int i = 1;
                Console.WriteLine($" {i}. Nombre: {producto.NombreProducto} {producto.Extra} - Precio: {producto.Precio}");
                i++;
            }
        }

            Console.WriteLine("Ingrese el número del producto que desea agregar al carrito o '0' para regresar al menu principal:");
            int opcion = 0;
            bool validOption = false;
            do
            {
                try
                {
                    opcion = Convert.ToInt32(Console.ReadLine());
                    if (opcion < 0 || opcion > productosEncontrados.Count)
                    {
                        Console.WriteLine("Opción no válida, por favor ingrese un número válido.");
                        validOption = false;
                    }
                    else
                    {
                        validOption = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Por favor ingrese un número válido.");
                    validOption = false;
                }
            } while (validOption == false);
            if (opcion > 0)
            {
               InicioUsuario(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID);
            }
        
    }
}