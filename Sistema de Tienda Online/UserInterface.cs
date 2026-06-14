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
        string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID, string rutaCarrito, ManejoCarrito carritos, 
        CarritoDeCompras carritoUnico)
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
                IniciarSesionEn(usuarios, cuentaEmpresarial, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID, rutaCarrito,carritos, carritoUnico);
                break;
            case "NO":
                CrearNuevaCuentaDeUsuario(usuarios, cuentaEmpresarial, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID, rutaCarrito, carritos, carritoUnico);
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
        ManejoDeProductos manejoDeProductos,Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID,
        string rutaCarrito, ManejoCarrito carritos, CarritoDeCompras carritoUnico)
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
        Inicio(usuarios, cuentaEmpresarial, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID,
            rutaCarrito, carritos, carritoUnico);
    }

    /// <summary>
    /// CLase hecha para que el usuario que ya posea una cuenta
    /// que pueda iniciar sesión, en el cual se le solicitará 
    /// su correo electrónico y contraseña para poder acceder a 
    /// dicha cuenta
    /// </summary>
    public static void IniciarSesionEn(UsuariosAlmacenados usuarios, CuentaEmpresarial cuentaEmpresarial, SistemaDeArchivado sistemaDeArchivado, ManejoDeProductos manejoDeProductos,
        Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID, string rutaCarrito,
        ManejoCarrito carritos, CarritoDeCompras carritoUnico)
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
        InicioUsuario(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID, rutaCarrito, carritos, carritoUnico);
    }

    /// <summary>
    /// Aquí se va a mostrar el menú principal del usuario, en el cual podrá elegir entre buscar productos, ver su carrito de compras, ver su perfil o salir de la tienda, 
    /// cada opción lo llevará a una funcionalidad diferente dentro del programa.
    /// </summary>
    /// <param name="cuentaEmpresarial"></param>
    /// <param name="usuarios"></param>
    /// <param name="correo"></param>
    /// <param name="sistemaDeArchivado"></param>
    /// <param name="manejoDeProductos"></param>
    /// <param name="repository"></param>
    /// <param name="rutaUsuariosSinID"></param>
    /// <param name="rutaProductosSinID"></param>
    /// <param name="rutaUsuariosConID"></param>
    /// <param name="rutaProductosConID"></param>
    public static void InicioUsuario(CuentaEmpresarial cuentaEmpresarial, UsuariosAlmacenados usuarios, string correo, SistemaDeArchivado sistemaDeArchivado, ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID,
        string rutaCarrito, ManejoCarrito carritos, CarritoDeCompras nuevoCarrito)
    {
        Console.Clear();
        DiseñosGenerales diseños = new DiseñosGenerales();
        carritos.AsignarCarrito(usuarios, nuevoCarrito, correo);
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
                BuscarProductos(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID,
                    rutaUsuariosConID, rutaProductosConID, rutaCarrito, carritos, nuevoCarrito);
                break;
            case 2:
                CarritoDeCompras(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID,
                    rutaProductosConID, rutaCarrito, carritos, nuevoCarrito);
                break;
            case 3:
                VerPerfil(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID,
                    rutaCarrito, carritos, nuevoCarrito);
                break;
            case 4:
                Console.WriteLine("Saliendo de la tienda...");
                Thread.Sleep(1000);
                sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                sistemaDeArchivado.GuardarCarritos(carritos, rutaCarrito, repository);
                Console.ReadKey();
                break;
        }
    }

    /// <summary>
    /// Aquí se va a administrar todo lo relacionado con el carrito de compras, como agregar productos, eliminar productos, ver el total a pagar, etc.
    /// </summary>
    /// <param name="cuentaEmpresarial"></param>
    /// <param name="usuarios"></param>
    /// <param name="correo"></param>
    /// <param name="sistemaDeArchivado"></param>
    /// <param name="manejoDeProductos"></param>
    /// <param name="repository"></param>
    /// <param name="rutaUsuariosSinID"></param>
    /// <param name="rutaProductosSinID"></param>
    /// <param name="rutaUsuariosConID"></param>
    /// <param name="rutaProductosConID"></param>
    public static void CarritoDeCompras(CuentaEmpresarial cuentaEmpresarial, UsuariosAlmacenados usuarios, string correo, SistemaDeArchivado sistemaDeArchivado, 
        ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID,
        string rutaCarrito, ManejoCarrito carrito, CarritoDeCompras nuevoCarrito)
    {
        DiseñosGenerales diseños = new DiseñosGenerales();
        int opcion = 0;
        bool none = false;
        do
        {
            Console.Clear();
            diseños.RecuadroPrincipal("Carrito de compras");
            diseños.RemarcarTexto("Que desa hacer?");
            Console.WriteLine("1. Ver Carrito");
            Console.WriteLine("2. Eliminar un producto del carrito");
            Console.WriteLine("3. Comprar Todo el carrito");
            Console.WriteLine("4. Salir");
            do
            {
                try
                {
                    opcion = int.Parse(Console.ReadLine());
                    none = true;
                }catch (FormatException ex)
                {
                    Console.WriteLine("ERROR: dato ingresado invalido, digitelo nuevamente" + ex.Message);
                    none = false;
                }
            } while (none == false);

            switch (opcion)
            {
                case 1:
                    carrito.VerCarrito(correo, usuarios, manejoDeProductos, nuevoCarrito);
                    break;
                case 2:
                    int idProducto = 0;
                    bool fx = false;
                    Console.Clear();
                    diseños.RecuadroPrincipal("Eliminar producto del carrito");
                    diseños.RemarcarTexto("Ingrese el ID del producto a eliminar del carrrito");
                    do
                    {
                        try
                        {
                            idProducto = int.Parse(Console.ReadLine());
                            fx = true;
                        }catch(FormatException ex)
                        {
                            Console.WriteLine("ERROR: el dato ingresado invalido, intentelo nuevamente " + ex.Message);
                            fx = false;
                        }
                    } while (fx == false);
                    carrito.EliminarProductoDelCarrito(idProducto, correo, usuarios, manejoDeProductos);
                    sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                    sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                    sistemaDeArchivado.GuardarCarritos(carrito, rutaCarrito, repository);
                    break;
                case 3:
                    Console.Clear();
                    try
                    {
                        carrito.ComprarTodoElCarrito(manejoDeProductos);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine("ERROR: " + ex.Message);
                    }
                    sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                    sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                    sistemaDeArchivado.GuardarCarritos(carrito, rutaCarrito, repository);
                    diseños.RemarcarTexto("Tarea ejecutada");
                    Thread.Sleep(3000);
                    break;
                case 4:
                    sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                    sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                    sistemaDeArchivado.GuardarCarritos(carrito, rutaCarrito, repository);
                    InicioUsuario(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID,
            rutaUsuariosConID, rutaProductosConID, rutaCarrito, carrito, nuevoCarrito);
                    break;
            }
        } while (opcion != 4);
    }

    /// <summary>
    /// Aquí se va a mostrar toda la información del perfil del usuario, como su nombre completo, correo electrónico, número de teléfono, etc.
    /// </summary>
    /// <param name="cuentaEmpresarial"></param>
    /// <param name="usuarios"></param>
    /// <param name="correo"></param>
    /// <param name="sistemaDeArchivado"></param>
    /// <param name="manejoDeProductos"></param>
    /// <param name="repository"></param>
    /// <param name="rutaUsuariosSinID"></param>
    /// <param name="rutaProductosSinID"></param>
    /// <param name="rutaUsuariosConID"></param>
    /// <param name="rutaProductosConID"></param>
    public static void VerPerfil(CuentaEmpresarial cuentaEmpresarial, UsuariosAlmacenados usuarios, string correo, SistemaDeArchivado sistemaDeArchivado, 
        ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID,
        string rutaCarrito, ManejoCarrito carritos, CarritoDeCompras nuevoCarrito)
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
        InicioUsuario(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID, rutaCarrito, carritos, nuevoCarrito);
    }

    /// <summary>
    /// Aquí se va a implementar la funcionalidad de buscar productos, en el cual el usuario podrá ingresar el nombre del producto que desea buscar y se le mostrarán los resultados que coincidan con su búsqueda, además de que podrá agregar 
    /// el producto al carrito de compras directamente desde la búsqueda.
    /// </summary>
    /// <param name="cuentaEmpresarial"></param>
    /// <param name="usuarios"></param>
    /// <param name="correo"></param>
    /// <param name="sistemaDeArchivado"></param>
    /// <param name="manejoDeProductos"></param>
    /// <param name="repository"></param>
    /// <param name="rutaUsuariosSinID"></param>
    /// <param name="rutaProductosSinID"></param>
    /// <param name="rutaUsuariosConID"></param>
    /// <param name="rutaProductosConID"></param>
    public static void BuscarProductos(CuentaEmpresarial cuentaEmpresarial, UsuariosAlmacenados usuarios, string correo, SistemaDeArchivado sistemaDeArchivado, 
        ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID,
        string rutaCarrito, ManejoCarrito carritos, CarritoDeCompras nuevoCarrito)
    {
        Console.Clear();
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Buscar Productos");
        Console.WriteLine("Ingrese el nombre del producto que desea buscar:");
        string? nombreProducto = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nombreProducto))
        {
            Console.WriteLine("No puede buscar productos sin ingresar un nombre.");
            return;
        }

        var productosEncontrados = manejoDeProductos.ProductosConID
            .Where(kv => kv.Value.NombreProducto.Contains(nombreProducto, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (productosEncontrados.Count == 0)
        {
            Console.WriteLine("No se encontraron productos con ese nombre.");
        }
        else
        {
            Console.WriteLine("Productos encontrados:");
            int i = 1;
            foreach (var kv in productosEncontrados)
            {
                var producto = kv.Value;
                Console.WriteLine($"{i} - ID: {kv.Key}. Nombre: {producto.NombreProducto} {producto.Extra} - Precio: {producto.Precio}");
                i++;
            }
        }

            Console.WriteLine("Ingrese el ID del producto que desea agregar al carrito o '0' para regresar al menu principal:");
            int id = 0;
            bool validOption = false;
            do
            {
                try
                {
                     id = Convert.ToInt32(Console.ReadLine());
                    // Validar que el id no sea negativo y que exista en los productos
                    if (id < 0 || (id > 0 && !manejoDeProductos.ProductosConID.ContainsKey(id)))
                    {
                        Console.WriteLine("Opción no válida, por favor ingrese un ID existente o 0 para regresar.");
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
            if (id > 0)
            {

                // Agregar el producto seleccionado al carrito del usuario
                try
                {
                    carritos.AgregarAlCarrito(id, correo, manejoDeProductos, usuarios);
                    // Guardar cambios de carritos en archivo
                    
                        repository.GuardarCarritos(carritos, rutaCarrito);
                    Console.WriteLine("Producto agregado al carrito correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar el producto al carrito: " + ex.Message);
                }

               InicioUsuario(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID,
                   rutaCarrito, carritos, nuevoCarrito);
            }
            else if(id == 0)
        {
            InicioUsuario(cuentaEmpresarial, usuarios, correo, sistemaDeArchivado, manejoDeProductos, repository, rutaUsuariosSinID, rutaProductosSinID, rutaUsuariosConID, rutaProductosConID,
                   rutaCarrito, carritos, nuevoCarrito);
        }
    }
}