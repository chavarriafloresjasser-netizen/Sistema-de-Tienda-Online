using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
public class Repository
{
    public Repository()
    {
    }
    /// <summary>
    /// Guarda los datos de usuarios y productos en archivos XML. 
    /// Llama a los métodos específicos para guardar usuarios y productos, pasando las rutas de archivo correspondientes.
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    /// <param name="rutaUsuariosSinID"></param>
    /// <param name="rutaProductosSinID"></param>
    public void GuardarDatosSinID(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuariosSinID, string rutaProductosSinID)
    {
        GuardarUsuariosSinID(usuarios, rutaUsuariosSinID);
        GuardarProductosSinID(productos, rutaProductosSinID);
    }

    /// <summary>
    /// Guarda los datos de usuarios y productos en archivos XML, incluyendo sus IDs.
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    /// <param name="rutaUsuariosConID"></param>
    /// <param name="rutaProductosConID"></param>
    public void GuardarDatosConID(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuariosConID, string rutaProductosConID)
    {
        GuardarUsuariosConID(usuarios, rutaUsuariosConID);
        GuardarProductosConID(productos, rutaProductosConID);
    }
    /// <summary>
    /// Guarda los usuarios en un archivo XML. Cada usuario se representa como un elemento 
    /// "Usuario" con subelementos para el nombre, correo, contraseña y teléfono.
    /// </summary>
    /// <param name="Usuarios"></param>
    /// <param name="rutaArchivo"></param>
    private void GuardarUsuariosSinID(UsuariosAlmacenados Usuarios, string rutaArchivo)
    {
        try
        {
            XElement xmlUsuarios = new XElement("Usuarios",
                from usuarios in Usuarios.UsuariosSinID
                select new XElement("Usuario",
                    new XElement("Nombre", usuarios.Nombre),
                    new XElement("SegundoNombre", usuarios.SegundoNombre),
                    new XElement("PrimerApellido", usuarios.PrimerApellido),
                    new XElement("SegundoApellido", usuarios.SegundoApellido),
                    new XElement("Correo", usuarios.Correo),
                    new XElement("Contraseña", usuarios.Contraseña),
                    new XElement("Telefono", usuarios.Telefono),
                    new XElement("Fechacreacion", usuarios.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"))
                )
            );
            xmlUsuarios.Save(rutaArchivo);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("No tienes permiso para guardar los usuarios: " + ex.Message);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Ruta de archivo no encontrada al guardar los usuarios: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error de entrada/salida al guardar los usuarios: " + ex.Message);
        }
        catch(InvalidOperationException ex)
        {
            Console.WriteLine("Operación no válida al guardar los usuarios: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar los usuarios: " + ex.Message);
        }
    }

    /// <summary>
    /// Guarda los productos en un archivo XML. Cada producto se representa como un elemento
    /// "Producto" con subelementos para el nombre, extras, precio, descripción, categoría y stock.
    /// </summary>
    /// <param name="productos"></param>
    /// <param name="rutaArchivo"></param>
    private void GuardarProductosSinID(ManejoDeProductos productos, string rutaArchivo)
    {
        try
        {
            XElement xmlProductos = new XElement("Productos",
                from producto in productos.ProductosConID.Values
                select new XElement("Producto",
                    new XElement("Nombre", producto.NombreProducto),
                    new XElement("Extras", producto.Extra),
                    new XElement("Precio", producto.Precio),
                    new XElement("Descripcion", producto.Descripcion),
                    new XElement("Categoria", producto.Categoria),
                    new XElement("Stock", producto.StockInicial),
                    new XElement("FechaCreacion", producto.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"))
                )
            );
            xmlProductos.Save(rutaArchivo);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("No tienes permiso para guardar los productos: " + ex.Message);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Ruta de archivo no encontrada al guardar los productos: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error de entrada/salida al guardar los productos: " + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Operación no válida al guardar los productos: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar los productos: " + ex.Message);
        }
    }

    /// <summary>
    /// Guarda los usuarios con sus IDs en un archivo XML. Cada usuario se representa como un elemento
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="rutaArchivo"></param>
    private void GuardarUsuariosConID(UsuariosAlmacenados usuarios, string rutaArchivo)
    {
        try
        {
            XElement xmlUsuarios = new XElement("Usuarios",
                from Usuarios in usuarios.UsuariosConID
                select new XElement("Usuario",
                    new XElement("ID", Usuarios.Key),
                    new XElement("Nombre", Usuarios.Value.Nombre),
                    new XElement("SegundoNombre", Usuarios.Value.SegundoNombre),
                    new XElement("PrimerApellido", Usuarios.Value.PrimerApellido),
                    new XElement("SegundoApellido", Usuarios.Value.SegundoApellido),
                    new XElement("Correo", Usuarios.Value.Correo),
                    new XElement("Contraseña", Usuarios.Value.Contraseña),
                    new XElement("Telefono", Usuarios.Value.Telefono),
                    new XElement("Fechacreacion", Usuarios.Value.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"))
                )
            );
            xmlUsuarios.Save(rutaArchivo);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("No tienes permiso para guardar los usuarios: " + ex.Message);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Ruta de archivo no encontrada al guardar los usuarios: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error de entrada/salida al guardar los usuarios: " + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Operación no válida al guardar los usuarios: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar los usuarios: " + ex.Message);
        }
    }

    private void GuardarProductosConID(ManejoDeProductos productos, string rutaArchivo)
    {
        try
        {
            XElement xmlProductos = new XElement("Productos",
                from producto in productos.ProductosConID
                select new XElement("Producto",
                    new XElement("ID", producto.Key),
                    new XElement("Nombre", producto.Value.NombreProducto),
                    new XElement("Extras", producto.Value.Extra),
                    new XElement("Precio", producto.Value.Precio),
                    new XElement("Descripcion", producto.Value.Descripcion),
                    new XElement("Categoria", producto.Value.Categoria),
                    new XElement("Stock", producto.Value.StockInicial),
                    new XElement("FechaCreacion", producto.Value.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"))
                )
            );
            xmlProductos.Save(rutaArchivo);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("No tienes permiso para guardar los productos: " + ex.Message);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Ruta de archivo no encontrada al guardar los productos: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error de entrada/salida al guardar los productos: " + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Operación no válida al guardar los productos: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar los productos: " + ex.Message);
        }
    }
    private void GuardarCarritoDeCompras()
    {
        // Implementación para guardar el carrito de compras en un archivo XML
    }


    /// <summary>
    /// Carga los datos de usuarios y productos desde archivos XML. Llama a los métodos específicos 
    /// para cargar usuarios y productos, pasando las rutas de archivo correspondientes.
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    /// <param name="rutaUsuariosSinID"></param>
    /// <param name="rutaProductosSinID"></param>
    public void CargarDatosSinID(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuariosSinID, string rutaProductosSinID)
    {
        try
        {
            // Usuarios (sin ID)
            if (File.Exists(rutaUsuariosSinID))
            {
                var doc = XDocument.Load(rutaUsuariosSinID);
                usuarios.UsuariosSinID = doc.Root?
                    .Elements("Usuario")
                    .Select(x =>
                    {
                        string nombre = x.Element("Nombre")?.Value ?? string.Empty;
                        string correo = x.Element("Correo")?.Value ?? string.Empty;
                        string contraseña = x.Element("Contraseña")?.Value ?? string.Empty;
                        int telefono = int.TryParse(x.Element("Telefono")?.Value, out var t) ? t : 0;
                        string segundo = x.Element("SegundoNombre")?.Value ?? string.Empty;
                        string pApellido = x.Element("PrimerApellido")?.Value ?? string.Empty;
                        string sApellido = x.Element("SegundoApellido")?.Value ?? string.Empty;
                        DateTime fecha = DateTime.TryParse(x.Element("FechaCreacion")?.Value, out var fc) ? fc : DateTime.Now;

                        return new crearCuentaUsuario(nombre, correo, contraseña, telefono, segundo, pApellido, sApellido);
                    })
                    .ToList() ?? new List<crearCuentaUsuario>();
            }
            else usuarios.UsuariosSinID = new List<crearCuentaUsuario>();

            // Productos (sin ID)
            if (File.Exists(rutaProductosSinID))
            {
                var docP = XDocument.Load(rutaProductosSinID);
                productos.ProductosSinID = docP.Root?
                    .Elements("Producto")
                    .Select(x =>
                    {
                        string nombre = x.Element("Nombre")?.Value ?? string.Empty;
                        string extra = x.Element("Extras")?.Value ?? string.Empty;
                        string descripcion = x.Element("Descripcion")?.Value ?? string.Empty;
                        decimal precio = decimal.TryParse(x.Element("Precio")?.Value, out var p) ? p : 0m;
                        int stock = int.TryParse(x.Element("Stock")?.Value, out var s) ? s : 0;
                        string categoria = x.Element("Categoria")?.Value ?? string.Empty;
                        DateTime fecha = DateTime.TryParse(x.Element("FechaCreacion")?.Value, out var fc) ? fc : DateTime.Now;

                        if (string.IsNullOrWhiteSpace(extra))
                            return new AniadirProductos(nombre, descripcion, precio, stock, categoria);
                        else
                            return new AniadirProductos(nombre, extra, descripcion, precio, stock, categoria);
                    })
                    .ToList() ?? new List<AniadirProductos>();
            }
            else productos.ProductosSinID = new List<AniadirProductos>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar datos sin ID: " + ex.Message);
        }
    }

    /// <summary>
    /// Carga los datos de usuarios y productos desde archivos XML, incluyendo sus IDs. Cada usuario y producto se representa como un elemento
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    /// <param name="rutaUsuariosConID"></param>
    /// <param name="rutaProductosConID"></param>
    public void CargarDatosConID(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuariosConID, string rutaProductosConID)
    {
        /*Por cada elemento del XML que se va añadiendo se crea un mini condicional declarado con "??" en el cual
         se verifica que si el valor es nulo, se asigna un valor por defecto. El que mas o menos cambia es el id
         que solo se utiliza "?" para asignar el valor 0 dado caso el valor sea nulo*/
        try
        {
            // Cargar usuarios con ID
            if (File.Exists(rutaUsuariosConID))
            {
                var docU = XDocument.Load(rutaUsuariosConID);
                usuarios.UsuariosConID = docU.Root?
                    .Elements("Usuario")
                    .Select(x =>
                    {
                        int id = int.TryParse(x.Element("ID")?.Value, out var tmpId) ? tmpId : 0;
                        string nombre = x.Element("Nombre")?.Value ?? string.Empty;
                        string segundoNombre = x.Element("SegundoNombre")?.Value ?? string.Empty;
                        string primerApellido = x.Element("PrimerApellido")?.Value ?? string.Empty;
                        string segundoApellido = x.Element("SegundoApellido")?.Value ?? string.Empty;
                        string correo = x.Element("Correo")?.Value ?? string.Empty;
                        string contraseña = x.Element("Contraseña")?.Value ?? string.Empty;
                        int telefono = int.TryParse(x.Element("Telefono")?.Value, out var t) ? t : 0;
                        DateTime fecha = DateTime.TryParse(x.Element("FechaCreacion")?.Value, out var fc) ? fc : DateTime.Now;
                        var usuario = new crearCuentaUsuario(nombre, correo, contraseña, telefono, segundoNombre, primerApellido, segundoApellido);
                        return new { id, usuario };
                    })
                    .Where(p => p.id != 0)
                    .ToDictionary(p => p.id, p => p.usuario) ?? new Dictionary<int, crearCuentaUsuario>();
            }
            else
            {
                usuarios.UsuariosConID = new Dictionary<int, crearCuentaUsuario>();
            }

            // Cargar productos con ID
            if (File.Exists(rutaProductosConID))
            {
                var docP = XDocument.Load(rutaProductosConID);
                productos.ProductosConID = docP.Root?
                    .Elements("Producto")
                    .Select(x =>
                    {
                        int id = int.TryParse(x.Element("ID")?.Value, out var pid) ? pid : 0;
                        string nombre = x.Element("Nombre")?.Value ?? string.Empty;
                        string extra = x.Element("Extras")?.Value ?? string.Empty;
                        string descripcion = x.Element("Descripcion")?.Value ?? string.Empty;
                        decimal precio = decimal.TryParse(x.Element("Precio")?.Value, out var pr) ? pr : 0m;
                        int stock = int.TryParse(x.Element("Stock")?.Value, out var st) ? st : 0;
                        string categoria = x.Element("Categoria")?.Value ?? string.Empty;
                        DateTime fecha = DateTime.TryParse(x.Element("FechaCreacion")?.Value, out var fc) ? fc : DateTime.Now;

                        AniadirProductos producto;
                        if (!string.IsNullOrWhiteSpace(extra))
                            producto = new AniadirProductos(nombre, extra, descripcion, precio, stock, categoria);
                        else
                            producto = new AniadirProductos(nombre, descripcion, precio, stock, categoria);

                        return new { id, producto };
                    })
                    .Where(p => p.id != 0)
                    .ToDictionary(p => p.id, p => p.producto) ?? new Dictionary<int, AniadirProductos>();
            }
            else
            {
                productos.ProductosConID = new Dictionary<int, AniadirProductos>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar datos con ID: " + ex.Message);
        }
    }

    /// <summary>
    /// Sistema que guarda todos los carritos de todos los usuarios
    /// del sistema, en el cual se va a generar un archivo XML que se va
    /// a cargar a priori
    /// </summary>
    /// <param name="carritos"></param>
    /// <param name="rutaCarrito"></param>
    public void GuardarCarritos(ManejoCarrito carritos, string rutaCarrito)
    {
        try
        {
            XElement xmlCarritos = new XElement("Carritos",
                from carrito in carritos.Carritos
                select new XElement("Carrito",
                    new XElement("ID", carrito.Key),
                    new XElement("FechaDeCreacion", carrito.Value.FechaDeCreacion.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("UltimoCambio", carrito.Value.UltimoCambio.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("ListaDeProductos",
                        from prod in carrito.Value.ProductosEnCarrito
                        select new XElement("Producto",
                            new XElement("Nombre", prod.NombreProducto),
                            prod.Extra != null ? new XElement("Extras", prod.Extra) : null,
                            new XElement("Precio", prod.Precio),
                            new XElement("Descripcion", prod.Descripcion),
                            new XElement("Categoria", prod.Categoria),
                            new XElement("Stock", prod.StockInicial),
                            new XElement("FechaCreacion", prod.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"))
                        )
                    )
                )
            );
            xmlCarritos.Save(rutaCarrito);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("No tienes permiso para guardar los productos: " + ex.Message);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Ruta de archivo no encontrada al guardar los productos: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error de entrada/salida al guardar los productos: " + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Operación no válida al guardar los productos: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar los productos: " + ex.Message);
        }
    }

    /// <summary>
    /// Metodo que carga los carritos asignados al archivo xml
    /// </summary>
    /// <param name="carritos"></param>
    /// <param name="rutaCarritos"></param>
    public void CargarCarritos(ManejoCarrito carritos, string rutaCarritos)
    {
        try
        {
            if(File.Exists(rutaCarritos))
            {
                var docCart = XDocument.Load(rutaCarritos);
                // Root es <Carritos> y cada elemento es <Carrito>
                var dict = docCart.Root?
                    .Elements("Carrito")
                    .Select(x =>
                    {
                        int id = int.TryParse(x.Element("ID")?.Value, out var pid) ? pid : 0;
                        DateTime fecha = DateTime.TryParse(x.Element("FechaDeCreacion")?.Value, out var fc) ? fc : DateTime.Now;
                        DateTime ultimo = DateTime.TryParse(x.Element("UltimoCambio")?.Value, out var uc) ? uc : DateTime.Now;

                        var carritoObj = new CarritoDeCompras();
                        carritoObj.SetFechaDeCreacion(fecha);
                        carritoObj.UltimoCambio = ultimo;

                        var productos = x.Element("ListaDeProductos")?.Elements("Producto")
                            .Select(p =>
                            {
                                string nombre = p.Element("Nombre")?.Value ?? string.Empty;
                                string extra = p.Element("Extras")?.Value ?? string.Empty;
                                string descripcion = p.Element("Descripcion")?.Value ?? string.Empty;
                                decimal precio = decimal.TryParse(p.Element("Precio")?.Value, out var pr) ? pr : 0m;
                                int stock = int.TryParse(p.Element("Stock")?.Value, out var st) ? st : 0;
                                string categoria = p.Element("Categoria")?.Value ?? string.Empty;
                                // FechaCreacion en producto se ignora para reconstrucción si no es necesaria

                                if (!string.IsNullOrWhiteSpace(extra))
                                    return (AniadirProductos)new AniadirProductos(nombre, extra, descripcion, precio, stock, categoria);
                                else
                                    return (AniadirProductos)new AniadirProductos(nombre, descripcion, precio, stock, categoria);
                            })
                            .ToList() ?? new List<AniadirProductos>();

                        carritoObj.ProductosEnCarrito.AddRange(productos);

                        return new { id, carritoObj };
                    })
                    .Where(p => p.id != 0)
                    .ToDictionary(p => p.id, p => p.carritoObj) ?? new Dictionary<int, CarritoDeCompras>();

                carritos.Carritos = dict;
            }
            else
            {
                carritos.Carritos = new Dictionary<int, CarritoDeCompras>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar datos con ID: " + ex.Message);
        }
    }
}