namespace Pong.Balls {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(ColorizedSystem))]
    public sealed class ColorizedSystem : LateUpdateSystem {
        private Filter filter;

        public override void OnAwake() {
            filter = World.Filter.With<Colorized>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in filter) {
                ref Colorized colorized = ref entity.GetComponent<Colorized>();
                foreach (Renderer renderer in colorized.renderers) {
                    if (renderer is TrailRenderer trailRenderer) {
                        trailRenderer.startColor = colorized.variableColor.Value;
                    } else {
                        renderer.material.color = colorized.variableColor.Value;
                    }
                }
            }
        }
    }
}