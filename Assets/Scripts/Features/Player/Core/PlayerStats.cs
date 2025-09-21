using UnityEngine;

namespace Rowboat.Western.Features.Player.Core
{
    public class PlayerStats : MonoBehaviour
    {
        //TODO: Replace with proprietary GAS
        [SerializeField] 
        private float playerAcceleration = 1.5f;
        [SerializeField] 
        private float jumpHeight = 15f;
        
        public float PlayerAcceleration => playerAcceleration;
        public float JumpHeight => jumpHeight;
    }
}