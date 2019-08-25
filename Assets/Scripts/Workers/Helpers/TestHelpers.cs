using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using NSubstitute;

namespace Assets.Scripts.Workers
{
    public static class TestHelpers
    {
        public static ISquarePiece CreatePiece()
        {
            return Substitute.For<ISquarePiece>();
        }
    }
}
