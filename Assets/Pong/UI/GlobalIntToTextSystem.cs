namespace Pong.UI {
    using Morpeh;
    using Morpeh.Helpers;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(GlobalIntToTextSystem))]
    public sealed class GlobalIntToTextSystem : SimpleLateUpdateSystem<GlobalIntToText> {
        protected override void Process(Entity entity, ref GlobalIntToText component, in float deltaTime) {
            foreach (GlobalIntToText.IntToConvert toConvert in component.integersToConvert) {
                if (!inAwake && !toConvert.global) {
                    return;
                }

                var value = toConvert.global.Value.ToString();
                toConvert.text.text = !string.IsNullOrEmpty(toConvert.format)
                        ? string.Format(toConvert.format, value)
                        : value;
            }
        }

        public static GlobalIntToTextSystem Create() {
            return CreateInstance<GlobalIntToTextSystem>();
        }
    }
}