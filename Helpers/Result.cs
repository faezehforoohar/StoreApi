using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Helpers
{
    public class Result<T>
    {
        public T data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public Error error { get; set; }

        public Result(T _data, bool _success, string _message, Error _error)
        {
            data = _data;
            success = _success;
            message = _message;
            error = _error;
        }
        public Result( bool _success, string _message, Error _error)
        {
            success = _success;
            message = _message;
            error = _error;
        }
    }
    public class Error
    {
        public int code { get; set; }
        public List<string> data { get; set; }
    }
    public enum ErrorType
    {
        [Description("Create Error")]
        Create = 1,
        [Description("Update Error")]
        Update = 2,
        [Description("Delete Error")]
        Delete = 3,
        [Description("Login Error")]
        Login = 4,
        [Description("Fetch Error")]
        Fetch = 5,
    }
    public enum SuccessType
    {
        [Description("Create Success")]
        Create = 1,
        [Description("Update Success")]
        Update = 2,
        [Description("Delete Success")]
        Delete = 3,
        [Description("Login Success")]
        Login = 4,
        [Description("Fetch Success")]
        Fetch = 5,
     
    }
}
