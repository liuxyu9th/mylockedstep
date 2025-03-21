using System;
using System.Collections.Generic;
using Synchronize.Game.Lockstep.Config.Static;
using Synchronize.Game.Lockstep.Ecsr.Components.Common;
using Synchronize.Game.Lockstep.Ecsr.Components.Star;
using Synchronize.Game.Lockstep.Ecsr.Entitas;
using Synchronize.Game.Lockstep.Managers;
using TrueSync;

namespace Synchronize.Game.Lockstep.Ecsr.Systems
{
    public class SpawnEntitySystem : IEntitySystem
    {
        public EntityWorld World { set; get; }
        private ConfigModule _configModule = ModuleManager.GetModule<ConfigModule>();

        private void SpawnTreasure(StarObjectInfo star)
        {
            int groupId = _configModule.GetConfig<MapElementCFG>(star.ConfigId).TreasureGroupId;
            List<TreasureCFG> list = ConfigModule.TreasureGroupDict[groupId];
            foreach (var cfg in list)
            {
                
            }
        }
        
        public void Execute()
        {
            try
            {
                World.ForEachComponent<StarObjectInfo>(
                    star =>
                    {
                        Hp hp = World.GetComponentByEntityId<Hp>(star.EntityId);
                        if (hp.Value <= 0)
                        {
                            SpawnTreasure(star);
                        }
                    });
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError($"[RemoveEntitySystem] Error {exc.ToString()} {DateTime.Now.ToString()}");
            }
        }
    }
}