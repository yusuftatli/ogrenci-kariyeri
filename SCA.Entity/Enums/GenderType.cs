using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum GenderType : byte
    {
        [Description("Erkek")]
        Man = 1,
        [Description("Kadın")]
        Woman = 2,
    }
}
