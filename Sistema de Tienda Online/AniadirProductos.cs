public class AniadirProductos
{
    private string? _nombreProducto;
    private string? _extra;
    private string? _descripcion;
    private decimal _precio;
    private int _stockInicial;
    private string? _categoria;
    private DateTime _fechaCreacion;

    public AniadirProductos(string nombreProducto, string descripcion, decimal precio, int stockInicial, string categoria)
    {
        NombreProducto = nombreProducto;
        Descripcion = descripcion;
        FechaCreacion = DateTime.Now;
        Precio = precio;
        StockInicial = stockInicial;
        Categoria = categoria;
    }

    public AniadirProductos(string nombreProducto, string extra, string descripcion, decimal precio, int stockInicial, string categoria)
    {
        NombreProducto = nombreProducto;
        Extra = extra;
        Descripcion = descripcion;
        FechaCreacion = DateTime.Now;
        Precio = precio;
        StockInicial = stockInicial;
        Categoria = categoria;
    }

    public string? NombreProducto
    {
        get => _nombreProducto;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");
            _nombreProducto = value.Trim().ToUpper();
        }
    }

    public string? Extra
    {
        get => _extra;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El campo 'Extra' no puede estar vacío.");
            _extra = value.Trim().ToUpper();
        }
    }

    public string? Descripcion
    {
        get => _descripcion;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La descripción del producto no puede estar vacía.");
            _descripcion = value.Trim().ToUpper();
        }
    }

    public decimal Precio
    {
        get => _precio;
        set
        {
            if (value <= 0)
                throw new ArgumentException("El precio del producto debe ser un valor positivo.");
            _precio = value;
        }
    }

    public int StockInicial
    {
        get => _stockInicial;
        set
        {
            if (value < 0)
                throw new ArgumentException("El stock inicial del producto no puede ser un valor negativo.");
            _stockInicial = value;
        }
    }

    public string? Categoria
    {
        get => _categoria;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La categoría del producto no puede estar vacía.");
            _categoria = value.Trim().ToUpper();
        }
    }

    public DateTime FechaCreacion
    {
        get => _fechaCreacion;
        private set => _fechaCreacion = value;
    }
}