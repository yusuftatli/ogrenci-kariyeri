using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum SectorType : byte
    {
        [Display(Name = "Public")]
        Public = 1,
        [Display(Name = "Special")]
        Special = 2
    }
}
