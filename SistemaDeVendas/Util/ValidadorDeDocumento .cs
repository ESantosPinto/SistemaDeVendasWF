using System.Linq;

public static class ValidadorDeDocumento
{
    public static bool ValidarCpf(string cpf)
    {
        cpf = StringUtils.RemoverFormatacao(cpf);
        if (cpf.Length != 11 || !long.TryParse(cpf, out _))
            return false;

        var numeros = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        if (numeros.All(n => n == numeros[0]))
            return false;

        for (int j = 9; j <= 10; j++)
        {
            int soma = 0;
            for (int i = 0; i < j; i++)
                soma += numeros[i] * (j + 1 - i);

            int digito = (soma * 10) % 11;
            if (digito == 10)
                digito = 0;

            if (numeros[j] != digito)
                return false;
        }
        return true;
    }

    public static bool ValidarCnpj(string cnpj)
    {
        cnpj = StringUtils.RemoverFormatacao(cnpj);
        if (cnpj.Length != 14 || !long.TryParse(cnpj, out _))
            return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int soma = multiplicador1.Zip(tempCnpj, (m, t) => m * int.Parse(t.ToString())).Sum();

        int resto = (soma % 11);
        int digito = resto < 2 ? 0 : 11 - resto;

        tempCnpj += digito;
        soma = multiplicador2.Zip(tempCnpj, (m, t) => m * int.Parse(t.ToString())).Sum();

        resto = (soma % 11);
        digito = resto < 2 ? 0 : 11 - resto;

        return cnpj.EndsWith(digito.ToString());
    }
}
