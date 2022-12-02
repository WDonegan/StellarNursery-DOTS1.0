using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace WJD.Components
{
    public struct DustSpawnerData : IComponentData
    {
        public int spawnRate;
        public float spawnDuration;
        public bool useColorList;
        
        public Entity pfDust;
        public NativeArray<float4> colors;

    }
}