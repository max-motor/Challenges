namespace TurtleChallengeCSharp.Game.Output
{
    public interface IOutput
    {
        void Rotated(Direction direction);
        void Moved(Coordinates position);
        void HitMine();
        void Error(string message);
        void Won();
        void DidNotWin();
        void Start(int sequenceNumber);
    }
}