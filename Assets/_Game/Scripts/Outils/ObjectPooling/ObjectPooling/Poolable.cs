using UnityEngine;
using System;

namespace Pools
{
    // [DisallowMultipleComponent]
    public class Poolable : MonoBehaviour
    {

        public IPoolable[] _poolables = Array.Empty<IPoolable>();
        public bool _isInitialized = false;

        private void Awake()
        {
            // _poolables = GetComponentsInChildren<IPoolable>(true);
            // _isInitialized = true;
        }


        public void OnReuse()
        {
            // if (!_isInitialized)
            // 	return;
            // for (int i = 0; i < _poolables.Length; i++)
            // 	_poolables[i].OnReuse();
        }


        public void OnRelease()
        {
            // for (int i = 0; i < _poolables.Length; i++)
            // 	_poolables[i].OnRelease();
        }
    }
}
