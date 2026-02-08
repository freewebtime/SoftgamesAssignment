using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    [CreateAssetMenu(fileName = "MagicWordsConfig", menuName = "Configs/MagicWordsConfig")]
    public class MagicWordsConfig : ScriptableObject
    {
        public string EndpointUrl;

        public bool IsSimulateSlowInternetConnection;

        public float SlowInternetConnectionDelay;

        public List<EmojiReplaceConfig> EmojiReplaceConfigs;
    }

    [Serializable]
    public struct EmojiReplaceConfig
    {
        public string Pattern;
        public string Emoji;
    }
}
