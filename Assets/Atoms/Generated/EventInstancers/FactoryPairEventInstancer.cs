using UnityEngine;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Instancer of type `FactoryPair`. Inherits from `AtomEventInstancer&lt;FactoryPair, FactoryPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/FactoryPair Event Instancer")]
    public class FactoryPairEventInstancer : AtomEventInstancer<FactoryPair, FactoryPairEvent> { }
}
