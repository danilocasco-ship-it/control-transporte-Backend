namespace IntegracionLogin
{
    public class OpcionesPortalLogin
    {
        public const string Seccion = "PortalLogin";

        public bool Activo { get; set; } = true;
        public string Slug { get; set; } = string.Empty;
        public string UrlFrontend { get; set; } = string.Empty;
        public int SegundosExpiracionSesion { get; set; } = 90;
        public OpcionesJwtHandoff JwtHandoff { get; set; } = new();
    }

    public class OpcionesJwtHandoff
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
