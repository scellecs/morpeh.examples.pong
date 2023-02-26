namespace Pong.Paddles {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Providers;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct Paddle : IComponent, IValidatableWithGameObject {
        [Required] public Rigidbody2D body;
        [Required] public BoxCollider2D collider;
        public bool xAxis;
        public bool yAxis;

        public void OnValidate(GameObject gameObject) {
            if (body == null) {
                body = gameObject.GetComponent<Rigidbody2D>();
            }

            if (collider == null) {
                collider = gameObject.GetComponent<BoxCollider2D>();
            }
        }
    }

    [AddComponentMenu("Pong/" + nameof(Paddle))]
    public sealed class PaddleProvider : MonoProvider<Paddle> { }
}