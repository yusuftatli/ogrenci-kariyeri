using System.ComponentModel;

namespace SCA.Entity.Enums
{
    public enum CrudType : byte
    {
        [Description("Insert")]
        Insert = 1,

        [Description("Update")]
        Update = 2,

        [Description("Delete")]
        Delete = 3,
        [Description("List")]
        List = 1,
    }
}