using Unity.Entities;
using Unity.Mathematics;

namespace WJD.Components
{
    public struct ForceAccumulator : IComponentData
    {
        public float3 value;
    }
}
