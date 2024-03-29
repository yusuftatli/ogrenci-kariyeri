﻿using System.ComponentModel;

namespace SCA.Entity.Enums
{
    public enum PageType : byte
    {
        [Description("Header sayfası")]
        HeaderPage = 1,

        [Description("Footer sayfası")]
        FooterPage = 2,

        [Description("Her ikisinde de gözüksün")]
        BothOfThem = 3
    }
}
