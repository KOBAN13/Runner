using Configs;
using UnityEngine;
using Zenject;

namespace Character.Collisions
{
    public class Obstacle : MonoBehaviour
    {
        private CollisionHandler _collisionHandler;

        [Inject]
        public void Construct(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IUseConfigable>(out var config) && other.TryGetComponent<IAnimator>(out var animator)) 
                _collisionHandler.NotifyPlayerCollision(animator, config, other);
        }
    }
}