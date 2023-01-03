namespace Pong.Scores {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Providers;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct HitScoreCounter : IComponent {
        [Range(0, 32)] public int scoreLayer;

        [ReadOnly] public bool hit;
    }

    [AddComponentMenu("Pong/" + nameof(HitScoreCounter))]
    public sealed class HitScoreCounterProvider : MonoProvider<HitScoreCounter> {
        private void OnCollisionEnter2D(Collision2D other) {
            ref HitScoreCounter data = ref GetData();
            data.hit = other.gameObject.layer == data.scoreLayer;
        }
    }
}