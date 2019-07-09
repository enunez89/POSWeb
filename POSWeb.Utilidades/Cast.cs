using System;
using Newtonsoft.Json;
using System.Reflection;

namespace POSWeb.Utilidades
{
    public static class Cast
    {
        public static T CastObjectJson<T>(this object objOrigen)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(objOrigen));
        }
    }
}
