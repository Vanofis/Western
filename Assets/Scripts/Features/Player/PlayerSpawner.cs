using UnityEngine;
using Zenject;

namespace Rowboat.Western.Features.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;
        
        [Inject]
        private readonly DiContainer DIContainer;

        private void Awake()
        {
            DIContainer.InstantiatePrefab(playerPrefab, new GameObjectCreationParameters
            {
                Position = transform.position,
                Rotation = transform.rotation,
            });
            
            Destroy(gameObject);
        }
    }
}