namespace CannonChallenge.Player
{
    /// <summary>
    /// Contract for possible players inputs
    /// </summary>
    public interface IPlayerInput
    {
        void OnMove();
         
        void OnFire();
    }
}