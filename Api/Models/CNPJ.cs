using System;

namespace Api.Models
{
	public class CNPJ
	{
		public string Value { get; private set; }

		protected CNPJ() { }

		public CNPJ(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("CNPJ cannot be null or empty.", nameof(value));

			if (!IsValidCNPJ(value))
				throw new ArgumentException("Invalid CNPJ format.", nameof(value));

			Value = value;
		}
		public static bool IsValidCNPJ(string cnpj)
		{
			if (string.IsNullOrWhiteSpace(cnpj))
				return false;

			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

			if (cnpj.Length != 14)
				return false;

			int[] multiplicador1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

			string tempCNPJ = cnpj.Substring(0, 12);
			int soma = 0;
			for (int i = 0; i < multiplicador1.Length; i++)
			{
				soma += int.Parse(tempCNPJ[i].ToString()) * multiplicador1[i];
			}

			int resto = soma % 11;
			string digito1 = resto < 2 ? "0" : (11 - resto).ToString();

			tempCNPJ += digito1;
			soma = 0;
			for (int i = 0; i < multiplicador2.Length; i++)
			{
				soma += int.Parse(tempCNPJ[i].ToString()) * multiplicador2[i];
			}

			resto = soma % 11;
			string digito2 = resto < 2 ? "0" : (11 - resto).ToString();

			return cnpj.EndsWith(digito1 + digito2);
		}

		public override string ToString()
		{
			return Value;
		}


	}
}
