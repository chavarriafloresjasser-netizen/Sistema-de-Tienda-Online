public class Diseños
{
    public Diseños()
    {
    }
    /// <summary>
    /// Enciera el texto en un recuadro principal, el cual se puede usar 
    /// para los títulos de cada sección del programa
    /// </summary>
    /// <param name="texto"></param>
    public void RecuadroPrincipal(string texto)
    {
        int cantidadDeCaracteres = texto.Count();
        Console.WriteLine("╔" + new string('═', cantidadDeCaracteres + 2) + "╗");
        Console.WriteLine("║ " + texto + " ║");
        Console.WriteLine("╚" + new string('═', cantidadDeCaracteres + 2) + "╝");
        Console.WriteLine();
        Console.WriteLine();
    }
    /// <summary>
    /// Remarca textos importantes, como preguntas o indicaciones, 
    /// para que el usuario pueda identificarlos fácilmente
    /// </summary>
    /// <param name="texto"></param>
    public void RemarcarTexto(string texto)
    {
        Console.WriteLine("» " + texto + " «");
        Console.WriteLine();
    }

    public void Si_o_No()
    {
        Console.WriteLine("*Si  *No");
        Console.WriteLine();
    }
}