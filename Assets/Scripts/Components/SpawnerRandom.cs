using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace WJD.Components
{
    public struct SpawnerRandom : IComponentData
    {
        public Random value;
    }
}