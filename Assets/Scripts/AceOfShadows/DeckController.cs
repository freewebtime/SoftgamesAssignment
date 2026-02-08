using Assets.Scripts.App;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public class DeckController : MonoBehaviour
    {
        private CardViewPool _cardViewPool;
        public CardViewPool CardViewPool
        {
            get
            {
                if (_cardViewPool == null)
                {
                    _cardViewPool = CardViewPool.Instance;
                }
                return _cardViewPool;
            }
        }

        [SerializeField]
        private PixelPerfectCamera _pixelPerfectCamera;

        [SerializeField]
        private List<CardStack> _cardStacks;

        [SerializeField]
        private List<CardView> _allCards;

        [SerializeField]
        private bool _instantiateCardsImmediately = true;

        [SerializeField]
        private AceOfShadowsConfig _config;

        [SerializeField]
        private GameOverUIScreen _gameOverScreen;

        private IEnumerator Start()
        {
            SetupCamera();
            yield return LoadingSequence();
            yield return Gameplay();
            yield return ShowResults();
        }

        private void SetupCamera()
        {
            if (_pixelPerfectCamera == null)
            {
                Debug.LogError("PixelPerfectCamera reference is missing. Please assign it in the inspector.");
                return;
            }

            var gameplayArea = CalcGameplayArea();
            _pixelPerfectCamera.FitRect(gameplayArea, GetViewportPadding());
        }

        private void OnDestroy()
        {
            DisposeCards();
        }

        private IEnumerator LoadingSequence()
        {
            var initialStack = _cardStacks != null && _cardStacks.Count > 0 ? _cardStacks[0] : null;
            if (initialStack == null)
            {
                Debug.LogError("No card stacks found for loading sequence.");
                yield break;
            }

            yield return InstantiateCards(initialStack);
        }

        private IEnumerator InstantiateCards(CardStack stack)
        {
            if (stack == null)
            {
                Debug.LogError("Card stack is null. Cannot instantiate cards.");
                yield break;
            }

            var cardsCount = GetCardsCount();
            _allCards ??= new List<CardView>();
            for (var cardIndex = _allCards.Count; cardIndex < cardsCount; cardIndex++)
            {
                CardView card = InstantiateCard(stack, cardIndex);

                _allCards.Add(card);
                stack.PushCard(card);

                if (!_instantiateCardsImmediately)
                {
                    yield return null;
                }
            }
        }

        private IEnumerator Gameplay()
        {
            var originStack = _cardStacks != null && _cardStacks.Count > 0 ? _cardStacks[0] : null;
            var targetStack = _cardStacks != null && _cardStacks.Count > 1 ? _cardStacks[1] : null;

            if (originStack == null || targetStack == null)
            {
                Debug.LogError("Not enough card stacks found for gameplay. At least 2 stacks are required.");
                yield break;
            }

            var cardMoveTime = GetCardMoveTime();

            while (originStack.TryPopCard(out CardView card))
            {
                var targetPosition = targetStack.GetNextCardPosition();
                var originalPosition = card.Transform.position;
                var distance = Vector3.Distance(card.Transform.position, targetPosition);
                var startMovingTime = Time.time;

                card.SendToTop();
                
                while (Time.time - startMovingTime < cardMoveTime)
                {
                    var progress = (Time.time - startMovingTime) / cardMoveTime;
                    card.Transform.position = Vector3.Lerp(originalPosition, targetPosition, progress);

                    yield return null;
                }

                card.ResetSortingOrder();

                targetStack.PushCard(card);
            }
        }

        private IEnumerator ShowResults()
        {
            if (_gameOverScreen != null)
            {
                _gameOverScreen.Show();
            }

            yield break;
        }

        private CardView InstantiateCard(CardStack stack, int cardIndex)
        {
            var card = CardViewPool.GetCard();
            card.Transform.SetParent(stack.Transform);

            card.CardId = cardIndex;
            card.Sprite = GetCardSprite(cardIndex);
            card.CardColor = GetCardColor(cardIndex);
            return card;
        }

        private void DisposeCards()
        {
            if (_allCards == null)
            {
                return;
            }

            var cardViewPool = CardViewPool.Instance;

            for (var cardIndex = 0; cardIndex < _allCards.Count; cardIndex++)
            {
                CardView card = _allCards[cardIndex];
                if (card != null)
                {
                    if (cardViewPool != null)
                    {
                        cardViewPool.ReleaseCard(card);
                    }
                    else
                    {
                        Destroy(card.gameObject);
                    }
                }
            }

            _allCards.Clear();
        }

        private float GetCardMoveTime()
        {
            if (_config == null)
            {
                return 1f;
            }

            return _config.CardMoveTime;
        }

        private float GetViewportPadding()
        {
            if (_config == null)
            {
                return 0f;
            }

            return _config.ViewportPadding;
        }

        private Rect CalcGameplayArea()
        {
            if (_config == null)
            {
                return default;
            }

            return _config.CalcGameplayArea();
        }

        private int GetCardsCount()
        {
            if (_config != null)
            {
                return _config.CardsCount;
            }

            return 0;
        }

        private Color GetCardColor(int cardIndex)
        {
            if (_config == null)
            {
                return Color.white;
            }

            return _config.GetCardColor(cardIndex);
        }

        private Sprite GetCardSprite(int cardIndex)
        {
            if (_config == null)
            {
                return null;
            }

            return _config.GetCardSprite(cardIndex);
        }
    }
}
