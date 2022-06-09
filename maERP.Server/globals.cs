global using Microsoft.EntityFrameworkCore.Design;


class Globals
{
    public static string maxLength(string str, int length)
    {
        if (str.Length > length) str = str.Substring(0, length);

        return str;
    }
}