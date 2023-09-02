namespace Pong.Scores {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Globals.Events;
    using Scellecs.Morpeh.Globals.Variables;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(ScoreSystem))]
    public sealed class ScoreSystem : UpdateSystem {
        public GlobalVariableInt currentScores;
        public GlobalVariableInt highScores;
        public GlobalEvent resetScores;
        public GlobalEvent[] decreaseScoresTriggers;

        private Filter filter;

        public override void OnAwake() {
            filter = World.Filter.With<HitScoreCounter>().Build();
            currentScores.Value = 0;
        }

        public override void OnUpdate(float deltaTime) {
            if (resetScores.IsPublished) {
                highScores.SetValue(0);
            }

            foreach (GlobalEvent trigger in decreaseScoresTriggers) {
                if (trigger.IsPublished) {
                    currentScores.Value -= trigger.BatchedChanges.Count;
                }
            }

            foreach (Entity entity in filter) {
                ref HitScoreCounter counter = ref entity.GetComponent<HitScoreCounter>();
                if (!counter.hit) {
                    continue;
                }

                currentScores.Value++;
                counter.hit = false;

                if (currentScores.Value > highScores.Value) {
                    highScores.Value = currentScores.Value;
                }
            }
        }
    }
}