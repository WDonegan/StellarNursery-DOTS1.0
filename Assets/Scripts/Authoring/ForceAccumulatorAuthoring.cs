using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using WJD.Components;

namespace WJD.Authoring
{
    public class ForceAccumulatorAuthoring : MonoBehaviour
    {
        
    }
    
    public class ForceAccumulatorBaker : Baker<ForceAccumulatorAuthoring>
    {
        public override void Bake(ForceAccumulatorAuthoring authoring)
        {
            AddComponent(new ForceAccumulator { value = float3.zero });
        }
    }
}