namespace Pong.UI {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(GlobalIntToTextSystem))]
    public sealed class GlobalIntToTextSystem : LateUpdateSystem {
        private Filter filter;

        public override void OnAwake() {
            filter = World.Filter.With<GlobalIntToText>().Build();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in filter) {
                Process(ref entity.GetComponent<GlobalIntToText>());
            }
        }

        private void Process(ref GlobalIntToText intToText) {
            foreach (GlobalIntToText.IntToConvert toConvert in intToText.integersToConvert) {
                if (!intToText.hasActualValues || toConvert.global.IsPublished) {
                    var value = toConvert.global.Value.ToString();
                    toConvert.text.text = !string.IsNullOrEmpty(toConvert.format)
                            ? string.Format(toConvert.format, value)
                            : value;

                    intToText.hasActualValues = true;
                }
            }
        }
    }
}