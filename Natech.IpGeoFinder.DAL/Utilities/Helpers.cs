using System;

namespace Natech.IpGeoFinder.DAL.Utilities
{
    public static class Helpers
    {
        /// <summary>
        /// If the dateTime is null it will raise error. If nnot it will tcast it to non nullable
        /// </summary>
        /// <param name="dt">Datetime nullable</param>
        /// <returns></returns>
        public static DateTime ToNonNullable(this DateTime? dt)
        {
            if (dt != null)
            {
                return (DateTime)dt;
            }
            else
                throw new Exception("DateTime ToNonNullable faild because the date was null as we didn't expect it");
        }
    }
}
