public static class StringUtils
{
    public static string RemoverFormatacao(string texto)
    {
        return texto.Replace(".", "").Replace("-", "").Replace(" ", "").Replace("/", "");
    }
}
