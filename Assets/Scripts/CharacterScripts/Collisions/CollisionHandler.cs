using System;
using System.Collections;
using Configs;
using Ui;
using Ui.UiInterface;
using UnityEngine;
using Zenject;

namespace Character.Collisions
{
    public class CollisionHandler
    {
        private CoroutineHelper _coroutineHelper;
        private IStopMovable _stopMovable;
        private ICoupon _couponCount;
        
        public event Action<IAnimator, IUseConfigable> OnObstacleCollision;
        public event Action OnCouponCollision;

        [Inject]
        public void Construct(CoroutineHelper coroutineHelper, IStopMovable stopMovable, ICoupon couponCount)
        {
            _coroutineHelper = coroutineHelper;
            _stopMovable = stopMovable;
            _couponCount = couponCount;
        }
        
        public void NotifyObstacleCollision(IAnimator player, IUseConfigable config)
        {
            OnObstacleCollision?.Invoke(player, config);
        }

        public void NotifyCouponCollision()
        {
            OnCouponCollision?.Invoke();
        }

        public void HandleObstacleCollision(IAnimator animator, IUseConfigable config)
        {
            _coroutineHelper.StartExternalCoroutine(HittingObstacle(animator, config));
        }
        
        public void HandleCouponCollision()
        {
            _couponCount.CountCoupon += 1;
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