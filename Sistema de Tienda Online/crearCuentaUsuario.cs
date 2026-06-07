public sealed class crearCuentaUsuario : CrearCuenta
{
    private string? _segundoNombre;
    private string? _primerApellido;
    private string? _segundoApellido;

    public crearCuentaUsuario(string nombre, string correo, string contraseña, int telefono, string segundoNombre, string primerApellido, string segundoApellido) : base(nombre, correo, contraseña, telefono)
    {
        SegundoNombre = segundoNombre;
        PrimerApellido = primerApellido;
        SegundoApellido = segundoApellido;
    }

    public string? SegundoNombre
    {
        get => _segundoNombre;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El segundo nombre no puede estar vacío.");
            _segundoNombre = value.Trim().ToUpper();
        }
    }

    public string? PrimerApellido
    {
        get => _primerApellido;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El primer apellido no puede estar vacío.");
            _primerApellido = value.Trim().ToUpper();
        }
    }

    public string? SegundoApellido
    {
        get => _segundoApellido;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El segundo apellido no puede estar vacío.");
            _segundoApellido = value.Trim().ToUpper();
        }
    }
}