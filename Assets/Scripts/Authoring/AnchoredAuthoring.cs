using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using WJD.Components;

namespace WJD.Authoring
{
    public class AnchoredAuthoring : MonoBehaviour
    {
        public float3 value;
    }

    public class AnchoredBaker : Baker<AnchoredAuthoring>
    {
        public override void Bake(AnchoredAuthoring authoring)
        {
            AddComponent(new Anchored { Value = authoring.value});
        }
    }
}
