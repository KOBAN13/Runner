using System;
using Configs;
using UnityEngine;
using Zenject;

namespace Character.Collisions
{
    public class BootstrapCollision : MonoBehaviour
    {
        private CollisionHandler _collisionHandler;

        [Inject]
        public void Construct(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler ?? throw new ArgumentNullException($"{nameof(_collisionHandler)} is null fix this");
        }

        public void OnEnable()
        {
            _collisionHandler.OnObstacleCollision += HandleObstacleCollision;
            _collisionHandler.OnCouponCollision += HandleCouponCollision;
        }

        public void OnDisable()
        {
            _collisionHandler.OnObstacleCollision -= HandleObstacleCollision;
            _collisionHandler.OnCouponCollision -= HandleCouponCollision;
        }

        private void HandleObstacleCollision(IAnimator playerAnimator, IUseConfigable config)
        {
            _collisionHandler.HandleObstacleCollision(playerAnimator, config);
        }

        private void HandleCouponCollision()
        {
            _collisionHandler.HandleCouponCollision();
        }
    }
}