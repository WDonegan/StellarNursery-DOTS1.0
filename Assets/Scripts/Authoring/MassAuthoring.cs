using Unity.Entities;
using UnityEngine;
using WJD.Components;

namespace WJD.Authoring
{
    public class MassAuthoring : MonoBehaviour
    {

    }

    public class MassBaker : Baker<MassAuthoring>
    {
        public override void Bake(MassAuthoring authoring)
        {
            var mass = GetComponent<Rigidbody>().mass;
            AddComponent(new Mass { value = mass });
        }
    }
}
