namespace Pong.UI {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Globals.Variables;
    using Scellecs.Morpeh.Providers;
    using TMPro;
    using UnityEngine;

    [Serializable]
    public struct GlobalIntToText : IComponent {
        public IntToConvert[] integersToConvert;

        [Serializable]
        public struct IntToConvert {
            public GlobalVariableInt global;
            public TextMeshProUGUI text;
            public string format;
        }
    }

    [AddComponentMenu("Pong/UI/" + nameof(GlobalIntToText))]
    public sealed class GlobalIntToTextProvider : MonoProvider<GlobalIntToText> { }
}