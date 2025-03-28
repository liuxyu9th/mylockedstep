using Synchronize.Game.Lockstep.Managers;

namespace Synchronize.Game.Lockstep.Ecsr.Renderer
{
    public class ShowRender:ActionRenderer
    {
        protected override void OnRender()
        {
            Components.Common.Transform2D transform2d = m_Simulation.GetEntityWorld().GetComponentByEntityId<Components.Common.Transform2D>(EntityId);
            if (transform2d == null)
            {
                ModuleManager.GetModule<PoolModule>().Recycle(GetComponent<PoolObject>().GetFullName(), gameObject);
            }
        }
    }
}