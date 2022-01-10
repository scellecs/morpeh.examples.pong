namespace Pong.Balls {
    using Morpeh;
    using Morpeh.Helpers;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(ColorizedSystem))]
    public sealed class ColorizedSystem : SimpleUpdateSystem<Colorized> {
        protected override void Process(Entity entity, ref Colorized colorized, in float deltaTime) {
            foreach (Renderer renderer in colorized.renderers) {
                if (renderer is TrailRenderer trailRenderer) {
                    trailRenderer.startColor = colorized.variableColor.Value;
                } else {
                    renderer.material.color = colorized.variableColor.Value;
                }
            }
        }

        public static ColorizedSystem Create() {
            return CreateInstance<ColorizedSystem>();
        }
    }
}