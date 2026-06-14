public class CarritoDeCompras
{
    private DateTime _fechaDeCreacion;
    private DateTime _ultimoCambio;
    public List<AniadirProductos> ProductosEnCarrito = new List<AniadirProductos>();
    public CarritoDeCompras(UsuariosAlmacenados usuarios, ManejoDeProductos productos)
    {
        FechaDeCreacion = DateTime.Now;
        UltimoCambio = DateTime.Now;
    }
    public DateTime FechaDeCreacion
    {
        get => _fechaDeCreacion;
        private set => _fechaDeCreacion = value;
    }

    // Permite a Repository restaurar la fecha de creación al cargar desde XML
    internal void SetFechaDeCreacion(DateTime fecha)
    {
        _fechaDeCreacion = fecha;
    }

    public DateTime UltimoCambio
    {
        get => _ultimoCambio;
        set
        {
            if(value < FechaDeCreacion)
                throw new ArgumentException("La fecha del último cambio no puede ser anterior a la fecha de creación del carrito.");
            _ultimoCambio = value;
        }
    }
}  