namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public interface IPieceDestroy
    {
        bool ToBeDestroyed { get; }

        void OnPressed();
        void Update();
        void OnDestroy();
        void NotifyOfDestroy();
        void Reset();
    }
}
