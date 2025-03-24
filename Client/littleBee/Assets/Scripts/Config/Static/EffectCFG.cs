using System.Collections.Generic;
using Synchronize.Game.Lockstep.Config.Static.Interface;

namespace Synchronize.Game.Lockstep.Config.Static
{
    public class EffectCFG:ICFG
    {
        public int ConfigId { get; set; }
        public int EffectGroupId{ get; set; }
        public int EffectTypeId{ get; set; }
    }
}