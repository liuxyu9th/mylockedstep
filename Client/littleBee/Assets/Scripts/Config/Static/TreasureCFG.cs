using Synchronize.Game.Lockstep.Config.Static.Interface;

namespace Synchronize.Game.Lockstep.Config.Static
{
    public class TreasureCFG:ICFG,IObjectCFG
    {
        public int ConfigId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int ResKey { get; set; }
        public int Mass { get; set; }
        public int Diameter { get; set; }
        
        public int EffectId{ get; set; }
    }
}