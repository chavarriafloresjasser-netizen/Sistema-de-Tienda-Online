public sealed class EmpresarialInterface
{
    public EmpresarialInterface()
    {
    }

    /// <summary>
    /// Permite al usuario iniciar sesión en su cuenta empresarial. Solicita el correo electrónico y la contraseña, y 
    /// verifica si coinciden con los datos de la cuenta proporcionada. Si las credenciales son correctas, 
    /// se muestra un mensaje de bienvenida y se accede a la administración de la cuenta. 
    /// Si las credenciales son incorrectas, se bloquea el intento durante 40 segundos antes de permitir un nuevo intento.
    /// </summary>
    /// <param name="cuenta"></param>
    public void IniciarSesion(CuentaEmpresarial cuenta)
    {
        Console.Clear();
        bool opcion = false;
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal("Iniciar Seción");
        do
        {
            diseños.RemarcarTexto("Ingrese el correo electrónico:");
            string? correo = Console.ReadLine();
            diseños.RemarcarTexto("Ingrese la contraseña:");
            string? contraseña = Console.ReadLine();
            diseños.RemarcarTexto("Ingrese el NIT:");
            string? nit = Console.ReadLine();

            if (correo == cuenta.Correo! && contraseña == cuenta.Contraseña && nit == cuenta.Nit)
            {
                Console.Clear();
                diseños.RemarcarTexto($"¡Bienvenido a la administración de {cuenta.Nombre}!");
                opcion = true;
            }
            else
            {
                diseños.RemarcarTexto("Incorrect values");
                for (int i = 40; i > 0; i--)
                {
                    Console.Clear();
                    Console.Write($"\rIntento bloqueado. Reintente en {i} segundos...");
                    Thread.Sleep(1000);
                }
            }
        } while (opcion == false);
        Inicio(cuenta);
    }
    /// <summary>
    /// Permite al usuario acceder a la administración de su cuenta empresarial después de iniciar sesión correctamente.
    /// </summary>
    /// <param name="cuenta"></param>
    public void Inicio(CuentaEmpresarial cuenta)
    {
        Console.Clear();
        DiseñosGenerales diseños = new DiseñosGenerales();
        diseños.RecuadroPrincipal($"Administración de {cuenta.Nombre}");
        diseños.RemarcarTexto("¿Qué quiere administrar?");
    }

    public void VerTodosLosProductos()
    {
    }

    public void VerTodosLosUsuarios()
    {
    }

    public void VerTodasLasVentas()
    {
    }

    public void AniadirNuevoProducto()
    {
    }

    public void EliminarProducto()
    {
    }
    public void ModificarProducto()
    {
    }
    public void VerInformacionDeUnProducto()
    {
    }
}