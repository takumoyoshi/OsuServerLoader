using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkayuLoader.Services
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
            "",
            "-devserver risunasa.xyz",
            "-devserver ussr.pl",
            "-devserver ascension.wtf",
            "-devserver heia.kim",
            "-devserver lisek.world",
            "-devserver ripple.moe",
            "-devserver gatari.pw",
            "-devserver halcyon.moe",
            "-devserver ez-pp.farm",
            "-devserver akatsuki.pw"
        };
    }
}
