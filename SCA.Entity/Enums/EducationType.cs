using System.ComponentModel;

namespace SCA.Entity.Enums
{
    public enum EducationType : long
    {
        [Description("Lise")]
        HighSchool = 1,

        [Description("Üniversite")]
        University = 2,

        [Description("Yüksek Lisans")]
        Master = 3
    }
}
