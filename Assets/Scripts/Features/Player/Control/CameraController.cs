using UnityEngine;
using UnityEngine.InputSystem;
using TriInspector;
using Zenject;
using Rowboat.Western.Features.Player.Core;

namespace Rowboat.Western.Features.Player.Control
{
    public class CameraController : MonoBehaviour
    {
        [Min(1)]
        [SerializeField]
        private float sensitivity = 2.5f;

        [PropertySpace, Min(0)]
        [SerializeField] 
        private float maxRotation = 75.0f;
        [Min(0)]
        [SerializeField] 
        private float minRotation = -85.0f;
        
        private float _currentYRotation;
        
        [Inject]
        private readonly PlayerCore Core;
        
        private void OnEnable()
        {
            //TODO: Move somewhere else
            Cursor.lockState = CursorLockMode.Locked;
            
            Core.Input.Map.Camera.Enable();
            
            Core.Input.Map.Camera.X.started += RotateCameraX;
            Core.Input.Map.Camera.Y.started += RotateCameraY;
        }
        
        private void OnDisable()
        {
            Core.Input.Map.Camera.X.started -= RotateCameraX;
            Core.Input.Map.Camera.Y.started -= RotateCameraY;
            
            Core.Input.Map.Camera.Disable();
        }
        
        private void RotateCameraX(InputAction.CallbackContext context)
        {
            Quaternion deltaXRotation = 
                Quaternion.Euler(new Vector3(0f, context.ReadValue<float>() * sensitivity * Time.fixedDeltaTime, 0f));
            
            Core.Rigid.MoveRotation(Core.Rigid.transform.localRotation * deltaXRotation);
        }

        private void RotateCameraY(InputAction.CallbackContext context)
        {
            _currentYRotation -= context.ReadValue<float>() * sensitivity * Time.deltaTime;
            _currentYRotation = Mathf.Clamp(_currentYRotation, minRotation, maxRotation);
            
            Core.MainCamera.transform.localRotation = Quaternion.Euler(new Vector3(_currentYRotation, 0f, 0f));
        }
    }
}