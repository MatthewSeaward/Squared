using UnityEngine;

namespace Assets.Scripts.Workers.Powerups.Interfaces
{
    public interface IPowerup
    {
        Sprite Icon { get;  }
        bool Enabled { get; }
        string Name { get; }
        string Description { get; }

        void Invoke();
        void Update(float deltaTime);
    }
}
