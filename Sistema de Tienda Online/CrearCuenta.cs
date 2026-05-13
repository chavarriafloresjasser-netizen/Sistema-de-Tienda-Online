abstract class CrearCuenta
{
    //Atributos de la clase CrearCuenta que se heredan a las clases hijas
    private string? _nombre;
    private string? _correo;
    private string? _contraseña;
    private int _id;
    private int _telefono;

    //Constructor de la clase CrearCuenta
    public CrearCuenta(string nombre, string correo, string contraseña, int telefono)
    {
        Nombre = nombre;
        Correo = correo;
        Contraseña = contraseña;
        Telefono = telefono;
        CreacionCuenta();
    }
    //Metodos
    //Metodos virtuales (modificables por las clases hijas)
    public virtual string CreacionCuenta()
    {
        return $"La cuenta ha sido creada exitosamente.";
    }

    public virtual void MostrarInformacion()
    {
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Correo: {Correo}");
        Console.WriteLine($"Teléfono: {Telefono}");
    }
    //Metodos abstractos (obligatorios para las clases hijas)
    public abstract void AsignarID();
    //Metodos publicos (no modificables por las clases hijas)
    //Encapsulamiento de los atributos de la clase CrearCuenta
    public string? Nombre
    {
        get => _nombre;
        private set
        {
            if(string.IsNullOrWhiteSpace(value))
                 throw new ArgumentException("El nombre no puede estar vacío.");
            _nombre = value.Trim().ToUpper();
        }
    }
    public string? Correo
    {
        get => _correo;
        private set
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El correo no puede estar vacío.");
            if(!value.Contains("@") || !value.Contains(".com"))
                throw new ArgumentException("El correo no es válido.");
            _correo = value.Trim();
        }
    }

    public string? Contraseña
    {
        get => _contraseña;
        private set
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La contraseña no puede estar vacía.");
            if(value.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");
            _contraseña = value;
        }
    }

    public int Id
    {
        get => _id;
        private set
        {
            if(value <= 0)
                throw new ArgumentException("ID no autorizado");
            _id = value;
        }
    }

    public int Telefono
    {
        get => _telefono;
        private set
        {
            if(value != 8)
                throw new ArgumentException("Número de teléfono no autorizado");
            if (!value.ToString().StartsWith("8") && !value.ToString().StartsWith("5") && !value.ToString().StartsWith("7") && !value.ToString().StartsWith("2"))
                throw new ArgumentException("Número de teléfono no autorizado");
            _telefono = value;
        }
    }
}