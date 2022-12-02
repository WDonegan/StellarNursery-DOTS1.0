using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using WJD.Components;
using Random = Unity.Mathematics.Random;

namespace WJD.Authoring
{
    public class DustSpawnerAuthoring : MonoBehaviour
    {
        public uint seed;

        public float spawnDuration = 10;
        public int spawnRate = 1000;
        [Space]
        public GameObject pfDust;
        [Space]
        public bool useColorList = false;
        public Color[] colors;
    }

    public class DustSpawnerBaker : Baker<DustSpawnerAuthoring>
    {
        public override void Bake(DustSpawnerAuthoring authoring)
        {
            AddComponent(new DustSpawnerData
            {
                spawnRate = authoring.spawnRate,
                spawnDuration = authoring.spawnDuration,
                useColorList = authoring.useColorList,
                
                pfDust = GetEntity(authoring.pfDust),
                colors = new NativeArray<float4>(ToFloat4Array(authoring.colors), Allocator.Persistent)
            });
            AddComponent(new SpawnerRandom
            {
                value = Random.CreateFromIndex(authoring.seed)
            });
        }

        private float4[] ToFloat4Array(Color[] colors)
        {
            List<float4> asFloats = new List<float4>(colors.Length);
            foreach (var c in colors)
            {
                asFloats.Add(new float4(c.r,c.g,c.b,c.a));
            }

            return asFloats.ToArray();
        }
    }
}