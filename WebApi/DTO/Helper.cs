using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.DTO
{
    public class Helper
    {
        public static string ConvertEnumToString(Status status)
        {
            return status.ToString();
        }

        public static Status ConvertStringToEnum(string status)
        {
            return (Status)Enum.Parse(typeof(Status), status);
        }

        //serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    }

    
}
