using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public class CardStack : MonoBehaviour
    {
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

        [SerializeField]
        private Stack<CardView> _cards;

        [SerializeField]
        private AceOfShadowsConfig _config;

        [SerializeField]
        private CardsCounterUIScreen _cardsCounterUIScreen;

        public void PushCard(CardView card)
        {
            if (card == null)
            {
                Debug.LogError("Cannot add a null card to the stack.");
                return;
            }

            _cards ??= new Stack<CardView>();
            var cardPosition = GetNextCardPosition();

            card.Transform.position = cardPosition;
            _cards.Push(card);

            card.Transform.SetParent(Transform);

            UpdateUI();
        }

        public bool TryPopCard(out CardView card)
        {
            card = default;
            
            if (_cards != null && _cards.Count != 0)
            {
                while (_cards.TryPop(out card))
                {
                    if (card != null)
                    {
                        card.Transform.SetParent(null, true);
                        UpdateUI();
                        return true;
                    }
                }
            }

            return false;
        }

        private void UpdateUI()
        {
            if (_cardsCounterUIScreen != null)
            {
                _cardsCounterUIScreen.UpdateCardsCount(_cards != null ? _cards.Count : 0);
            }
        }

        public Vector3 GetNextCardPosition()
        {
            return GetCardPosition(_cards != null ? _cards.Count : 0);
        }

        public Vector3 GetCardPosition(int cardIndex)
        {
            var localPosition = cardIndex * GetCardOffset();
            return Transform.TransformPoint(localPosition);
        }

        private Vector3 GetCardOffset()
        {
            if (_config == null)
            {
                return default;
            }

            return _config.CardOffset;
        }
    }
}
