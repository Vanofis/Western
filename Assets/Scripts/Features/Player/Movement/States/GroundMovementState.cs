using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

namespace Rowboat.Western.Features.Player.Movement.States
{
    public class GroundMovementState : BaseMovementState
    {
        private Vector3 _currentDirection;
        
        private CompositeDisposable _disposables = new();
        
        public override void Initialize()
        {
            Core.Rigid.useGravity = true;
            Core.Rigid.linearDamping = 1f;
            
            Core.Input.Map.Ground.Enable();

            Core.Input.Map.Ground.Movement.performed += SetDirection;
            Core.Input.Map.Ground.Movement.canceled += SetDirection;

            Core.Input.Map.Ground.Jump.started += Jump;

            MovementCore.GroundDetector.IsGrounded.Subscribe(isGrounded =>
            {
                Core.Rigid.linearDamping = isGrounded ? 1f : 0.5f;
            }).AddTo(_disposables);
        }

        public override void FixedUpdate()
        {
            if (_currentDirection == Vector3.zero || !MovementCore.GroundDetector.IsGrounded.Value)
            {
                return;
            }
            
            Core.Rigid.AddForce(
                Core.Rigid.transform.TransformDirection(_currentDirection) * Stats.PlayerAcceleration, 
                ForceMode.Acceleration);
        }

        public override void Dispose()
        {
            _disposables.Dispose();
            
            Core.Input.Map.Ground.Movement.performed -= SetDirection;
            Core.Input.Map.Ground.Movement.canceled -= SetDirection;
            
            Core.Input.Map.Ground.Disable();
        }
        
        private void SetDirection(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            
            _currentDirection = new  Vector3(direction.x, 0f, direction.y);
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (MovementCore.GroundDetector.IsGrounded.Value)
            {
                Core.Rigid.AddForce(
                    Vector3.up * Mathf.Sqrt(2f * UnityEngine.Physics.gravity.magnitude * Stats.JumpHeight), 
                    ForceMode.VelocityChange);
            }
        }
    }
}