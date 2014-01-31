using System;

namespace MarvelPortable.Model
{
    public class MarvelException : Exception
    {
        public MarvelException(int code, string status)
        {
            Code = code;
            Status = status;
        }

        public int Code { get; set; }
        public string Status { get; set; }
    }
}
