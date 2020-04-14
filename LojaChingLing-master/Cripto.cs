using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCL
{
    public class Cripto
    {
        //Metodo para codificar, pode usar internal,public ou private...
        public string Base64Encode(string textoencode)
        {
            var textoencodebytes = System.Text.Encoding.UTF8.GetBytes(textoencode);
            return System.Convert.ToBase64String(textoencodebytes);
        }
    }
}
