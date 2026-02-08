using Assets.Scripts.App.UI;
using System;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.PhoenixFlame
{
    public class PhoenixFlameUIScreen : UIScreen<PhoenixFlameViewData>
    {
    }

    [Serializable]
    public class PhoenixFlameViewData : ViewData
    {
        [SerializeField]
        private UIButtonViewData _nextColorButton;

        [CreateProperty]
        public UIButtonViewData NextColorButton { get => _nextColorButton; set => _nextColorButton = value; }
    }
}
