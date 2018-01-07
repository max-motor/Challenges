using Moq;
using NUnit.Framework;
using TurtleChallengeCSharp.Game;
using TurtleChallengeCSharp.Game.Output;

namespace TurtleChallengeCSharpTests
{
    [TestFixture]
    public class SettingsTests
    {
        private Mock<IOutput> Output { get; set; }
        private string ValidSettings = "{   \"StartPosition\": {     \"X\": 0,     \"Y\": 0   },   \"StartDirection\": \"North\",   \"Exit\": {     \"X\": 9,     \"Y\": 9   },   \"Mines\": [     {       \"X\": 5,       \"Y\": 1     },     {       \"X\": 5,       \"Y\": 3     },     {       \"X\": 5,       \"Y\": 5     },     {       \"X\": 5,       \"Y\": 7     },     {       \"X\": 5,       \"Y\": 9     }   ],   \"Width\": 10,   \"Height\": 10 }";
        private string ValidMoves = "[[\"Rotate\"]]";

        [SetUp]
        public void Setup()
        {
            Output = new Mock<IOutput>();
        }

        [Test]
        public void MalformedSettingsJsonOutputsAnError()
        {
            var game = new GameChallenge(Output.Object);
            game.Play("abc", ValidMoves);
            Output.Verify(x=>x.Error(It.Is<string>(m=> m.Equals("Could not load settings from file, please check that data is correct format"))), Times.Once);
        }

        [Test]
        public void InvalidSettingsOutputsAnError()
        {
            var game = new GameChallenge(Output.Object);
            game.Play("{}", ValidMoves);
            Output.Verify(x => x.Error(It.Is<string>(m => m.Equals("Settings contain invalid values"))), Times.Once);
        }

        [Test]
        public void MalformedMovesJsonOutputsAnError()
        {
            var game = new GameChallenge(Output.Object);
            game.Play(ValidSettings, "abc");
            Output.Verify(x => x.Error(It.Is<string>(m => m.Equals("Error loading game moves from file, please check they are in correct format"))), Times.Once);
        }

        [Test]
        public void InvalidMovesOutputsAnError()
        {
            var game = new GameChallenge(Output.Object);
            game.Play(ValidSettings, "[[],[],[]]");
            Output.Verify(x => x.Error(It.Is<string>(m => m.Equals("Error loading game moves from file, no moves defined"))), Times.Once);
        }

        [Test]
        public void ValidSettingsRunASequence()
        {
            var game = new GameChallenge(Output.Object);
            game.Play(ValidSettings, ValidMoves);
            Output.Verify(x => x.Start(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void MultipleMoveSequencesAreRun()
        {
            var game = new GameChallenge(Output.Object);
            game.Play(ValidSettings, "[[\"Rotate\"],[\"Rotate\"],[\"Rotate\"]]");
            Output.Verify(x => x.Start(It.IsAny<int>()), Times.Exactly(3));
        }
    }
}
