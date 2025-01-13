using System.Text.RegularExpressions;

namespace SistemaDeVendas.Utils
{
    public static class ValidadorDeEmail
    {
        public static bool ValidarEmail(string email)
        {
            string padraoEmail = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, padraoEmail);
        }
    }
}
