using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using WJD.Aspects;

namespace WJD.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(GravitationalForceSystem))]
    public partial struct MassGravitySystem : ISystem
    {
        private const float GC = 0.006673f;  // Gravitational Constant
        private const float FC = 0.9987f;    // Friction Coefficient
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //state.Enabled = false;
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (main, mainEntity) in
                     SystemAPI.Query<GravityAspect>().WithEntityAccess())
            {
                var finalForceV = float3.zero;
                
                foreach (var (sub, subEntity) in
                         SystemAPI.Query<GravityAspect>().WithEntityAccess())
                {
                    if (mainEntity == subEntity) continue;

                    var deltaPos = sub.Position - main.Position;

                    var deltaV = math.normalize(deltaPos);
                    
                    var force = GC * ((main.Mass * sub.Mass) / math.dot(deltaPos, deltaPos));

                    var forceV = deltaV * force;

                    finalForceV += forceV;
                }

                finalForceV *= FC;
                main.ForceAccumulator = finalForceV;
            }
        }
        
        
    }
}
