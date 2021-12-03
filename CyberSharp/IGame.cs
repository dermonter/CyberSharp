namespace CyberSharp
{
    /// <summary>
    /// Allows the user to play a game.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// This method starts the game - it asks for the player name,
        /// then processes commands until the game ends.
        /// </summary>
        void Start();
    }
}
