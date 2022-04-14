using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelIdentityErrors
{
    public class AppIdentityErrorDescriber : IdentityErrorDescriber
    {
        public virtual IdentityError DuplicateCodeErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateCodeErorr),
                Description = "Lỗi mã đã tồn tại !!"
            };

        }
        public virtual IdentityError NotFindFileErorr()
        {
            return new IdentityError()
            {
                Code = nameof(NotFindFileErorr),
                Description = "Lỗi không tìm thấy file !!"
            };

        }
        public virtual IdentityError FindNofoundIDErorr()
        {
            return new IdentityError()
            {
                Code = nameof(FindNofoundIDErorr),
                Description = "Lỗi không tìm thấy ID"
            };

        }

        public virtual IdentityError DuplicateUrlFileError()
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateUrlFileError),
                Description = "Lỗi đường dẫn file bị trùng !"
            };
        }

        public virtual IdentityError DbUpdateErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DbUpdateErorr),
                Description = "Lỗi update dữ liệu !!!"
            };
        }

        public virtual IdentityError DataNullErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DataNullErorr),
                Description = "Lỗi không tìm thấy dữ liệu !!!"
            };
        }

        public virtual IdentityError DatabaseAllErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DatabaseAllErorr),
                Description = "Lỗi hệ thống liên hệ Admin !!"
            };
        }
    }
}
