using System;
using System.Globalization;

namespace DemoWebApp.Settings
{
    public class DemoWebApiSettings
    {
        public const string Section = "DemoWebApiSettings";

        public string Instance { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseAddress { get; set; }
        public string ResourceId { get; set; }
        public string Authority
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                                     Instance, TenantId);
            }
        }
    }
}
