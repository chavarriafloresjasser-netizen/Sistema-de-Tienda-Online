public sealed class ManejoDeProductos
{
    //Listado de los productos que se vallan agregando a la tienda, con su respectiva información como el nombre, precio y cantidad disponible.
    /*Se utiliza para añadir todos los productos que se vallan alargando a lo largo del tiempo aunque ya no se vendan,
     lo cual permite una gestión flexible de los productos en proceso de registro o sin necesidad de un identificador único.*/
    public List<AniadirProductos> ProductosSinID;
    /*Productos con ID que ya se puedan eliminar sin afectar al funcionamiento del sistema y al sistema de IDs*/
    public Dictionary<int, AniadirProductos> ProductosConID;
    public ManejoDeProductos()
    {
        ProductosSinID = new List<AniadirProductos>();
        ProductosConID = new Dictionary<int, AniadirProductos>();
    }

    /// <summary>
    /// Agrega un nuevo producto a la tienda. El producto se añade tanto a la lista de productos 
    /// sin ID como al diccionario de productos con ID, asignándole un ID único basado en la cantidad actual de productos.
    /// </summary>
    /// <param name="producto"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void AgregarProducto(AniadirProductos producto)
    {
        int id = ProductosSinID.Count + 1; // Generar un ID único basado en la cantidad de productos actuales
        if (producto == null)
            throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo.");
        if (ProductosConID.Values.Any(p => p.NombreProducto == producto.NombreProducto))
            throw new ArgumentException("El producto ya está registrado.", nameof(producto));
        ProductosSinID.Add(producto);
        ProductosConID[id] = producto;
    }

    /// <summary>
    /// Elimina un producto de la tienda utilizando su ID sin afectar al funcionamiento del sistema y al sistema de IDs.
    /// Si el ID no existe, se lanza una excepción.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="ArgumentException"></exception>
    public void EliminarProducto(int id)
    {
        if (!ProductosConID.ContainsKey(id))
            throw new ArgumentException("El ID del producto no existe.", nameof(id));
        var producto = ProductosConID[id];
        ProductosConID.Remove(id);
    }
}