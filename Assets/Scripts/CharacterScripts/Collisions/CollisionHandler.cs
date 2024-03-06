using System;
using System.Collections;
using Configs;
using UnityEngine;
using Zenject;

namespace Character.Collisions
{
    public class CollisionHandler
    {
        private CoroutineHelper _coroutineHelper;
        private IStopMovable _stopMovable;
        
        public event Action<IAnimator, IUseConfigable> OnPlayerCollision;

        [Inject]
        public void Construct(CoroutineHelper coroutineHelper, IStopMovable stopMovable)
        {
            _coroutineHelper = coroutineHelper;
            _stopMovable = stopMovable;
        }
        
        public void NotifyPlayerCollision(IAnimator player, IUseConfigable config, Collider collider)
        {
            OnPlayerCollision?.Invoke(player, config);
        }

        public void HandlePlayerCollision(IAnimator animator, IUseConfigable config)
        {
            _coroutineHelper.StartExternalCoroutine(HittingObstacle(animator, config));
        }

        private IEnumerator HittingObstacle(IAnimator animator, IUseConfigable config)
        {
            animator.animatorPlayer.SetTriggerEnter();
            
            _stopMovable.OnSubcribeEvent();
            _stopMovable.InvokeEventStopMovements(config);
            yield return new WaitForSeconds(config.ConfigPlayer.RecoveryTimeAfterCollision);
            
            animator.animatorPlayer.SetTriggerExit();
            _stopMovable.Dispose();
        }
    }
}