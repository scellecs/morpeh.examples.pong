namespace Pong.Balls {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Globals.Variables;
    using Scellecs.Morpeh.Providers;
    using TriInspector;
    using UnityEngine;

    [Serializable]
    public struct Colorized : IComponent {
        [Required] public Renderer[] renderers;

        [Required] public GlobalVariableColor variableColor;
    }

    [AddComponentMenu("Pong/" + nameof(Colorized))]
    public sealed class ColorizedProvider : MonoProvider<Colorized> { }
}