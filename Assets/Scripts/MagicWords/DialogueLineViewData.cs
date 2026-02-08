using Assets.Scripts.App.UI;
using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.MagicWords
{
    [Serializable]
    public class DialogueLineViewData : ViewData
    {
        [SerializeField]
        private AvatarViewData _avatar;

        [CreateProperty]
        public AvatarViewData Avatar 
        { 
            get => _avatar; 
            set
            {
                if (Equals(_avatar, value))
                {
                    return;
                }

                _avatar = value;
                CommitChanges();
            } 
        }

        [SerializeField]
        private string _name;

        [CreateProperty]
        public string Name 
        { 
            get => _name; 
            set
            {
                if (Equals(_name, value))
                {
                    return;
                }

                _name = value;
                CommitChanges();
            }
        }

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

        [CreateProperty]
        public FlexDirection FlexDirection => Position == AvatarPosition.Left ? FlexDirection.Row : FlexDirection.RowReverse;

        [CreateProperty]
        public float BorderRadiusLeft => Position == AvatarPosition.Left ? 0f : 10f;

        [CreateProperty]
        public float BorderRadiusRight => Position == AvatarPosition.Left ? 10f : 0f;

        [SerializeField]
        private AvatarPosition _position;

        [CreateProperty]
        public AvatarPosition Position 
        { 
            get => _position; 
            set
            {
                if (Equals(_position, value))
                {
                    return;
                }

                _position = value;
                CommitChanges();
            }
        }
    }
}
