using Pharmacy.Implement.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.Definitions
{
    public static class PharmacyDefinitions
    {
        public static readonly int MINIMUM_PASSWORD_LENGHT = 8;
        public static readonly char[] SPECIAL_CHARS_OF_PASSWORD = "!#$%&'()*+,-./:;<=>?@[]^_`{|}~".ToCharArray();
    }

    public enum NewPasswordAwareMessage
    {
        [StringValue("Mật khẩu không được chứa khoảng trắng!")]
        WhiteSpaceAware = 1,
        [StringValue("Mật khẩu mới không được trùng mật khẩu hiện tại!")]
        SameBefore = 2,
        [StringValue("Mật khẩu tối thiểu 8 ký tự!")]
        NotMeetLenght = 3,
        [StringValue("Mật khẩu phải có ít nhất 1 trong các ký tự !#$%&'()*+,-./:;<=>?@[]^_`{|}~")]
        WrongFormat = 4
    }
}
