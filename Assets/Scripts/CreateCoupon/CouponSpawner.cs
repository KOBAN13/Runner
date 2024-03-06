using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Coupon;
using UnityEngine;
using Zenject;

namespace CreateCoupon
{
    public class CouponSpawner : MonoBehaviour
    {
        [field: Header("Spawn Points")]
        [field: SerializeField] public List<Transform> SpawnPoint { get; private set; }
        
        [field: Header("Coupon Spawn Settings")]
        [field: SerializeField] public CouponConfig CouponConfig { get; private set; }

        public List<Ticket> Coupons { get; private set; } = new();
        private CouponFactory _couponFactory;
        
        [Inject]
        public void Construct(PoolObject<Ticket> poolObject)
        {
            _couponFactory = new CouponFactory(poolObject, CouponConfig);
        }

        private void Awake()
        {
            StartCoroutine(WaitLoadCoupon());
        }

        private IEnumerator WaitLoadCoupon()
        {
            yield return new WaitForSeconds(2f);
            
            SpawnPoint
                .Select(position => MoveCouponToSpawnPoint(_couponFactory
                    .GetCoupon(CouponConfig.KeyForGetCoupon), position.position))
                .ToList()
                .ForEach(coupon => Coupons.Add(coupon));
        }

        private Ticket MoveCouponToSpawnPoint(Ticket component, Vector3 target)
        {
            component.transform.position = target;
            return component;
        }
    }
}