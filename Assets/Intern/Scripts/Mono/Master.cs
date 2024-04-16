using UnityEngine;

namespace Client.Mono
{
    public class Master : MonoBehaviour
    {
        private static Master _instance;

        private UpdateSlave[] _updateSlaves;
        private FixedUpdateSlave[] _fixedUpdateSlaves;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            _updateSlaves = FindObjectsOfType<UpdateSlave>(true);
            _fixedUpdateSlaves = FindObjectsOfType<FixedUpdateSlave>(true);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            foreach (var s in _updateSlaves)
            {
                s.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            foreach (var s in _fixedUpdateSlaves)
            {
                s.OnFixedUpdate(deltaTime);
            }
        }
    }
}