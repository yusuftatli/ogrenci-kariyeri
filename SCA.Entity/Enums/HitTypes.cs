using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum HitTypes : byte
    {
        [Display(Name = "HeadLine")]
        Man = 1,
        [Display(Name = "Manset")]
        Woman = 2,
        [Display(Name = "MostPopuler")]
        MostPopuler = 3,
        [Display(Name = "MainMenu")]
        MainMenu = 4,
        [Display(Name = "DailyMostPopuler")]
        DailyMostPopuler = 5,
        [Display(Name = "ConstantMainMenu")]
        ConstantMainMenu = 7,
    }
}
