public static class UserInterface
{ 
    /// <summary>
    /// Inicio de todo el programa, donde le pide al usuario que inicie sesion
    /// si ya posee una, de lo contrario que cree una nueva
    /// </summary>
    public static void Inicio()
    {
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Glados Shoping");
        bool excepcion = false;
        //Frontend
        Console.WriteLine("Sea bienvenido al sistema de tienda online");
        diseñosGenerales.RemarcarTexto("¿Tiene una cuenta ya registrada?");
        diseñosGenerales.Si_o_No();
        string? opcion;
        do
        {
            do
            {
                opcion = Console.ReadLine();

                if (opcion == null)
                {
                    Console.WriteLine("No puede quedar nulo el campo");
                    excepcion = false;
                }
                excepcion = true;
            } while (excepcion == false);

            opcion = opcion.Trim().ToUpper();

            if (opcion != "SI" && opcion != "NO")
            {
                Console.WriteLine("Opción no válida, por favor ingrese 'Si' o 'No'");
                excepcion = false;
            }
        } while (excepcion == false);

        switch (opcion)
        {
            case "SI":
                IniciarSesionEnCuenta();
                break;
            case "NO":
                CrearNuevaCuentaDeUsuario();
                break;
        }
    }

    /// <summary>
    /// Realiza el proceso de creación de una nueva cuenta de usuario,
    /// en el cual todos los campos son obligatorios, si dado caso el
    /// usuario ingresa un dato no válido, se le indicará el error y 
    /// se le solicitará que vuelva a ingresar la información hasta que sea correcta.
    /// </summary>
    public static void CrearNuevaCuentaDeUsuario()
    {
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Registrar nueva cuenta");
        CrearCuentaUsuario? nuevoUsuario = null;
        bool opcion = false;
        string? PrimerNombre = "";
        string? SegundoNombre = "";
        string? PrimerApellido = "";
        string? SegundoApellido = "";
        string? CorreoElectronico = "";
        string? Contraseña = "";
        int Telefono = 0;
        do
        {
            diseñosGenerales.RemarcarTexto("Primer Nombre:");
            PrimerNombre = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Segundo Nombre:");
            SegundoNombre = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Primer Apellido:");
            PrimerApellido = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Segundo Apellido:");
            SegundoApellido = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Correo Electrónico:");
            CorreoElectronico = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Contraseña:");
            Contraseña = Console.ReadLine();
            diseñosGenerales.RemarcarTexto("Teléfono:");
            do
            {
                try
                {
                    Telefono = Convert.ToInt32(Console.ReadLine());
                    opcion = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Por favor ingrese un número válido para el teléfono.");
                    opcion = false;
                }
            } while (opcion == false);

            try
            {
                nuevoUsuario = new CrearCuentaUsuario(PrimerNombre, CorreoElectronico, Contraseña, Telefono, SegundoNombre, PrimerApellido, SegundoApellido);
                opcion = true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                opcion = false;
            }
        } while (opcion == false);
    }

    /// <summary>
    /// CLase hecha para que el usuario que ya posea una cuenta
    /// que pueda iniciar sesión, en el cual se le solicitará 
    /// su correo electrónico y contraseña para poder acceder a 
    /// dicha cuenta
    /// </summary>
    public static void IniciarSesionEnCuenta()
    {
        DiseñosGenerales diseñosGenerales = new DiseñosGenerales();
        diseñosGenerales.RecuadroPrincipal("Iniciar sesión");
        // Lógica para iniciar sesión
    }
}