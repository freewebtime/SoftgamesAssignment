using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.App.UI
{
    [Serializable]
    public class UIButtonViewData : ViewData
    {
        [SerializeField]
        private string _text;

        [CreateProperty]
        public string Text { get => _text; set => _text = value; }

        [SerializeField]
        private UnityEvent _onClick;

        [CreateProperty]
        public UnityEvent OnClick { get => _onClick; set => _onClick = value; }
    }
}