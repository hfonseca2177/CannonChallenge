using Dots.Data;
using Dots.Tags;
using Unity.Burst;
using Unity.Entities;

namespace Dots.Systems
{
    /// <summary>
    /// Checks for life span time and remove the entity after it expires
    /// </summary>
    [BurstCompile]
    public partial class LifetimeSystem: SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
        
        protected override void OnCreate()
        {
            _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var ecbParallel = _commandBufferSystem.CreateCommandBuffer().AsParallelWriter();
            var dealtTime = Time.DeltaTime;
         
            Entities.WithAll<BubbleTag>().ForEach((Entity entity, int entityInQueryIndex, ref LifeSpanData lifeSpanData) =>
            {

                lifeSpanData.SpawnTime += dealtTime;

                if (lifeSpanData.SpawnTime > lifeSpanData.LifeTime)
                {
                    ecbParallel.DestroyEntity(entityInQueryIndex, entity);
                }

            }).ScheduleParallel();
            //Ends the job and allow the ecb playback
            _commandBufferSystem.AddJobHandleForProducer(this.Dependency);

        }
        
    }
}