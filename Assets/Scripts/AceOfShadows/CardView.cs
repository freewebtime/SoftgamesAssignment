using System;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public class CardView : MonoBehaviour
    {
        public int CardId;

        public SpriteRenderer SpriteRenderer;

        public Sprite Sprite
        {
            get => SpriteRenderer.sprite;
            set => SpriteRenderer.sprite = value;
        }

        public Color CardColor
        {
            get => SpriteRenderer.color;
            set => SpriteRenderer.color = value;
        }

        [SerializeField]
        private Transform _transform;
        public Transform Transform
        {
            get
            {
                if (_transform == null)
                {
                    _transform = transform;
                }
                return _transform;
            }
        }

        public int SendOnTopSortingOrder = int.MaxValue;
        public int OriginalSortingOrder = 0;

        public void SendToTop()
        {
            if (SpriteRenderer != null)
            {
                SpriteRenderer.sortingOrder = SendOnTopSortingOrder;
            }
        }

        public void ResetSortingOrder()
        {
            if (SpriteRenderer != null)
            {
                SpriteRenderer.sortingOrder = OriginalSortingOrder;
            }
        }
    }
}
