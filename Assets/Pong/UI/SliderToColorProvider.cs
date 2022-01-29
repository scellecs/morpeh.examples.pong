namespace Pong.UI {
    using System;
    using Morpeh;
    using Morpeh.Globals;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public struct SliderToColor : IComponent, IValidatableWithGameObject {
        [Required] public Slider slider;

        [Required] public GlobalVariableColor color;

        [ReadOnly] public bool sliderChanged;

        public void OnValidate(GameObject gameObject) {
            if (slider == null) {
                slider = gameObject.GetComponent<Slider>();
            }
        }
    }

    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("Pong/UI/" + nameof(SliderToColor))]
    public sealed class SliderToColorProvider : MonoProvider<SliderToColor> {
        private void Awake() {
            GetData().slider.onValueChanged.AddListener(SliderChanged);
        }

        private void OnDestroy() {
            GetData().slider.onValueChanged.RemoveListener(SliderChanged);
        }

        private void SliderChanged(float value) {
            GetData().sliderChanged = true;
        }
    }
}