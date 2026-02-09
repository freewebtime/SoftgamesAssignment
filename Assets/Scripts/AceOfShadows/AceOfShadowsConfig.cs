using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    [CreateAssetMenu(fileName = "AceOfShadowsConfig", menuName = "Configs/AceOfShadowsConfig", order = 0)]
    public class AceOfShadowsConfig : ScriptableObject
    {
        [Header("Card Settings")]
        
        public int CardsCount = 144;
        public List<Sprite> CardSprites;
        public List<Color> SpriteColors;
        public CardView CardPrefab;

        
        [Header("Viewport Settings")]
        
        public Vector2 CardSize = new Vector2(1.28f, 1.96f);
        public Vector3 CardOffset = new Vector3(0.1f, 0, 0.01f);
        public float PixelsPerUnit = 100f;
        public float ViewportPadding = 0.5f;

        public float DistanceBetweenStacks = 3f;
        
        public float CardMoveTime = 1f;
        
        public float PauseAfterGameOverDuration = 0f;

        public Rect CalcGameplayArea()
        {
            var size = new Vector2
            {
                x = (CardsCount - 1) * CardOffset.x + CardSize.x,
                y = DistanceBetweenStacks + CardSize.y
            };
            var position = new Vector2
            {
                x = -CardSize.x/2f,
                y = -(DistanceBetweenStacks + CardSize.y)/2f
            };
        
            return new Rect(position, size);
        }

        public Color GetCardColor(int cardIndex)
        {
            if (SpriteColors != null && SpriteColors.Count > 0)
            {
                return SpriteColors[cardIndex % SpriteColors.Count];
            }

            return Color.white;
        }

        public Sprite GetCardSprite(int cardIndex)
        {
            if (CardSprites != null && CardSprites.Count > 0)
            {
                return CardSprites[cardIndex % CardSprites.Count];
            }

            return null;
        }
    }
}
