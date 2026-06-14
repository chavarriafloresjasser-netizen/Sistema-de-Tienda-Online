public class ManejoCarrito
{
    //Diccionario en el cual se va a almacenar el id del usuario que creo el carrito y el carrito
    public Dictionary<int, CarritoDeCompras> Carritos;
    /// <summary>
    /// Constructor en el cual se le va a asignar el carrito correspondiente
    /// </summary>
    public ManejoCarrito()
    {
        Carritos = new Dictionary<int, CarritoDeCompras>();
    }

    /// <summary>
    /// Agrega un producto al carrito que contiene el usuario propietario del carrito, en el cual verifica
    /// si el producto a añadir existe, luego de eso en un if se verifica que el usuario con el carrito exista
    /// esta parte del código es ambigua, ya que solo está diseñado para marcar si exsite un bug dentro de esta
    /// parte del código
    /// </summary>
    /// <param name="idProducto"></param>
    /// <param name="correo"></param>
    /// <param name="productos"></param>
    /// <param name="usuarios"></param>
    public void AgregarAlCarrito(int idProducto, string correo, ManejoDeProductos productos, UsuariosAlmacenados usuarios)
    {
        var usuario = usuarios.UsuariosConID.FirstOrDefault(u => u.Value.Correo == correo);
        int idUsuario = usuario.Key;
        if (!productos.ProductosConID.TryGetValue(idProducto, out AniadirProductos productoAsignado))
        {
            Console.WriteLine("ERROR: Product not found in system");
            return;
        }

        if (Carritos.TryGetValue(idUsuario, out CarritoDeCompras carritoUsuario))
        {
            carritoUsuario.ProductosEnCarrito.Add(productoAsignado);
            carritoUsuario.UltimoCambio = DateTime.Now;
        }
        else
        {
            Console.WriteLine("ERROR: User not found in system");
        }
    }

    /// <summary>
    /// Con este método el usuario puede eliminar el producto deseado de su carrito
    /// </summary>
    /// <param name="idProducto"></param>
    /// <param name="Correo"></param>
    /// <param name="usuarios"></param>
    /// <param name="productos"></param>
    public void EliminarProductoDelCarrito(int idProducto, string Correo, UsuariosAlmacenados usuarios, ManejoDeProductos productos)
    {
        var usuario = usuarios.UsuariosConID.FirstOrDefault(u => u.Value.Correo == Correo);
        int idUsuario = usuario.Key;
        if (!productos.ProductosConID.TryGetValue(idProducto, out AniadirProductos productoAsignado))
        {
            Console.WriteLine("ERROR: Product not found in system");
            return;
        }
        if (Carritos.TryGetValue(idUsuario, out CarritoDeCompras carritoUsuario))
        {
            carritoUsuario.ProductosEnCarrito.Remove(productoAsignado);
        }
    }

    public void VerCarrito(string Correo, UsuariosAlmacenados usuario, ManejoDeProductos productos, CarritoDeCompras carritoParametro)
    {
        int totalProductosDiferentes = 0;
        int totalProductos = 0;
        decimal TotalMonto = 0m;
        
        // Buscar usuario por correo y obtener su id
        var usuarioEncontrado = usuario.UsuariosConID.FirstOrDefault(u => u.Value.Correo == Correo);
        int idUsuario = usuarioEncontrado.Key;
        if (!Carritos.TryGetValue(idUsuario, out CarritoDeCompras carritoUsuario))
        {
            Console.WriteLine("ERROR: User cart not found.");
            return;
        }

        if (carritoUsuario.ProductosEnCarrito == null || !carritoUsuario.ProductosEnCarrito.Any())
        {
            Console.WriteLine("El carrito está vacío.");
            return;
        }

        // Agrupar productos por nombre (usar nombre o una marca para nulos) y calcular totales
        var grupos = carritoUsuario.ProductosEnCarrito
            .GroupBy(p => p.NombreProducto ?? "<sin_nombre>");

        totalProductosDiferentes = grupos.Count();
        totalProductos = carritoUsuario.ProductosEnCarrito.Count;

        foreach (var grupo in grupos)
        {
            int cantidad = grupo.Count();
            decimal precioUnitario = grupo.First().Precio;
            TotalMonto += precioUnitario * cantidad;
        }

        // Mostrar resumen del carrito
        Console.WriteLine($"Productos diferentes: {totalProductosDiferentes}");
        Console.WriteLine($"Total de productos (cantidad): {totalProductos}");
        Console.WriteLine($"Total monto: {TotalMonto:C}");
        Console.WriteLine();
        foreach (var grupo in grupos)
        {
            var nombre = grupo.Key;
            int cantidad = grupo.Count();
            decimal precioUnitario = grupo.First().Precio;
            decimal subtotal = precioUnitario * cantidad;
            Console.WriteLine($"- {nombre}: {cantidad} x {precioUnitario:C} = {subtotal:C}");
        }
    }

    /// <summary>
    /// Metodo hecho para asignarle el carrito al usuario con su correspondiente id para saber de quien 
    /// es el carrito
    /// </summary>
    /// <param name="usuario"></param>
    /// <param name="carrito"></param>
    public void AsignarCarrito(UsuariosAlmacenados usuario, CarritoDeCompras carrito, string correo)
    {
        var user = usuario.UsuariosConID.FirstOrDefault(e => e.Value.Correo == correo);
        if(!Carritos.ContainsKey(user.Key))
        {
            Carritos.Add(user.Key, carrito);
        }
    }

    /// <summary>
    /// Al momento de aplicar este método se va a aplicar lo siguiente, primero se van a restar productos que tenemos al carritos
    /// a los productos asignados en el ManejoDeProductos, y segundo, se va a vaciar todo el carrito
    /// </summary>
    public void ComprarTodoElCarrito(ManejoDeProductos productos)
    {
        if (productos == null)
            throw new ArgumentNullException(nameof(productos));

        // Recorre cada carrito de cada usuario
        foreach (var kv in Carritos.ToList())
        {
            var idUsuario = kv.Key;
            var carrito = kv.Value;

            if (carrito.ProductosEnCarrito == null || !carrito.ProductosEnCarrito.Any())
                continue;

            // Agrupar productos iguales en el carrito para conocer cantidades
            var agrupados = carrito.ProductosEnCarrito
                .GroupBy(p => new { p.NombreProducto, p.Extra, p.Precio, p.Categoria })
                .Select(g => new { Producto = g.First(), Cantidad = g.Count() })
                .ToList();

            foreach (var item in agrupados)
            {
                try
                {
                    // Buscar el ID del producto en el ManejoDeProductos comparando campos clave
                    var encontrado = productos.ProductosConID
                        .FirstOrDefault(p =>
                            string.Equals(p.Value.NombreProducto, item.Producto.NombreProducto, StringComparison.OrdinalIgnoreCase)
                            && string.Equals(p.Value.Extra ?? string.Empty, item.Producto.Extra ?? string.Empty, StringComparison.OrdinalIgnoreCase)
                            && p.Value.Precio == item.Producto.Precio
                            && string.Equals(p.Value.Categoria ?? string.Empty, item.Producto.Categoria ?? string.Empty, StringComparison.OrdinalIgnoreCase)
                        );

                    if (encontrado.Value == null)
                    {
                        Console.WriteLine($"No se encontró el producto '{item.Producto.NombreProducto}' en el inventario. No se actualizará stock.");
                        continue;
                    }

                    int idProducto = encontrado.Key;
                    var productoEnInventario = productos.ProductosConID[idProducto];

                    if (productoEnInventario.StockInicial < item.Cantidad)
                    {
                        throw new InvalidOperationException(
                            "Stock insuficiente."
                        );
                    }

                    if (productoEnInventario.StockInicial >= item.Cantidad)
                    {
                        productoEnInventario.StockInicial -= item.Cantidad;
                    }
                    else
                    {
                        // Si no hay suficiente stock, restar lo máximo posible y avisar
                        int vendido = productoEnInventario.StockInicial;
                        productoEnInventario.StockInicial = 0;
                        Console.WriteLine($"Solo se pudieron vender {vendido} unidades de '{productoEnInventario.NombreProducto}' (se solicitaron {item.Cantidad}).");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar producto '{item.Producto?.NombreProducto}': {ex.Message}");
                }
            }

            // Vaciar el carrito y actualizar fecha del último cambio
            carrito.ProductosEnCarrito.Clear();
            carrito.UltimoCambio = DateTime.Now;
        }
    }
}