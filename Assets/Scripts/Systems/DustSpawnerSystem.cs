using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using WJD.Aspects;
using WJD.Components;

namespace WJD.Systems
{
    [BurstCompile]
    public partial struct DustSpawnerSystem : ISystem
    {
        private float spawnTime;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DustSpawnerData>();
            spawnTime = 0;
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            var spawnerEntity = SystemAPI.GetSingletonEntity<DustSpawnerData>();
            var spawnerData = SystemAPI.GetAspectRW<DustSpawnerAspect>(spawnerEntity);
            
            for (var i = 0; i < spawnerData.SpawnRate; i++)
            {
                var dust = ecb.Instantiate(spawnerData.Prefab);
                var dustTransform = spawnerData.GetRandomTransform();
                
                ecb.SetComponent(dust, new Translation { Value = dustTransform.Position });
                ecb.SetComponent(dust, new Rotation { Value = dustTransform.Rotation });
                ecb.SetComponent(dust, new URPMaterialPropertyBaseColor { Value = spawnerData.GetColor()});
                
                spawnTime += SystemAPI.Time.DeltaTime;
            }

            if (spawnTime >= spawnerData.SpawnDuration)
                state.Enabled = false;
        }
    }
}  