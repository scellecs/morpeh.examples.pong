namespace Pong.UI {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(SliderToColorSystem))]
    public sealed class SliderToColorSystem : LateUpdateSystem {
        private Filter filter;

        public override void OnAwake() {
            filter = World.Filter.With<SliderToColor>().Build();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in filter) {
                ref SliderToColor slider = ref entity.GetComponent<SliderToColor>();
                if (!slider.hasActualValue) {
                    Color.RGBToHSV(slider.color.Value, out float h, out _, out _);
                    slider.slider.value = h;
                    slider.hasActualValue = true;
                } else if (slider.sliderChanged) {
                    slider.color.Value = Color.HSVToRGB(slider.slider.value, 1f, 1f);
                    slider.sliderChanged = false;
                }
            }
        }
    }
}