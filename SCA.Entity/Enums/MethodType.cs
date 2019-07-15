using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum MethodType : byte
    {
        [Display(Name = "Kullanıcı Kayıt")]
        LoginCreate = 1
    }
}
