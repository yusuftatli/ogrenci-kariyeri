using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum QuestionType : byte
    {
        [Display(Name = "Kısa Cevap")]
        ShortAnswer = 1,
        [Display(Name = "Text format")]
        TextFormat = 2
    }
}
