﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.Collisions;
using Configs;
using Coupon;
using UnityEngine;
using Zenject;

namespace CreateCoupon
{
    public class TicketSpawner : MonoBehaviour, ILimiter
    {
        [field: Header("Spawn Points")]
        [field: SerializeField] public List<Transform> SpawnPointFirst { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPointSecond { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPointThird { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPointFourth { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPointFifth { get; private set; }
        [field: SerializeField] public List<Limiter> LimiterSpawnTicket { get; private set; }
        
        [field: Header("Coupon Spawn Settings")]
        [field: SerializeField] public TicketConfig TicketConfig { get; private set; }

        private List<Ticket> _tickets = new();
        private CouponFactory _couponFactory;

        private Dictionary<Limiter, List<Transform>> _limiterZone = new();

        [Inject]
        public void Construct(PoolObject<Ticket> poolObject)
        {
            _couponFactory = new CouponFactory(poolObject, TicketConfig);
        }

        private void Awake()
        {
            StartCoroutine(WaitLoadCoupon());
            
            _limiterZone.Add(LimiterSpawnTicket[0], SpawnPointSecond);
            _limiterZone.Add(LimiterSpawnTicket[1], SpawnPointThird);
            _limiterZone.Add(LimiterSpawnTicket[2], SpawnPointFourth);
            _limiterZone.Add(LimiterSpawnTicket[3], SpawnPointFifth);
        }

        public void HandlerLimiter(Limiter limiter)
        {
            _tickets
                .ForEach(x => x.gameObject.SetActive(false));
            SpawnTicket(_limiterZone
                .SingleOrDefault(x => x.Key == limiter).Value);
        }

        private void SpawnTicket(List<Transform> spawnPoint)
        {
            spawnPoint
                .Select(position => MoveCouponToSpawnPoint(_couponFactory
                    .GetCoupon(TicketConfig.KeyForGetCoupon), position.position))
                .ToList()
                .ForEach(coupon => _tickets.Add(coupon));
        }
        
        private IEnumerator WaitLoadCoupon()
        {
            yield return new WaitForSeconds(1f);
            
            SpawnTicket(SpawnPointFirst);
        }

        private Ticket MoveCouponToSpawnPoint(Ticket component, Vector3 target)
        {
            component.transform.position = target;
            return component;
        }
    }
    
    
}