using Assets.Scripts.App.UI;
using System;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.App.AceOfShadows
{
    public class CardsCounterUIScreen : UIScreen<CardsCounterViewData>
    {
        public void UpdateCardsCount(int count)
        {
            if (ViewData != null)
            {
                ViewData.CardsCount = count;
            }
        }
    }

    [Serializable]
    public class CardsCounterViewData : ViewData
    {
        [SerializeField]
        private int _cardsCount;

        [CreateProperty]
        public int CardsCount 
        { 
            get => _cardsCount; 
            set
            {
                if (Equals(_cardsCount, value))
                {
                    return;
                }

                _cardsCount = value;
                CommitChanges();
            } 
        }
    }
}
