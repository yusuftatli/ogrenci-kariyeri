using System.ComponentModel;

namespace SCA.Entity.Enums
{
    public enum EducationType : byte
    {
        [Description("Lise Öğrencisi")]
        HighSchoolStudent = 1,

        [Description("Lise Mezunu")]
        HighSchoolGraduate = 4,

        [Description("Önlisans Öğrencisi")]
        AssociateStudent = 5,

        [Description("Önlisans Mezunu")]
        AssociateGraduate = 6,

        [Description("Üniversite Öğrencisi")]
        UniversityStudent = 2,

        [Description("Üniversite Mezunu")]
        UniversityGraduate = 3,

        [Description("Lisansüstü Öğrencisi")]
        PostGraduateStudent = 8,

        [Description("Lisansüstü Mezunu")]
        PostGraduateGraduate = 9
    }
}