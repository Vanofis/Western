using UnityEngine;
using Rowboat.Western.Features.Player.Control;

namespace Rowboat.Western.Features.Player.Core
{
    public class PlayerCore : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private PlayerInput playerInput;
        
        [Header("Physics")]
        [SerializeField]
        private Rigidbody rigid;
        [SerializeField]
        private SphereCollider head;
        [SerializeField]
        private CapsuleCollider body;
        [SerializeField]
        private BoxCollider legs;

        public Camera MainCamera => mainCamera;
        public PlayerInput Input => playerInput;
        
        public Rigidbody Rigid => rigid;
        public SphereCollider Head => head;
        public CapsuleCollider Body => body;
        public BoxCollider Legs => legs;
    }
}