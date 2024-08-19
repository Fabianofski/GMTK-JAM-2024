using UnityEngine;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event of type `F4B1.Core.Factory`. Inherits from `AtomEvent&lt;F4B1.Core.Factory&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Factory", fileName = "FactoryEvent")]
    public sealed class FactoryEvent : AtomEvent<F4B1.Core.Factory>
    {
    }
}
