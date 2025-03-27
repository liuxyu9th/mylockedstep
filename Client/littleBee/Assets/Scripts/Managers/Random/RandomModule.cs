using Synchronize.Game.Lockstep.Behaviours;
using Synchronize.Game.Lockstep.Ecsr.Entitas;
using TrueSync;

namespace Synchronize.Game.Lockstep.Managers.Random
{
    public class RandomModule:IModule
    {

        public int Next(int min, int max)
        {
            var seed = SimulationManager.Instance.GetSimulation().GetBehaviour<LogicFrameBehaviour>().CurrentFrameIdx;
            TSRandom tsRandom = TSRandom.New(seed);
            return tsRandom.Next(min, max);
        }

        public void Init()
        {
        }
    }
}