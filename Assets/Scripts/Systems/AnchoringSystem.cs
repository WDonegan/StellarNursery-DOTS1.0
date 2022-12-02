using Unity.Entities;
using Unity.Transforms;
using WJD.Components;

namespace WJD.Systems
{
    public partial struct AnchoringSystem : ISystem 
    {
        public void OnCreate(ref SystemState state)
        {
            
        }

        public void OnDestroy(ref SystemState state)
        {
            
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (anchor, transform) in SystemAPI.Query<Anchored, TransformAspect>())
            {
                transform.Position = anchor.Value;
            }
        }
    }
}