using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using WJD.Components;

namespace WJD.Aspects
{
    [BurstCompile]
    public readonly partial struct GravityAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRO<Mass> mass;
        private readonly RefRO<Translation> translation;
        private readonly RefRW<ForceAccumulator> forceAccumulator;

        public float Mass => mass.ValueRO.value;

        public float3 Position => translation.ValueRO.Value;

        public float3 ForceAccumulator
        {
            get => forceAccumulator.ValueRO.value;
            set => forceAccumulator.ValueRW.value = value;
        }
    }
}