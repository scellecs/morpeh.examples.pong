namespace Pong.UI {
    using System;
    using Morpeh;
    using Morpeh.Globals;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct CanvasGroupSwitcher : IComponent {
        [Required] public CanvasGroup canvasGroup;

        [Required] public GlobalEvent globalEvent;

        public bool currentState;
    }

    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Pong/UI/" + nameof(CanvasGroupSwitcher))]
    public sealed class CanvasGroupSwitcherProvider : MonoProvider<CanvasGroupSwitcher> {
        private void OnValidate() {
            ref CanvasGroupSwitcher data = ref GetData();
            if (data.canvasGroup == null) {
                data.canvasGroup = GetComponent<CanvasGroup>();
            }
        }
    }
}