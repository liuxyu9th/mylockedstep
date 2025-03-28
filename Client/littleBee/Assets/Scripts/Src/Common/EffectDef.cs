using System;
using System.Collections.Generic;
using Synchronize.Game.Lockstep;
using Synchronize.Game.Lockstep.Ecsr.Components.Common;
using Synchronize.Game.Lockstep.Ecsr.Entitas;

public enum EffectType
{
    AddHP = 10001,
    
}
public class EffectDef
{
    private static Dictionary<EffectType, Action<EntityWorld,uint,int[]>> _effectDict = new Dictionary<EffectType, Action<EntityWorld,uint,int[]>>()
    {
        {EffectType.AddHP,AddHp},
    };

    public static void TriggerEffect(EffectType type,EntityWorld world,uint entityId,int[] param)
    {
        _effectDict[type](world,entityId,param);
    }
    
    // param 0 要加的血量
    private static void AddHp(EntityWorld world,uint entityId,int[] param)
    {
        Hp hp = world.GetComponentByEntityId<Hp>(entityId);
        hp.AddHp(param[0]);
    }
}