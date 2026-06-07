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
    /// <param name="rutaUsuarios"></param>
    /// <param name="rutaProductos"></param>
    /// <param name="repository"></param>
    public void GuardarDatos(UsuariosAlmacenados usuarios, ManejoDeProductos productos, string rutaUsuarios, string rutaProductos, Repository repository)
    {
        repository.GuardarDatos(usuarios, productos, rutaUsuarios, rutaProductos);
    }
}