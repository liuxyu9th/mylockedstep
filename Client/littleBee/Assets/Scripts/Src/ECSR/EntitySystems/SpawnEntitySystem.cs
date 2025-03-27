using System;
using System.Collections.Generic;
using Synchronize.Game.Lockstep.Config.Static;
using Synchronize.Game.Lockstep.Ecsr.Components.Common;
using Synchronize.Game.Lockstep.Ecsr.Components.Star;
using Synchronize.Game.Lockstep.Ecsr.Entitas;
using Synchronize.Game.Lockstep.Managers;
using Synchronize.Game.Lockstep.Managers.Random;
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
            int weightSum = 0;
            foreach (var cfg in list)
            {
                weightSum += cfg.ProbWeight;
            }

            int x = 0;
            TSRandom tsRandom = TSRandom.New((int)star.EntityId);
            int rand = tsRandom.Next(1, weightSum+1);
            for (int i = 0; i < list.Count; i++)
            {
                x += list[i].ProbWeight;
                if (rand <= x)
                {
                    Transform2D tf = World.GetComponentByEntityId<Transform2D>(star.EntityId);
                    EntityManager.CreatTreasure(World,list[i],tf.Position,star.EntityId * 10 + 1);
                    return;
                }
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
                UnityEngine.Debug.LogError($"[SpawnSystem] Error {exc.ToString()} {DateTime.Now.ToString()}");
            }
        }
    }
}