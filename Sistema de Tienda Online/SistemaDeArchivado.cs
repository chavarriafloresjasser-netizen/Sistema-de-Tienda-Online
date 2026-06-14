using System.Xml.Linq;
public class SistemaDeArchivado
{
    public SistemaDeArchivado()
    {
    }
    /// <summary>
    /// Guarda los datos de los usuarios y productos utilizando el repositorio proporcionado.
    /// Además de hacer el manejos de excepciones al momento de crear carritos
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    /// <param name="rutaUsuariosSinID"></param>
    /// <param name="rutaProductosSinID"></param>
    /// <param name="repository"></param>
    public void GuardarDatosSinID(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuariosSinID, string rutaProductosSinID, Repository repository)
    {
        try
        {
            repository.GuardarDatosSinID(usuarios, productos, rutaUsuariosSinID, rutaProductosSinID);
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
    /// Guarda los datos de los usuarios y productos utilizando el repositorio proporcionado, incluyendo los ID de los usuarios y productos.
    /// Además de añadirle los manejos de excepciones correspondientes
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    /// <param name="rutaUsuariosConID"></param>
    /// <param name="rutaProductosConID"></param>
    /// <param name="repository"></param>
    public void GuardarDatosConID(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuariosConID, string rutaProductosConID, Repository repository)
    {
        try
        { 
        repository.GuardarDatosConID(usuarios, productos, rutaUsuariosConID, rutaProductosConID);
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
    /// Guarda todos los carritos de todos los usuarios correspondientes, además de implementar
    /// las excepciones correspondientes para evitar errores de compilación
    /// </summary>
    /// <param name="carritos"></param>
    /// <param name="rutaCarrito"></param>
    /// <param name="repository"></param>
    public void GuardarCarritos(ManejoCarrito carritos, string rutaCarrito, Repository repository)
    {
        try
        {
            repository.GuardarCarritos(carritos,rutaCarrito);
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
}