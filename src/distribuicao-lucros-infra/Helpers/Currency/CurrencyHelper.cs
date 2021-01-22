namespace distribuicao_lucros_infra.Helpers.Currency
{
    public static class CurrencyHelper
    {
        public static string ToCurrency(this double value)
        {
            return value.ToString("C");
        }
    }
}
