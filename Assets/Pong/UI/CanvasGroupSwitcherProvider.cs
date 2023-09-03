namespace Pong.UI {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Globals.Events;
    using Scellecs.Morpeh.Providers;
    using TriInspector;
    using UnityEngine;

    [Serializable]
    public struct CanvasGroupSwitcher : IComponent, IValidatableWithGameObject {
        [Required] public CanvasGroup canvasGroup;

        [Required] public GlobalEvent globalEvent;

        public bool currentState;

        public void OnValidate(GameObject gameObject) {
            if (canvasGroup == null) {
                canvasGroup = gameObject.GetComponent<CanvasGroup>();
            }
        }
    }

    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Pong/UI/" + nameof(CanvasGroupSwitcher))]
    public sealed class CanvasGroupSwitcherProvider : MonoProvider<CanvasGroupSwitcher> { }
}