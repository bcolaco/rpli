namespace RPLi;

public class RPLi
{
    public static string Render(string rpl)
    {
        return System.Text.RegularExpressions.Regex.Replace(rpl, "<#--.*-->", string.Empty);
    }
}