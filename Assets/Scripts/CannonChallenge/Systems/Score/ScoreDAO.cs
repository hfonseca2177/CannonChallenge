using CannonChallenge.Serialization;
using UnityEngine;

namespace CannonChallenge.Systems.Score
{
    /// <summary>
    /// Score Data Access Object - Read and Write score records
    /// </summary>
    public class ScoreDAO : MonoBehaviour
    {
        [SerializeField] private FileSerializer _fileSerializer;
        
        public void Save(LevelScore dto)
        {
            _fileSerializer.Write(dto);
        }

        public LevelScore Retrieve()
        {
            _fileSerializer.Read<LevelScore>(out var dto);
            return dto;
        }
    }
}