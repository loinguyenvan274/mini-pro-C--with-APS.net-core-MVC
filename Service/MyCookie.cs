using System;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web_1.Service
{
    public class MyCookie
    {
        public string _value { get; set; }
        public DateTime _expires { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is MyCookie ortherMyCookie)
            {
                return _value == ortherMyCookie._value;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static MyCookie ReadCookie(IRequestCookieCollection cookies, string cookieName)
        {
            if (cookies.ContainsKey(cookieName))
            {
                var cookieValue = cookies[cookieName];
                var cookieExpires = DateTime.Now.AddDays(1);
                return new MyCookie { _value = cookieValue, _expires = cookieExpires };
            }
            return null;
        }
    }
}