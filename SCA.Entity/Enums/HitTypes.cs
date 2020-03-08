using System.ComponentModel.DataAnnotations;

namespace SCA.Entity.Enums
{
    public enum HitTypes : byte
    {
        [Display(Name = "HeadLine")]
        HeadLine = 1,
        [Display(Name = "Manset")]
        Manset = 2,
        [Display(Name = "MostPopuler")]
        MostPopuler = 3,
        [Display(Name = "MainMenu")]
        MainMenu = 4,
        [Display(Name = "DailyMostPopuler")]
        DailyMostPopuler = 5,
        [Display(Name = "ConstantMainMenu")]
        ConstantMainMenu = 7,
        [Display(Name = "LastAssay")]
        LastAssay = 8,
        [Display(Name = "CategoryNews")]
        CategoryNews = 9
    }
}
