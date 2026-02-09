using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.App.UI
{
    static class RegisterGlobalConverters
    {
        private static bool _registered;

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod]
        public static void Register()
        {
            if (_registered)
            {
                return;
            }

            // Create a converter group.
            var group = new ConverterGroup("Percents");

            // Add converters to the converter group.
            group.AddConverter((ref float value) => value.ToString("P0"));

            // Register the converter group
            ConverterGroups.RegisterConverterGroup(group);

            _registered = true;
        }
    }
}