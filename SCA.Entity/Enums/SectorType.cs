using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum SectorType : byte
    {
        [Description("Kamu")]
        Man = 1,
        [Description("Özel Sektör")]
        Woman = 2,
    }
}
