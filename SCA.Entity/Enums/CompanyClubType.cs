using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum CompanyClupType : byte
    {
        [Display(Name = "Company")]
        Man = 1,
        [Display(Name = "Club")]
        Woman = 2,
    }
}
