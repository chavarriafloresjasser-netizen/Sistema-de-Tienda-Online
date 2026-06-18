public sealed class EmpresarialInterface
{
    public EmpresarialInterface()
    {
    }

    /// <summary>
    /// Permite al usuario iniciar sesión en su cuenta empresarial. Solicita el correo electrónico y la contraseña, y 
    /// verifica si coinciden con los datos de la cuenta proporcionada. Si las credenciales son correctas, 
    /// se muestra un mensaje de bienvenida y se accede a la administración de la cuenta. 
    /// Si las credenciales son incorrectas, se bloquea el intento durante 40 segundos antes de permitir un nuevo intento.
    /// </summary>
    /// <param name="cuenta"></param>
    public void IniciarSesion(CuentaEmpresarial cuenta, UsuariosAlmacenados usuarios, SistemaDeArchivado sistemaDeArchivado,
        ManejoDeProductos productos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        Console.Clear();
        bool opcion = false;
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal("Iniciar Sesión");
        do
        {
            diseños.RemarcarTexto("Ingrese el correo electrónico:");
            string? correo = Console.ReadLine();
            diseños.RemarcarTexto("Ingrese la contraseña:");
            string? contraseña = Console.ReadLine();
            diseños.RemarcarTexto("Ingrese el NIT:");
            string? nit = Console.ReadLine();

            if (correo == cuenta.Correo! && contraseña == cuenta.Contraseña && nit == cuenta.Nit)
            {
                Console.Clear();
                diseños.RemarcarTexto($"¡Bienvenido a la administración de {cuenta.Nombre}!");
                opcion = true;
            }
            else
            {
                diseños.RemarcarTexto("Incorrect values");
                for (int i = 40; i > 0; i--)
                {
                    Console.Clear();
                    Console.Write($"\rIntento bloqueado. Reintente en {i} segundos...");
                    Thread.Sleep(1000);
                }
            }
        } while (opcion == false);
        Inicio(cuenta, usuarios, sistemaDeArchivado, productos, repository, rutaProductosSinID, rutaProductosSinID, rutaUsuariosConID, 
            rutaProductosConID);
    }
    /// <summary>
    /// Permite al usuario acceder a la administración de su cuenta empresarial después de iniciar sesión correctamente.
    /// </summary>
    /// <param name="cuenta"></param>
    public void Inicio(CuentaEmpresarial cuenta, UsuariosAlmacenados usuarios, SistemaDeArchivado sistemaDeArchivado,
        ManejoDeProductos manejoDeProductos, Repository repository, string rutaUsuariosSinID, string rutaProductosSinID, string rutaUsuariosConID, string rutaProductosConID)
    {
        DiseñosGenerales diseños = new DiseñosGenerales();
        bool terminar = false;
        bool none = false;
        int opcion = 0;
        string oute = "";
        do
        {
            Console.Clear();
            diseños.RecuadroPrincipal($"Administración de {cuenta.Nombre}");
            diseños.RemarcarTexto("¿Qué quiere administrar?");
            Console.WriteLine("1. Ver todos los productos en la tienda");
            Console.WriteLine("2. Ver todos los usuarios registrados");
            Console.WriteLine("3. Añadir nuevo producto");
            Console.WriteLine("4. Eliminar producto");
            Console.WriteLine("5. Modificar producto");
            Console.WriteLine("6. Buscar producto por ID");
            Console.WriteLine("7. Salir");
            do
            {
                try
                {
                    opcion = int.Parse(Console.ReadLine());
                    none = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("ERROR: dato ingresado invalido, intentelo nuevamente" + ex);
                    none = false;
                } 
            } while (none == false);

            switch (opcion)
            {
                case 1:
                    VerTodosLosProductos(manejoDeProductos);
                    oute = Console.ReadLine();
                    break;
                case 2:
                    VerTodosLosUsuarios(usuarios);
                    oute = Console.ReadLine();
                    break;
                case 3:
                    AniadirNuevoProducto(manejoDeProductos);
                    sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                    sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                    oute = Console.ReadLine();
                    break;
                case 4:
                    EliminarProducto(manejoDeProductos);
                    sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                    sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                    oute = Console.ReadLine();
                    break;
                case 5:
                    ModificarProducto(manejoDeProductos);
                    sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                    sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                    oute = Console.ReadLine();
                    break;
                case 6:
                    VerInformacionDeUnProducto(manejoDeProductos);
                    oute = Console.ReadLine();
                    break;
                case 7:
                    Console.WriteLine("Saliendo del panel de administracion...");
                    Thread.Sleep(1000);
                    sistemaDeArchivado.GuardarDatosSinID(usuarios, manejoDeProductos, rutaUsuariosSinID, rutaProductosSinID, repository);
                    sistemaDeArchivado.GuardarDatosConID(usuarios, manejoDeProductos, rutaUsuariosConID, rutaProductosConID, repository);
                    terminar = true;
                    Console.ReadKey();
                    break;
                default:
                    break;
            }    
        } while (terminar == false);
    }

    /// <summary>
    /// Imprime todo lo que tiene cada producto registrado
    /// </summary>
    /// <param name="productos"></param>
    public void VerTodosLosProductos(ManejoDeProductos productos)
    {
        Console.Clear();
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal("Todos los productos registrados");
        Dictionary<int, AniadirProductos> Productos = productos.VerTodosLosProductos();
        foreach(var products in  Productos)
        {
            diseños.RemarcarTexto($"ID Producto: {products.Key}");
            Console.WriteLine($"Nombre del producto: {products.Value.NombreProducto}");
            if(products.Value.Extra != null)
            {
                Console.WriteLine($"Extra del producto: {products.Value.Extra}");
            }
            Console.WriteLine($"Descripción del producto: {products.Value.Descripcion}");
            Console.WriteLine($"Categoria del producto: {products.Value.Categoria}");
            Console.WriteLine($"Precio del producto: {products.Value.Precio:C}");
            Console.WriteLine($"Stock del producto: {products.Value.StockInicial}");
            Console.WriteLine($"Fecha de creación: {products.Value.FechaCreacion}");
        }
    }

    /// <summary>
    /// Mira lo que contiene cada usuario registrado en la pagina
    /// </summary>
    /// <param name="usuarios"></param>
    public void VerTodosLosUsuarios(UsuariosAlmacenados usuarios)
    {
        Console.Clear();
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal("Todos los usuarios registrados");
        Dictionary<int, crearCuentaUsuario> usuario = usuarios.VerUsuarios();
        foreach(var users in usuario)
        {
            diseños.RemarcarTexto($"ID Usuario: {users.Key}");
            Console.WriteLine($"Nombre del usuario: {users.Value.Nombre} {users.Value.SegundoNombre} {users.Value.PrimerApellido} {users.Value.SegundoApellido}");
            Console.WriteLine($"Correo: {users.Value.Correo}");
            Console.WriteLine($"Teléfono: {users.Value.Telefono}");
            Console.WriteLine($"Fecha de creación de cuenta: {users.Value.FechaCreacion}");
        }
    }

    /// <summary>
    /// Añade un producto nuevo al sistema con los parametros y excepciones señalados para evitar
    /// fallos en el sistema
    /// </summary>
    /// <param name="productos"></param>
    public void AniadirNuevoProducto(ManejoDeProductos productos)
    {
        Console.Clear();
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal("Añadir nuevo producto");
        string NombreProducto = string.Empty;
        string extra = string.Empty;
        string descripcion = string.Empty;
        decimal precio = 0m;
        int stock = 0;
        string categoria = string.Empty;
        bool none = false;
            diseños.RemarcarTexto("Nombre del producto");
            NombreProducto = Console.ReadLine();
            diseños.RemarcarTexto("Extra del producto (opcional)");
            extra = Console.ReadLine();
            diseños.RemarcarTexto("Descripcion del producto");
            descripcion = Console.ReadLine();
            diseños.RemarcarTexto("Precio del producto");
        do
        {
            try
            {
                precio = Convert.ToDecimal(Console.ReadLine());
                none = true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro: Dato ingresado no valido, intentelo nuevamente");
                none = false;
            }
        } while (none == false);
        diseños.RemarcarTexto("Stock del producto");
        do
        {
            try
            {
                stock = Convert.ToInt32((Console.ReadLine()));
            }catch(FormatException ex)
            {
                Console.WriteLine("Erro: Dato ingresado no valido, intentelo nuevamente");
                none = false;
            }
        } while (none == false);
        diseños.RemarcarTexto("Categoria del producto");
        categoria = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(extra))
        {
            try
            {
                AniadirProductos productoNuevo = new AniadirProductos(NombreProducto, descripcion, precio, stock, categoria);
                try
                {
                    productos.AgregarProducto(productoNuevo);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        else
        {
            try
            {
                AniadirProductos productoNuevo = new AniadirProductos(NombreProducto, extra, descripcion, precio, stock, categoria);
                try
                {
                    productos.AgregarProducto(productoNuevo);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }

    /// <summary>
    /// Elimina el producto con solo el id requerido
    /// </summary>
    /// <param name="productos"></param>
    public void EliminarProducto(ManejoDeProductos productos)
    {
        Console.Clear();
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal("Eliminar producto");
        int id = 0;
        bool none = false;
            diseños.RemarcarTexto("ID del producto");
            do
            {
                try
                {
                    id = int.Parse(Console.ReadLine());
                    none = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("El dato fue ingresado incorrectamente, ingrese el ID nuevamente");
                    none = false;
                }
            } while (none == false);
        try
        {
            productos.EliminarProducto(id);
        }
        catch (ArgumentException ex)
        {

            Console.WriteLine("Error: " + ex);
        }
    }

    /// <summary>
    /// Metodo que modifica el producto asignado con el id requerido, además
    /// se le exige al usuario que ingrese todos los datos que vaya a modificar
    /// </summary>
    /// <param name="productos"></param>
    public void ModificarProducto(ManejoDeProductos productos)
    {
        Console.Clear();
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal("Modificar Producto");
        int id = 0;
        string NombreProducto = string.Empty;
        string extra = string.Empty;
        string descripcion = string.Empty;
        decimal precio = 0m;
        int stock = 0;
        string categoria = string.Empty;
        bool none = false;
        diseños.RemarcarTexto("ID del producto");
        do
        {
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
                none = true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro: Dato ingresado no valido, intentelo nuevamente");
                none = false;
            }
        } while (none == false);
        diseños.RemarcarTexto("Nombre del producto");
        NombreProducto = Console.ReadLine();
        diseños.RemarcarTexto("Extra del producto (opcional)");
        extra = Console.ReadLine();
        diseños.RemarcarTexto("Descripcion del producto");
        descripcion = Console.ReadLine();
        diseños.RemarcarTexto("Precio del producto");
        do
        {
            try
            {
                precio = Convert.ToDecimal(Console.ReadLine());
                none = true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro: Dato ingresado no valido, intentelo nuevamente");
                none = false;
            }
        } while (none == false);
        diseños.RemarcarTexto("Stock del producto");
        do
        {
            try
            {
                stock = Convert.ToInt32((Console.ReadLine()));
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro: Dato ingresado no valido, intentelo nuevamente");
                none = false;
            }
        } while (none == false);
        diseños.RemarcarTexto("Categoria del producto");
        categoria = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(extra))
        {
            try
            {
                AniadirProductos productoNuevo = new AniadirProductos(NombreProducto, descripcion, precio, stock, categoria);
                try
                {
                    productos.ModificarProducto(id,productoNuevo);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        else
        {
            try
            {
                AniadirProductos productoNuevo = new AniadirProductos(NombreProducto, extra, descripcion, precio, stock, categoria);
                try
                {
                    productos.ModificarProducto(id, productoNuevo);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }

    /// <summary>
    /// Busca el producto gracias al metodo de VerProductoUnico que tiene
    /// de parametro el id a buscar; imprime todo el contenido del producto
    /// a excepcion del id
    /// </summary>
    /// <param name="productos"></param>
    public void VerInformacionDeUnProducto(ManejoDeProductos productos)
    {
        Console.Clear();
        int id = 0;
        bool none = false;
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal("Informacion individual del producto");
        diseños.RemarcarTexto("Ingrese el ID del producto a buscar");
        do
        {
            try
            {
                id = int.Parse(Console.ReadLine());
                none = true;
            }catch(FormatException ex)
            {
                Console.WriteLine("EROR: dato ingresado invalido, intentelo nuevamente " + ex);
                none = false;
            }
        } while (none == false);
        try
        {
            AniadirProductos productoEncontrado = productos.VerProductoUnico(id);
            Console.WriteLine($"Nombre del producto: {productoEncontrado.NombreProducto}");
            if (productoEncontrado.Extra != null)
            {
                Console.WriteLine($"Extra del producto: {productoEncontrado.Extra}");
            }
            Console.WriteLine($"Descripción del producto: {productoEncontrado.Descripcion}");
            Console.WriteLine($"Categoria del producto: {productoEncontrado.Categoria}");
            Console.WriteLine($"Precio del producto: {productoEncontrado.Precio:C}");
            Console.WriteLine($"Stock del producto: {productoEncontrado.StockInicial}");
            Console.WriteLine($"Fecha de creación: {productoEncontrado.FechaCreacion}");
            Console.WriteLine();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("ERRO: " + ex);
        }
    }
}