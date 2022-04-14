using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelExceptions
{
    public class PropertyCodeDulicateException : Exception
    {
        public PropertyCodeDulicateException() : base("Lỗi trùng lặp chỉ mục Mã ")
        {

        }
    }
}
