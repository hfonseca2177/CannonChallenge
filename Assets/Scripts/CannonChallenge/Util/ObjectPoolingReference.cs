using UnityEngine;

namespace CannonChallenge.Util
{
    /// <summary>
    /// Serialized reference to a object pool
    /// </summary>
    [CreateAssetMenu(fileName = "_ObjPoolReference", menuName = "CannonChallenge/Util/Obj Pool Reference", order = 0)]
    public class ObjectPoolingReference : ScriptableObject
    {
        public ObjectPooling Pool;
    }
}