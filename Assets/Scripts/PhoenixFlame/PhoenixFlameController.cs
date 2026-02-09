using UnityEngine;

namespace Assets.Scripts.PhoenixFlame
{
    public class PhoenixFlameController : MonoBehaviour
    {
        [SerializeField]
        private PhoenixFlameConfig _config;

        [SerializeField]
        private PhoenixFlameUIScreen _uiScreen;

        [SerializeField]
        private PhoenixFlameParticles _flameParticles;

        public void NextColor()
        {
            if (_flameParticles == null)
            {
                Debug.LogError("Flame particles component is not assigned.");
                return;
            }

            _flameParticles.NextColor();
        }
    }
}
