using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum GenderType : byte
    {
        [Display(Name = "Bay")]
        Man = 1,
        [Display(Name = "Bayan")]
        Woman = 2,
    }
}
