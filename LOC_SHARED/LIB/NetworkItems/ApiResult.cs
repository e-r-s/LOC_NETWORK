using System;
using System.Runtime.Serialization;

namespace LOC_SHARED.NetworkItems
{

    /// <summary>
    /// Used for Api calls
    /// </summary>
    public class BaseApiResult
    {
        public int RequiestId { get; set; }
        public long Time { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }

        [field: NonSerialized]
        [IgnoreDataMember] 
        public string RawData { get; set; }
    }

    /// <summary>
    /// Used for HTTPSERVER result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>  : BaseApiResult
    {
        //public int RequiestId { get; set; }
        //public long Time { get; set; }
        //public string Error { get; set; }
        //public bool Success { get; set; }
        public T Data { get; set; }
    }
}
