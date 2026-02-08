using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    [CreateAssetMenu(fileName = "MagicWordsConfig", menuName = "Configs/MagicWordsConfig")]
    public class MagicWordsConfig : ScriptableObject
    {
        public string EndpointUrl;

        public List<EmojiReplaceConfig> EmojiReplaceConfigs;

        private Dictionary<string, string> _emojiReplaceDictionary;
        public Dictionary<string, string> EmojiReplaceDictionary
        {
            get
            {
                if (_emojiReplaceDictionary == null || _emojiReplaceDictionary.Count <= 0)
                {
                    _emojiReplaceDictionary = new Dictionary<string, string>();
                    if (EmojiReplaceConfigs != null)
                    {
                        foreach (var config in EmojiReplaceConfigs)
                        {
                            if (!string.IsNullOrEmpty(config.Pattern))
                            {
                                _emojiReplaceDictionary[config.Pattern] = config.Emoji;
                            }
                        }
                    }
                }
                return _emojiReplaceDictionary;
            }
        }
    }

    [Serializable]
    public struct EmojiReplaceConfig
    {
        public string Pattern;
        public string Emoji;
    }
}
