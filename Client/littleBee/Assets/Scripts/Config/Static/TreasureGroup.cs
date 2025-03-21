using Synchronize.Game.Lockstep.Config.Static.Interface;

namespace Synchronize.Game.Lockstep.Config.Static
{
    public class TreasureGroup:ICFG
    {
        public int ConfigId { get; set; }
        public int GroupId{ get; set; }
        public int TreasureId { get; set; }
        public int Prob { get; set; }
    }
}