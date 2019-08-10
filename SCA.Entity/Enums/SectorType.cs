using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum SectorType : byte
    {
        [Display(Name = "Public")]
        Man = 1,
        [Display(Name = "Special")]
        Woman = 2,
    }
}
