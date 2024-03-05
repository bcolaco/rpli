namespace RPLi;

using System.Text.RegularExpressions;

public class RPLi
{
    public static string Render(string rpl)
    {
        rpl = RemoveComments(rpl);
        rpl = ReplaceInterpolations(rpl);

        return rpl;
    }

    private static string RemoveComments(string rpl)
    {
        return Regex.Replace(rpl, "<#--.*-->", string.Empty);
    }

    private static string ReplaceInterpolations(string rpl)
    {
        return Regex.Replace(rpl, @"\${""(.*)""}", "$1");
    }
}
