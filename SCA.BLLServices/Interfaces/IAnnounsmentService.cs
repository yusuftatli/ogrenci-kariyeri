using SCA.BLLServices.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public interface IAnnounsmentService<U> : IGenericService<U> where U : class
    {
    }
}
