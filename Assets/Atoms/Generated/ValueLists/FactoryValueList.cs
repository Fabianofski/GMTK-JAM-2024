using UnityEngine;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Value List of type `F4B1.Core.Factory`. Inherits from `AtomValueList&lt;F4B1.Core.Factory, FactoryEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Factory", fileName = "FactoryValueList")]
    public sealed class FactoryValueList : AtomValueList<F4B1.Core.Factory, FactoryEvent> { }
}
