using Unity.Entities;

namespace Dots.Data
{
    [GenerateAuthoringComponent]
    public struct LifeSpanData: IComponentData
    {
        public float LifeTime;
        public float SpawnTime;
    }
}