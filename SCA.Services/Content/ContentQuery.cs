using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Services
{
    public static class ContentQuery
    {
        public static string ContentListQuery = @"select 		
            _c.Id,
			_c.Header,
            _c.Writer,
            _c.ReadCount,
            _c.CreatedDate,
            _c.PublishDate,
            _c.PublishStateType,
             case 
				when _c.PublishStateType = 1 then 'Taslak'
                when _c.PublishStateType = 2 then 'Yayın Aşamasında'
                when _c.PublishStateType = 2 then 'Yayında Değil'
                else 'Yayında' 
			end as PublishStateTypeDes,
            _c.PlatformType,
            case 
				when _c.PlatformType = 1 then 'Mobil'
                when _c.PlatformType = 2 then 'Web'
                else 'Web/Mobil' 
			end as PublishStateTypeDes,
            _c.ConfirmUserName
            from Content _c 
            where _c.PublishDate >='@PublishStartDate' and _c.PublishDate <= '@PublishEndate'  order by _c.PublishDate desc Limit 100";

        public static string ContentListQueryWithUser = @"select 		
            _c.Id,
			_c.Header,
            _c.Writer,
            _c.ReadCount,
            _c.CreatedDate,
            _c.PublishDate,
            _c.PublishStateType,
             case 
				when _c.PublishStateType = 1 then 'Taslak'
                when _c.PublishStateType = 2 then 'Yayın Aşamasında'
                when _c.PublishStateType = 2 then 'Yayında Değil'
                else 'Yayında' 
			end as PublishStateTypeDes,
            _c.PlatformType,
            case 
				when _c.PlatformType = 1 then 'Mobil'
                when _c.PlatformType = 2 then 'Web'
                else 'Web/Mobil' 
			end as PublishStateTypeDes,
            _c.ConfirmUserName
            from Content _c 
            where _c.PublishDate >='@PublishStartDate' and _c.PublishDate <= '@PublishEndate' and _c.UserId=@UserId order by _c.PublishDate desc Limit 100";

    }
}
