namespace IntegracionLogin
{
    public static class ContratoHandoffModulo
    {
        public const string PrefijoRuta = "api/handoff";

        public static string ClaimPorSlug(string slug) => $"acceso_{slug?.Trim()}";
    }
}
