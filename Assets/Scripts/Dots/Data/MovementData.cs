using Unity.Entities;
using Unity.Mathematics;

namespace Dots.Data
{
    [GenerateAuthoringComponent]
    public struct MovementData: IComponentData
    {
        public float3 Direction;
        public float Speed;
        public float TurnSpeed;
    }
}