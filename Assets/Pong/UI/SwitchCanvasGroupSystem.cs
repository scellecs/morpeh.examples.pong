namespace Pong.UI {
    using Morpeh;
    using Morpeh.Helpers;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(SwitchCanvasGroupSystem))]
    public sealed class SwitchCanvasGroupSystem : SimpleLateUpdateSystem<CanvasGroupSwitcher> {
        protected override void Process(Entity entity, ref CanvasGroupSwitcher switcher, in float deltaTime) {
            if (!switcher.globalEvent) {
                return;
            }

            if (switcher.currentState) {
                switcher.canvasGroup.alpha = 0f;
                switcher.canvasGroup.interactable = false;
                switcher.canvasGroup.blocksRaycasts = false;
                switcher.currentState = false;
            } else {
                switcher.canvasGroup.alpha = 1f;
                switcher.canvasGroup.interactable = true;
                switcher.canvasGroup.blocksRaycasts = true;
                switcher.currentState = true;
            }
        }

        public static SwitchCanvasGroupSystem Create() {
            return CreateInstance<SwitchCanvasGroupSystem>();
        }
    }
}