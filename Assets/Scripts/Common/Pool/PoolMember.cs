using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

namespace PER.Common.Pool
{
    public abstract class PoolMember<T> : MonoBehaviour where T : IPool
    {
        private IPool _pool;
        private MonoBehaviour _member;

        virtual protected void Awake()
        {
            _pool = FindObjectOfType(typeof(T)) as IPool;
        }

        public void ReturnToPool()
        {
            _pool.Add(this.gameObject);
        }


    }

}