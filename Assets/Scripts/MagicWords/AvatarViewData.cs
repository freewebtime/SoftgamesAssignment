using Assets.Scripts.App.UI;
using System;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    [Serializable]
    public class AvatarViewData : ViewData
    {
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
        private string _url;

        [CreateProperty]
        public string Url 
        { 
            get => _url; 
            set
            {
                if (Equals(_url, value))
                {
                    return;
                }

                _url = value;
                CommitChanges();
            }
        }

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

        [SerializeField]
        private Texture2D _texture;
        
        [CreateProperty]
        public Texture2D Texture 
        { 
            get => _texture; 
            set
            {
                if (Equals(_texture, value))
                {
                    return;
                }

                _texture = value;
                CommitChanges();
            } 
        }
    }
}
