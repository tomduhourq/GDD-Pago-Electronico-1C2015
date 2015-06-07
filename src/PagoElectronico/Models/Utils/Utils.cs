using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Models.Utils
{
    public static class Utils
    {
        public static int mapBoolToBit(bool bit) {
            return bit ? 1 : 0;
        }
    }
}
