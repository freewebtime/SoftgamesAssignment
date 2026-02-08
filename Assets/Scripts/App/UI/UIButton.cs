using System;
using Unity.Properties;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Assets.Scripts.App.UI
{
    [UxmlElement]
    public partial class UIButton : Button
    {
        private object _onClick;

        [CreateProperty]
        public object OnClick
        {
            get => _onClick;
            set
            {
                _onClick = value;
                NotifyPropertyChanged(nameof(OnClick));
            }
        }

        [UxmlAttribute("onClick")]
        private string _onClickPath;

        [CreateProperty]
        public string OnClickPath
        {
            get => _onClickPath;
            set
            {
                _onClickPath = value;
                NotifyPropertyChanged(nameof(OnClickPath));
            }
        }

        protected BindingId _onClickBindingId = new BindingId(nameof(OnClick));

        public UIButton()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<ClickEvent>(evt => OnClicked(evt));
        }

        protected virtual void OnAttachToPanel(AttachToPanelEvent evt)
        {
            if (!string.IsNullOrEmpty(OnClickPath))
            {
                SetBinding(_onClickBindingId, new DataBinding
                {
                    dataSourcePath = PropertyPath.FromName(OnClickPath),
                });
            }
        }

        protected virtual void OnClicked(ClickEvent evt)
        {
            var callback = OnClick;

            if (callback is Action action)
            {
                action.Invoke();
            }
            else if (callback is UnityEvent unityEvent)
            {
                unityEvent.Invoke();
            }

            evt.StopPropagation();
        }
    }
}