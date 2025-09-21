using Zenject;
using Rowboat.Western.Features.Player.Core;

namespace Rowboat.Western.Features.Player.Movement.States
{
    public abstract class BaseMovementState
    {
        [Inject] 
        protected readonly PlayerCore Core;
        [Inject]
        protected readonly PlayerMovementCore MovementCore;
        [Inject]
        protected readonly PlayerStats Stats;
        
        public virtual void Initialize() {}
        public virtual void FixedUpdate() {}
        public virtual void Dispose() {}
    }
}