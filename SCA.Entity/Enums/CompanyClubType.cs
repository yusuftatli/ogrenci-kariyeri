using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum CompanyClupType : byte
    {
        [Display(Name = "Company")]
        Company = 1,
        [Display(Name = "Club")]
        Club = 2
    }
}
