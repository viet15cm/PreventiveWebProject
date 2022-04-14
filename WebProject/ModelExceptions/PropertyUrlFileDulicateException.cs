using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelExceptions
{
    public class PropertyUrlFileDulicateException : Exception 
    {
        public PropertyUrlFileDulicateException() : base("Lỗi trùng lặp chỉ mục UrlImg !")
        {

        }

    }
}
