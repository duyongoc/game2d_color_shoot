using UnityEngine;

namespace Pools
{
    [System.Serializable]
    class PoolContainer
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField, Min(1)] private int _startCount;

        public void Populate() => _prefab.InitPoolPrefab(_startCount);
    }

    [DefaultExecutionOrder(-9999), DisallowMultipleComponent]
    internal sealed class PoolInstaller : MonoBehaviour
    {

        [Space(12)]
        [SerializeField] private PoolContainer[] _pools = null;


        #region UNITY
        private void Awake()
        {
            Init();
        }

		// private void Start()
		// {
		// }
        #endregion


        private void Init()
        {
            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i].Populate();
            }
        }

    }
}
