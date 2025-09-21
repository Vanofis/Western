using Rowboat.Western.Features.Player.Movement;
using UnityEngine;

namespace Rowboat.Western.Features.Player.Core
{
    public class PlayerMovementCore : MonoBehaviour
    {
        [SerializeField]
        private GroundDetector groundDetector;
        
        public GroundDetector GroundDetector => groundDetector;
    }
}