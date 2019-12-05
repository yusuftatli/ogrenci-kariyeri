using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum ReadType : byte
    {
        [Description("Test")]
        Content = 1,
        [Description("Makale")]
        Test = 2
    }

}
