using System.Xml.Linq;
public class SistemaDeArchivado
{
    public SistemaDeArchivado()
    {
    }
    /// <summary>
    /// Guarda los datos de los usuarios y productos utilizando el repositorio proporcionado.
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    /// <param name="rutaUsuariosSinID"></param>
    /// <param name="rutaProductosSinID"></param>
    /// <param name="repository"></param>
    public void GuardarDatosSinID(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuariosSinID, string rutaProductosSinID, Repository repository)
    {
        repository.GuardarDatosSinID(usuarios, productos, rutaUsuariosSinID, rutaProductosSinID);
    }

    /// <summary>
    /// Guarda los datos de los usuarios y productos utilizando el repositorio proporcionado, incluyendo los ID de los usuarios y productos.
    /// </summary>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    /// <param name="rutaUsuariosConID"></param>
    /// <param name="rutaProductosConID"></param>
    /// <param name="repository"></param>
    public void GuardarDatosConID(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuariosConID, string rutaProductosConID, Repository repository)
    {
        repository.GuardarDatosConID(usuarios, productos, rutaUsuariosConID, rutaProductosConID);
    }
}