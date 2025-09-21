using UnityEngine;
using UniRx;
using Zenject;
using Rowboat.Western.Features.Player.Core;

namespace Rowboat.Western.Features.Player.Movement
{
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField]
        private LayerMask groundLayer;
        [SerializeField] 
        private float detectionRange;
        
        public readonly ReactiveProperty<bool> IsGrounded = new();
        
        [Inject]
        private readonly PlayerCore Core;
        
        private void FixedUpdate()
        {
            Vector3 origin = Core.Legs.transform.position + Vector3.down * (Core.Legs.size.y * 0.45f);
            
            Physics.Raycast(origin, Vector3.down, out RaycastHit hit, detectionRange, groundLayer);
            
            IsGrounded.Value = hit.collider;
        }
    }
}