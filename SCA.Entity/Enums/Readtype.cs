using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum ReadType : int
    {
        [Display(Name = "Test")]
        Test = 1,
        [Display(Name = "Makale")]
        Content = 2
    }


}
