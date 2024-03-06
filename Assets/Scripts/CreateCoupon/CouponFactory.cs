using System;
using System.Threading.Tasks;
using Configs;
using Coupon;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CreateCoupon
{
    public class CouponFactory
    {
        private PoolObject<Ticket> _poolObject;
        private GameObject _coupon;
        private CouponConfig _couponConfig;

        public CouponFactory(PoolObject<Ticket> poolObject, CouponConfig couponConfig)
        {
            _couponConfig = couponConfig ? couponConfig : throw new ArgumentNullException($"{nameof(poolObject)} is null fix this");
            
            _poolObject = poolObject ?? throw new ArgumentNullException($"{nameof(poolObject)} is null fix this");
            
            InitPrefab(_couponConfig.PathLoadPrefab);
        }

        public Ticket GetCoupon(string key)
        {
            return _poolObject.GetElementInPool(key);
        }

        private async void InitPrefab(string path)
        {
            _coupon = await LoadCoupon(path);
            
            _poolObject.AddElementsInPool(_couponConfig.KeyForGetCoupon, _coupon, _couponConfig.CountCouponInScene);
        }

        private async Task<GameObject> LoadCoupon(string path)
        {
            TaskCompletionSource<GameObject> isTaskCompletion = new TaskCompletionSource<GameObject>();

            try
            {
                AsyncOperationHandle<GameObject> coupon = Addressables.LoadAssetAsync<GameObject>(path);
                await coupon.Task;

                if (coupon.Status == AsyncOperationStatus.Succeeded) isTaskCompletion.SetResult(coupon.Result);
                else isTaskCompletion.SetException(new Exception("Failed load asset"));
                
                Addressables.Release(coupon);

            }
            catch (Exception exception)
            {
                isTaskCompletion.SetException(exception);
            }

            return await isTaskCompletion.Task;
        }
    }
}