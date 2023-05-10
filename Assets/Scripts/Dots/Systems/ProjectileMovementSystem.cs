using Dots.Data;
using Dots.Tags;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Dots.Systems
{
    [UpdateAfter(typeof(LifetimeSystem))]
    [BurstCompile]
    public partial class ProjectileMovementSystem : SystemBase
    {
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.WithAll<BubbleTag>().ForEach((ref Translation translation, in Rotation rotation, in MovementData moveData) =>
        {
            translation.Value = translation.Value + (deltaTime * moveData.Speed * math.forward(rotation.Value));
        }).ScheduleParallel();
    }
    }
}