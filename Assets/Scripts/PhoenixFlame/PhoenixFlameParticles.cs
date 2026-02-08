using Assets.Scripts.App.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PhoenixFlame
{
    public class PhoenixFlameParticles : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private string _colorIndexParameterName;

        [SerializeField]
        private List<int> _flameColorParameters;

        [SerializeField]
        private int _currentFlameColorIndex;

        public void NextColor()
        {
            if (_animator == null)
            {
                Debug.LogError("Animator component is not assigned.");
                return;
            }

            var nextColorParameter = GetNextColorParameter();
            _animator.SetInteger(_colorIndexParameterName, nextColorParameter);
        }

        private int GetNextColorParameter()
        {
            if (_flameColorParameters == null || _flameColorParameters.Count == 0)
            {
                Debug.LogError("Flame color parameters list is empty.");
                return 0; 
            }

            _currentFlameColorIndex = (_currentFlameColorIndex + 1) % _flameColorParameters.Count;
            return _flameColorParameters[_currentFlameColorIndex];
        }
    }
}
