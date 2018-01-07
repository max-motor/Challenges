using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TurtleChallengeCSharp.Game.Output;

namespace TurtleChallengeCSharp.Game
{
    public class GameChallenge
    {
        public GameChallenge()
        {
            Output = new ConsoleOutput();
        }

        public GameChallenge(IOutput output)
        {
            Output = output ?? new ConsoleOutput();
        }

        private IOutput Output { get; set; }
        private Settings Settings { get; set; }
        private Coordinates CurrentPosition { get; set; }
        private Direction CurrentDirection { get; set; }
        private List<List<Move>> Moves { get; set; }

        /// <summary>
        /// Loads games settings
        /// </summary>
        /// <param name="settings">Game settings in JSON format.</param>
        public bool LoadSettings(string settings)
        {
            try
            {
                Settings = JsonConvert.DeserializeObject<Settings>(settings);
                if (!Settings.IsValid())
                {
                    Output.Error("Settings contain invalid values");
                    return false;
                }

                return true;
            }
            catch (JsonReaderException)
            {
                Output.Error("Could not load settings from file, please check that data is correct format");
                return false;
            }
        }

        /// <summary>
        /// Loads sequences of game moves
        /// </summary>
        /// <param name="moves">Game moves in JSON format. Example: [["Move","Move","Rotate","Move","Move"],["Move","Move","Move","Move","Rotate"],["Rotate","Move","Move","Move","Move"]]</param>
        public bool LoadMoves(string moves)
        {
            try
            {
                Moves = JsonConvert.DeserializeObject<List<List<Move>>>(moves);
                if(Moves == null || Moves.All(x => x.Count == 0))
                {
                    Output.Error("Error loading game moves from file, no moves defined");
                    return false;
                }
                return true;
            }
            catch (JsonReaderException)
            {
                Output.Error("Error loading game moves from file, please check they are in correct format");
                return false;
            }
        }

        /// <summary>
        /// Executes the sequesnce of moves and reports the game outcome
        /// </summary>
        public void Play(string settingsJson, string movesJson)
        {
            if (!LoadSettings(settingsJson))
                return;

            if (!LoadMoves(movesJson))
                return;

            for (var i = 0; i < Moves.Count; i++)
            {
                Output.Start(i + 1);
                // Reset start position
                CurrentPosition = new Coordinates
                {
                    X = Settings.StartPosition.X,
                    Y = Settings.StartPosition.Y
                };
                CurrentDirection = Settings.StartDirection;

                // Play sequence
                PlaySequence(Moves[i]);
            }
        }

        /// <summary>
        /// Plays the sequence of moves
        /// </summary>
        /// <param name="moves">Sequence of moves</param>
        private void PlaySequence(List<Move> moves)
        {
            var status = PositionStatus.Default;

            foreach (var move in moves)
            {
                if (move == Game.Move.Move)
                {
                    if (Move())
                    {
                        Output.Moved(CurrentPosition);
                    }
                    else
                    {
                        Output.Error("Tutrle reached the end of the board.");
                    }

                    status = CheckPositionStatus();
                    if (status != PositionStatus.Default)
                        break;
                }
                else if (move == Game.Move.Rotate)
                {
                    Rotate();
                    Output.Rotated(CurrentDirection);
                }
            }

            if (status == PositionStatus.Won)
            {
                Output.Won();
                return;
            };

            if (status == PositionStatus.HitMine)
            {
                Output.HitMine();
                return;
            }

            Output.DidNotWin();
        }

        /// <summary>
        /// Updates Turtle tirection by 90 degrees clockwise
        /// </summary>
        private void Rotate()
        {
            CurrentDirection = (Direction)(((int)CurrentDirection + 1) % 4);
        }

        /// <summary>
        /// Update Turtle's position based on current direction
        /// </summary>
        /// <returns>
        /// Returns true if the Turtle stays inside the board, false if tries to move outside of the board
        /// </returns>
        private bool Move()
        {
            int incX = 0;
            int incY = 0;
            switch (CurrentDirection)
            {
                case Direction.North:
                    incY = 1;
                    break;
                case Direction.East:
                    incX = 1;
                    break;
                case Direction.South:
                    incY = -1;
                    break;
                case Direction.West:
                    incX = -1;
                    break;
            }

            var newX = CurrentPosition.X + incX;
            // Check if move is within the game board
            if (newX < 0 || newX > Settings.Width - 1)
                return false;

            var newY = CurrentPosition.Y + incY;
            // Check if move is within the game board
            if (newY < 0 || newY > Settings.Height - 1)
                return false;

            CurrentPosition.X = newX;
            CurrentPosition.Y = newY;

            return true;
        }

        /// <summary>
        /// Verify game status
        /// </summary>
        /// <returns>Returns game status depending on current position and game settings</returns>
        private PositionStatus CheckPositionStatus()
        {
            // Reached the exit
            if (CurrentPosition.IsEqual(Settings.Exit))
            {
                return PositionStatus.Won;
            }

            // Hit a mine
            if (Settings.Mines.Any(mine => mine.IsEqual(CurrentPosition)))
            {
                return PositionStatus.HitMine;
            }

            // Ended up aon a regular cell
            return PositionStatus.Default;
        }
    }
}
