using Synchronize.Game.Lockstep.Ecsr.Entitas;
using TrueSync;

namespace Synchronize.Game.Lockstep.Managers.Random
{
    public class RandomModule:IModule
    {

        public int Next(int min, int max)
        {
            var world = SimulationManager.Instance.GetSimulation().GetEntityWorld();
            TSRandom tsRandom = TSRandom.New((int) world.IdManager._createdEntityId);
            return tsRandom.Next(min, max);
        }

        public void Init()
        {
        }
    }
}