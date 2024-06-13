using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuServerLoader.Services
{
    internal class GlobalVars
    {
        public static string[] serverDevFlags
        {
            get
            {
                return _serverDevFlags;
            }
            set
            {
                _serverDevFlags = value;
            }
        }

        public static string[] _serverDevFlags =
        {
            "ppy.sh",
            "osuokayu.moe",
            "ussr.pl",
            "ascension.wtf",
            "heia.kim",
            "lisek.world",
            "ripple.moe",
            "gatari.pw",
            "halcyon.moe",
            "ez-pp.farm",
            "akatsuki.pw"
        };
    }
}
