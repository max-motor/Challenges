namespace TurtleChallengeCSharp.Game
{
    public enum PositionStatus
    {
        /// <summary>
        /// Turtle is on regular cell
        /// </summary>
        Default = 0,
        /// <summary>
        /// Turtle is on a mine
        /// </summary>
        HitMine,
        /// <summary>
        /// Turtle reached the Exit
        /// </summary>
        Won
    }
}