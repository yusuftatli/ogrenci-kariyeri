using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class SliderContentDto
    {
        public List<ContentForHomePageDTO> SliderContents { get; set; }
        public ContentForHomePageDTO TopContent { get; set; }
        public ContentForHomePageDTO BottomContent { get; set; }
    }
}
