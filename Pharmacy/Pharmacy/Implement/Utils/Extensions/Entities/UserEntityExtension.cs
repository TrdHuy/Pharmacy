using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HPSolutionCCDevPackage.netFramework.AtumImageView;

namespace Pharmacy.Implement.Utils.Extensions.Entities
{
    public static class UserEntityExtension
    {
        private static Dictionary<tblUser, UserData> cacheUserDataList = new Dictionary<tblUser, UserData>();

#nullable enable
        public static UserData GetUserData(this tblUser? user)
        {
            RegisterUser(user);

            return cacheUserDataList[user];
        }

        public static void SetUserData(this tblUser user, UserData userData)
        {
            if (cacheUserDataList.ContainsKey(user))
            {
                cacheUserDataList[user] = userData;
            }
            else
            {
                RegisterUser(user);
                cacheUserDataList[user] = userData;
            }
        }

        public static string GetUserDataJSON(this tblUser? user)
        {
            if (user != null)
            {
                var data = cacheUserDataList[user];
                if (data != null)
                {
                    var jsonData = JsonConvert.SerializeObject(data);
                    return jsonData;
                }
            }
            return "";
        }

        public static void ClearCache(this tblUser user)
        {
            cacheUserDataList.Clear();
        }

#nullable enable
        private static void RegisterUser(tblUser? user)
        {
            if (!cacheUserDataList.ContainsKey(user))
            {
                var data = new UserData();
                if (!String.IsNullOrEmpty(user.UserDataJSON))
                {
                    data = JsonConvert.DeserializeObject<UserData>(user.UserDataJSON);
                }
                cacheUserDataList[user] = data;
            }
        }


    }

    public class UserData
    {
        public AtumUserData PersonalAvatarInfo { get; set; }

    }
}
