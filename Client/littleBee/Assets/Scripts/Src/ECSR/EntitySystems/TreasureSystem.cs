using Synchronize.Game.Lockstep.Config.Static;
using Synchronize.Game.Lockstep.Ecsr.Components.Common;
using Synchronize.Game.Lockstep.Ecsr.Entitas;
using Synchronize.Game.Lockstep.Managers;

namespace Synchronize.Game.Lockstep.Ecsr.Systems
{
    public class TreasureSystem:IEntitySystem
    {
        public EntityWorld World { get; set; }
        public void Execute()
        {
            World.ForEachComponent<Treasure>(treasure =>
            {
                var tf = World.GetComponentByEntityId<Transform2D>(treasure.EntityId);
                if (tf.CollisionEntityId == 0)
                {
                    return;
                }

                foreach (var id in tf.CollisionEntityIds)
                {
                    if (id < 10)
                    {
                        TreasureCFG treasureCfg = ModuleManager.GetModule<ConfigModule>().GetConfig<TreasureCFG>(treasure.treasureCfgId);
                        for (int i = 0; i < treasureCfg.EffectIds.Length; i++)
                        {
                            EffectDef.TriggerEffect((EffectType)treasureCfg.EffectIds[i],World,tf.CollisionEntityId,treasureCfg.EffectParams[i]);
                        }
                        World.RemoveEntity(treasure.EntityId);
                        return;
                    }
                }
            });
                
        }
    }
}