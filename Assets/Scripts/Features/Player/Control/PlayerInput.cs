using UnityEngine;
using Rowboat.Western.Data.Input;

namespace Rowboat.Western.Features.Player.Control
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerGameplayInputMap Map { get; private set; }

        private void Awake()
        {
            Map = new();
        }
        
        private void OnEnable()
        {
            Map.Enable();
        }

        private void OnDisable()
        {
            Map.Disable();
        }
    }
}