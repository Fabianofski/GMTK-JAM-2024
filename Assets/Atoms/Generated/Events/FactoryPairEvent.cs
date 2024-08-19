using UnityEngine;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event of type `FactoryPair`. Inherits from `AtomEvent&lt;FactoryPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/FactoryPair", fileName = "FactoryPairEvent")]
    public sealed class FactoryPairEvent : AtomEvent<FactoryPair>
    {
    }
}
