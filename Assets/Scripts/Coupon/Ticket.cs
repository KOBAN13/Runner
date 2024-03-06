using System;
using Character.Collisions;
using UnityEngine;
using Zenject;

namespace Coupon
{
    public class Ticket : MonoBehaviour
    {
        private CollisionHandler _collisionHandler;

        [Inject]
        public void Construct(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            _collisionHandler.NotifyCouponCollision();
            
            gameObject.SetActive(false);
        }
    }
}