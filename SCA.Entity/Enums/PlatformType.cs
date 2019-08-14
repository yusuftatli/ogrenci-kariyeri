using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum PlatformType
    {
        [Description("Mobil")]
        Mobil = 1,
        [Description("Web")]
        Web = 2,
        [Description("Web-Mobil")]
        WebMobil = 3,
    }
}
