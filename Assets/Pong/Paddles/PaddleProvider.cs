namespace Pong.Paddles {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Providers;
    using UnityEngine;

    [Serializable]
    public struct Paddle : IComponent, IValidatableWithGameObject {
        public Rigidbody2D body;
        public bool xAxis;
        public bool yAxis;

        public void OnValidate(GameObject gameObject) {
            if (body == null) {
                body = gameObject.GetComponent<Rigidbody2D>();
            }
        }
    }

    [AddComponentMenu("Pong/" + nameof(Paddle))]
    public sealed class PaddleProvider : MonoProvider<Paddle> { }
}