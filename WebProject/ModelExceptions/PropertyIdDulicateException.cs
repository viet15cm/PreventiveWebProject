using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelExceptions
{
    public class PropertyIdDulicateException : Exception
    {
        public PropertyIdDulicateException() : base("Lỗi trùng lặp chỉ Id !")
        {

        }
    }
    
}
