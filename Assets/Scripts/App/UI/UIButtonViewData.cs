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
        public string Text 
        { 
            get => _text; 
            set
            {
                if (Equals(_text, value))
                {
                    return;
                }

                _text = value;
                CommitChanges();
            } 
        }

        [SerializeField]
        private UnityEvent _onClick;

        [CreateProperty]
        public UnityEvent OnClick 
        { 
            get => _onClick; 
            set
            {
                if (Equals(_onClick, value))
                {
                    return;
                }

                _onClick = value;
                CommitChanges();
            } 
        }
    }
}