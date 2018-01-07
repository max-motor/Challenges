using System.Collections.Generic;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using TurtleChallengeCSharp.Game;
using TurtleChallengeCSharp.Game.Output;

namespace TurtleChallengeCSharpTests
{
    [TestFixture]
    public class GameTests
    {
        private Mock<IOutput> Output { get; set; }

        [SetUp]
        public void Setup()
        {
            Output = new Mock<IOutput>();
        }

        [TestCase(Direction.North, Direction.East)]
        [TestCase(Direction.East, Direction.South)]
        [TestCase(Direction.South, Direction.West)]
        [TestCase(Direction.West, Direction.North)]
        public void RoteUpdatesDirection(Direction startDirection, Direction expectedDirection)
        {
            var game = new GameChallenge(Output.Object);
            var settings = new Settings
            {
                StartPosition = new Coordinates { X = 1, Y = 1 },
                StartDirection = startDirection,
                Exit = new Coordinates { X = 2, Y = 2 },
                Height = 3,
                Width = 3,
                Mines = new List<Coordinates> { new Coordinates { X = 1, Y = 2 } }
            };

            game.Play(JsonConvert.SerializeObject(settings), "[[\"Rotate\"]]");
            Output.Verify(x => x.Rotated(It.Is<Direction>(m => m == expectedDirection)), Times.Once);
        }

        [TestCase(Direction.North, 1, 2)]
        [TestCase(Direction.East, 2, 1)]
        [TestCase(Direction.South, 1, 0)]
        [TestCase(Direction.West, 0, 1)]
        public void MoveUpdatesPosition(Direction startDirection, int expectedX, int expectedY)
        {
            var game = new GameChallenge(Output.Object);
            var settings = new Settings
            {
                StartPosition = new Coordinates { X = 1, Y = 1 },
                StartDirection = startDirection,
                Exit = new Coordinates { X = 2, Y = 2 },
                Height = 3,
                Width = 3,
                Mines = new List<Coordinates> { new Coordinates { X = 1, Y = 2 } }
            };

            game.Play(JsonConvert.SerializeObject(settings), "[[\"Move\"]]");
            Output.Verify(x => x.Moved(It.Is<Coordinates>(m => m.Y == expectedY && m.X == expectedX)), Times.Once);
        }

        [Test]
        public void SuccessResult()
        {
            var game = new GameChallenge(Output.Object);
            var settings = new Settings
            {
                StartPosition = new Coordinates { X = 1, Y = 1 },
                StartDirection = Direction.North,
                Exit = new Coordinates { X = 1, Y = 2 },
                Height = 3,
                Width = 3,
                Mines = new List<Coordinates> { new Coordinates { X = 0, Y = 0 } }
            };

            game.Play(JsonConvert.SerializeObject(settings), "[[\"Move\"]]");
            Output.Verify(x => x.Won(), Times.Once);
        }

        [Test]
        public void HitMineResult()
        {
            var game = new GameChallenge(Output.Object);
            var settings = new Settings
            {
                StartPosition = new Coordinates { X = 1, Y = 1 },
                StartDirection = Direction.North,
                Exit = new Coordinates { X = 0, Y = 0 },
                Height = 3,
                Width = 3,
                Mines = new List<Coordinates> { new Coordinates { X = 1, Y = 2 } }
            };

            game.Play(JsonConvert.SerializeObject(settings), "[[\"Move\"]]");
            Output.Verify(x => x.HitMine(), Times.Once);
        }

        [Test]
        public void StillInDangerResult()
        {
            var game = new GameChallenge(Output.Object);
            var settings = new Settings
            {
                StartPosition = new Coordinates { X = 1, Y = 1 },
                StartDirection = Direction.North,
                Exit = new Coordinates { X = 0, Y = 0 },
                Height = 3,
                Width = 3,
                Mines = new List<Coordinates> { new Coordinates { X = 2, Y = 2 } }
            };

            game.Play(JsonConvert.SerializeObject(settings), "[[\"Move\"]]");
            Output.Verify(x => x.DidNotWin(), Times.Once);
        }
    }
}
