using System.ComponentModel;

namespace SCA.Entity.Enums
{
    public enum ContentPublishType : byte
    {
        [Description("Normal")]
        Normal = 1,

        [Description("Slider")]
        Slider = 2,

        [Description("Top Content")]
        TopContent = 3,

        [Description("Bottom Content")]
        BottomContent = 4
    }
}