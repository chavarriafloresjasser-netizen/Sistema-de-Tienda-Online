public sealed class UsuariosAlmacenados
{
    /// <summary>
    /// Diccionario que almacena los usuarios con un ID único como clave, lo que permite 
    /// una gestión eficiente de los usuarios registrados.
    /// </summary>
    public Dictionary<int, crearCuentaUsuario> UsuariosConID;
    /// <summary>
    /// Lista que almacena los usuarios sin un ID asignado, lo que permite una gestión flexible de 
    /// los usuarios en proceso de registro o sin necesidad de un identificador único.
    /// </summary>
    public List<crearCuentaUsuario> UsuariosSinID;
    public UsuariosAlmacenados()
    {
        UsuariosSinID = new List<crearCuentaUsuario>();
        UsuariosConID = new Dictionary<int, crearCuentaUsuario>();
    }
    /// <summary>
    /// Agrega un nuevo usuario a la colección de usuarios almacenados. El método asigna un ID único
    /// </summary>
    /// <param name="usuario"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void AgregarUsuario(crearCuentaUsuario usuario)
    {
        int id = UsuariosSinID.Count + 1; // Generar un ID único basado en la cantidad de usuarios actuales
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
        if(UsuariosConID.Values.Any(u => u.Correo == usuario.Correo))
            throw new ArgumentException("El correo electrónico ya está registrado.", nameof(usuario));
        if (UsuariosConID.Values.Any(u => u.Telefono == usuario.Telefono))
            throw new ArgumentException("El número de teléfono ya existe.", nameof(usuario));
        UsuariosSinID.Add(usuario);
        UsuariosConID[id] = usuario;
    }

    /// <summary>
    /// Verifica las credenciales de inicio de sesión de un usuario. El método busca el usuario por correo electrónico
    /// </summary>
    /// <param name="correo"></param>
    /// <param name="contraseña"></param>
    /// <exception cref="ArgumentException"></exception>
    public void VerificarInicioDeSecion(string correo, string contraseña)
    {
        var usuario = UsuariosConID.Values.FirstOrDefault(u => u.Correo == correo);
        if (usuario == null)
            throw new ArgumentException("El correo electrónico no está registrado.", nameof(correo));
        if (usuario.Contraseña != contraseña)
            throw new ArgumentException("La contraseña es incorrecta.", nameof(contraseña));
    }
}