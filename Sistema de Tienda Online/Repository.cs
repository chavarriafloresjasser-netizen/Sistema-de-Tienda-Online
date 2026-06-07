using System.Xml.Linq;
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
    /// <param name="rutaUsuarios"></param>
    /// <param name="rutaProductos"></param>
    public void GuardarDatos(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuarios, string rutaProductos)
    {
        GuardarUsuarios(usuarios, rutaUsuarios);
        GuardarProductos(productos, rutaProductos);
    }
    /// <summary>
    /// Guarda los usuarios en un archivo XML. Cada usuario se representa como un elemento 
    /// "Usuario" con subelementos para el nombre, correo, contraseña y teléfono.
    /// </summary>
    /// <param name="Usuarios"></param>
    /// <param name="rutaArchivo"></param>
    private void GuardarUsuarios(UsuariosAlmacenados Usuarios, string rutaArchivo)
    {
        try
        {
            XElement xmlUsuarios = new XElement("Usuarios",
                from usuarios in Usuarios.UsuariosConID
                select new XElement("Usuario",
                    new XElement("Nombre", usuarios.Value.Nombre),
                    new XElement("Correo", usuarios.Value.Correo),
                    new XElement("Contraseña", usuarios.Value.Contraseña),
                    new XElement("Telefono", usuarios.Value.Telefono)
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
    private void GuardarProductos(ManejoDeProductos productos, string rutaArchivo)
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
                    new XElement("Stock", producto.StockInicial)
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
}