using UnityEngine;
using Zenject;
using Rowboat.Western.Features.Player.Movement.States;

namespace Rowboat.Western.Features.Player.Movement
{
    public class MovementStateMachine : MonoBehaviour
    {
        private BaseMovementState _currentMovementState;

        [Inject] 
        private readonly DiContainer Container;
        
        private void OnEnable()
        {
            _currentMovementState?.Initialize();
            //TODO: move somewhere else, testing purpose
            SetState<GroundMovementState>();
        }
        
        private void FixedUpdate()
        {
            _currentMovementState?.FixedUpdate();
        }
        
        private void OnDisable()
        {
            _currentMovementState?.Dispose();
        }
        
        public void SetState<T>() where T : BaseMovementState
        {
            _currentMovementState?.Dispose();
            _currentMovementState = Container.Instantiate<T>();
            _currentMovementState?.Initialize();
        }
    }
}