namespace Pong.Balls {
    using System;
    using Morpeh;
    using Morpeh.Globals;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct Colorized : IComponent {
        [Required] public Renderer[] renderers;

        [Required] public GlobalVariableColor variableColor;
    }

    [AddComponentMenu("Pong/" + nameof(Colorized))]
    public sealed class ColorizedProvider : MonoProvider<Colorized> { }
}