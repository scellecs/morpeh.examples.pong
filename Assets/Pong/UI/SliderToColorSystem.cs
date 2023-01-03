namespace Pong.UI {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Helpers;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(SliderToColorSystem))]
    public sealed class SliderToColorSystem : SimpleLateUpdateSystem<SliderToColor> {
        protected override void Process(Entity entity, ref SliderToColor component, in float deltaTime) {
            if (inAwake || component.color) {
                Color.RGBToHSV(component.color.Value, out float h, out _, out _);
                component.slider.value = h;
            } else if (component.sliderChanged) {
                component.color.Value = Color.HSVToRGB(component.slider.value, 1f, 1f);
            }
        }

        public static SliderToColorSystem Create() {
            return CreateInstance<SliderToColorSystem>();
        }
    }
}