public sealed class CuentaEmpresarial : CrearCuenta
{
    private string? _nit;
    public CuentaEmpresarial(string nombre = "Glados Shopping", string correo = "ShopGlados@Glad.com", string contraseña = "X9!vTq#7Lm@2zYp$4Rb^Kd*Wj", int telefono = 22463648, string nit = "J0310000087768") : base(nombre, correo, contraseña, telefono)
    {
        Nit = nit;
    }
    public string? Nit
    {
        get => _nit;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El NIT no puede estar vacío.");
            _nit = value.Trim().ToUpper();
        }
    }
}