public class CarritoDeCompras
{
    private DateTime _fechaDeCreacion;
    private DateTime _ultimoCambio;
    private decimal _total;
    public CarritoDeCompras(UsuariosAlmacenados usuarios, ManejoDeProductos productos)
    {
        FechaDeCreacion = DateTime.Now;
        UltimoCambio = DateTime.Now;
        Total = 0m;
    }
    
    public void AgregarAlCarrito()
    {
        
    }

    public void EliminarDelCarrito()
    {

    }

    public DateTime FechaDeCreacion
    {
        get => _fechaDeCreacion;
        private set => _fechaDeCreacion = value;
    }

    public DateTime UltimoCambio
    {
        get => _ultimoCambio;
        private set
        {
            if(value < FechaDeCreacion)
                throw new ArgumentException("La fecha del último cambio no puede ser anterior a la fecha de creación del carrito.");
            _ultimoCambio = value;
        }
    }

    public decimal Total
    {
        get => _total;
        private set
        {
            if (value < 0)
                throw new ArgumentException("El total del carrito no puede ser negativo.");
            _total = value;
        }
    }
}  