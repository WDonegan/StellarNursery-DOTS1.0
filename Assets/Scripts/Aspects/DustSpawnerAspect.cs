using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using WJD.Components;

namespace WJD.Aspects
{
    [BurstCompile]
    public readonly partial struct DustSpawnerAspect : IAspect
    {
        private readonly TransformAspect transform;

        private readonly RefRO<DustSpawnerData> dustSpawnerData;
        private readonly RefRW<SpawnerRandom> spawnerRandom;

        private float3 SpawnPosition => transform.Position;
        public int SpawnRate => dustSpawnerData.ValueRO.spawnRate;
        public float SpawnDuration => dustSpawnerData.ValueRO.spawnDuration;

        public Entity Prefab => dustSpawnerData.ValueRO.pfDust;

        [BurstCompile]
        public UniformScaleTransform GetRandomTransform()
        {
            return new UniformScaleTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomDirection(),
                Scale = GetRandomScale()
            };
        }

        [BurstCompile]
        private float3 GetRandomPosition()
        {
            var range = new float3(1f, 1f, 1f);
            
            var randomPosition = spawnerRandom.ValueRW.value.NextFloat3(SpawnPosition - range, SpawnPosition + range);
            
            return randomPosition;
        }

        [BurstCompile]
        private quaternion GetRandomDirection()
        {
            var randomDirection = spawnerRandom.ValueRW.value.NextQuaternionRotation();

            return randomDirection;
        }

        [BurstCompile]
        private float GetRandomScale()
        {
            var randomScale = spawnerRandom.ValueRW.value.NextFloat(-0.75f, 1.25f);

            return randomScale;
        }

        [BurstCompile]
        public float4 GetColor()
        {
            return dustSpawnerData.ValueRO.useColorList ? GetColorFromList() : GetRandomColor();
        }
        
        [BurstCompile]
        private float4 GetRandomColor()
        {
            var min = new float4(0, 0, 0, 0.3f);
            var max = new float4(1, 1, 1, 0.3f);
            var randomFloat4 = spawnerRandom.ValueRW.value.NextFloat4(min, max);

            return randomFloat4;
        }

        [BurstCompile]
        private float4 GetColorFromList()
        {
            var randomI = spawnerRandom.ValueRW.value.NextInt(0, dustSpawnerData.ValueRO.colors.Length);
            return dustSpawnerData.ValueRO.colors[randomI];
        }
    }
}