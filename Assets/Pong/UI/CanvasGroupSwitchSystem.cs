namespace Pong.UI {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(CanvasGroupSwitchSystem))]
    public sealed class CanvasGroupSwitchSystem : LateUpdateSystem {
        private Filter filter;

        public override void OnAwake() {
            filter = World.Filter.With<CanvasGroupSwitcher>().Build();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in filter) {
                ref CanvasGroupSwitcher switcher = ref entity.GetComponent<CanvasGroupSwitcher>();
                if (switcher.globalEvent.IsPublished) {
                    Switch(ref switcher);
                }
            }
        }

        private void Switch(ref CanvasGroupSwitcher switcher) {
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
    }
}