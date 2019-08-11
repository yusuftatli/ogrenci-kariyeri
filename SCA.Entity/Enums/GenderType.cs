using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum GenderType : byte
    {
        [Description("Bay")]
        Man = 1,
        [Description("Bayan")]
        Woman = 2,
    }
}
