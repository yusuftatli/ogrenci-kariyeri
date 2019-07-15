using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum PublishState : byte
    {
        [Display(Name = "Taslak")]
        Taslak = 1,
        [Display(Name = "PublishProcess")]
        PublishProcess = 2,
        [Display(Name = "UnPublish")]
        UnPublish = 3,
        [Display(Name = "Publish")]
        Publish = 4
    }
}
