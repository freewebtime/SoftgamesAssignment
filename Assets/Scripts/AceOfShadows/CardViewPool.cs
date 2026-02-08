using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public class CardViewPool : MonoBehaviour
    {
        [SerializeField]
        private Stack<CardView> Cards;

        [SerializeField]
        private AceOfShadowsConfig _config;

        [SerializeField] 
        private bool IsPreloadCards;

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

        public static CardViewPool Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            if (IsPreloadCards)
            {
                var cardPrefab = GetCardPrefab();
                if (cardPrefab == null)
                {
                    Debug.LogError("CardViewPool requires a CardPrefab in the config to preload cards.");
                    return;
                }
                var cardsCount = GetMaxCardsCount();

                for (int i = 0; i < cardsCount; i++)
                {
                    CardView card = Instantiate(cardPrefab, transform);
                    ReleaseCard(card);
                }
            }
        }

        public void ReleaseCard(CardView card)
        {
            if (card == null)
            {
                return;
            }

            card.gameObject.SetActive(false);
            card.transform.SetParent(Transform);

            Cards ??= new Stack<CardView>();
            Cards.Push(card);
        }

        public CardView GetCard()
        {
            if (Cards != null)
            {
                // try to find an available card in the pool
                while (Cards.TryPop(out var card))
                {
                    if (card != null)
                    {
                        card.gameObject.SetActive(true);
                        return card;
                    }
                }
            }

            // if no available card in the pool, instantiate a new one
            var cardPrefab = GetCardPrefab();
            if (cardPrefab != null)
            {
                return Instantiate(cardPrefab, transform);
            }

            return default;
        }

        private int GetMaxCardsCount()
        {
            if (_config == null)
            {
                return 0;
            }

            return _config.CardsCount;
        }

        private CardView GetCardPrefab()
        {
            if (_config == null)
            {
                return default;
            }

            return _config.CardPrefab;
        }
    }
}
