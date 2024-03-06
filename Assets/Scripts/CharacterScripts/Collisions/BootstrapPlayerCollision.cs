using System;
using Configs;
using UnityEngine;
using Zenject;

namespace Character.Collisions
{
    public class BootstrapPlayerCollision : MonoBehaviour
    {
        private CollisionHandler _collisionHandler;

        [Inject]
        public void Construct(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler ?? throw new ArgumentNullException($"{nameof(_collisionHandler)} is null fix this");
        }

        public void OnEnable()
        {
            _collisionHandler.OnPlayerCollision += HandlePlayerCollision;
        }

        public void OnDisable()
        {
            _collisionHandler.OnPlayerCollision -= HandlePlayerCollision;
        }

        private void HandlePlayerCollision(IAnimator player, IUseConfigable config)
        {
            _collisionHandler.HandlePlayerCollision(player, config);
        }
    }
}