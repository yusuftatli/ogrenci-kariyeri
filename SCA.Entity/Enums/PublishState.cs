using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum PublishState : byte
    {
        [Description("Taslak")]
        Taslak = 1,
        [Description("Yayın Aşamasında")]
        PublishProcess = 2,
        [Description("Yayında Değil")]
        UnPublish = 3,
        [Description("Yayında")]
        Publish = 4
    }
}
