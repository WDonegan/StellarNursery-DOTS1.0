using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;
using WJD.Aspects;
using WJD.Components;

namespace WJD.Systems
    {
        [BurstCompile]
        [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
        [UpdateAfter(typeof(MassGravitySystem))]
        public partial struct GravitationalForceSystem : ISystem
        {
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
                foreach (var (fa, pv, pm, tr, rt) in SystemAPI
                             .Query<RefRW<ForceAccumulator>, RefRW<PhysicsVelocity>, RefRW<PhysicsMass>, RefRW<Translation>, RefRW<Rotation>>())
                {
                    pv.ValueRW.ApplyImpulse(pm.ValueRW, tr.ValueRW, rt.ValueRW, fa.ValueRW.value,float3.zero);
                }
            }
        }
    }
