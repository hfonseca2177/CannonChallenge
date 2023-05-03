namespace CannonChallenge.Player
{
    /// <summary>
    /// Contract for possible players inputs
    /// </summary>
    public interface IPlayerInput
    {
        void OnMoveLeft();
         
        void OnMoveRight();

        void OnMoveUp();

        void OnMoveDown();

        void OnFire();
    }
}