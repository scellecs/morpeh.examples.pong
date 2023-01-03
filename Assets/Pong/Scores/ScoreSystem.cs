namespace Pong.Scores {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Globals.Variables;
    using Scellecs.Morpeh.Helpers;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(ScoreSystem))]
    public sealed class ScoreSystem : SimpleUpdateSystem<HitScoreCounter> {
        public GlobalVariableInt currentScores;
        public GlobalVariableInt highScores;

        protected override void Process(Entity entity, ref HitScoreCounter counter, in float deltaTime) {
            if (!counter.hit) {
                return;
            }

            currentScores.Value++;
            counter.hit = false;

            if (currentScores.Value > highScores.Value) {
                highScores.Value = currentScores.Value;
            }
        }

        public static ScoreSystem Create() {
            return CreateInstance<ScoreSystem>();
        }
    }
}