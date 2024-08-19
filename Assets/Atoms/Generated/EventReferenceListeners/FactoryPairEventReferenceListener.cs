using UnityEngine;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference Listener of type `FactoryPair`. Inherits from `AtomEventReferenceListener&lt;FactoryPair, FactoryPairEvent, FactoryPairEventReference, FactoryPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/FactoryPair Event Reference Listener")]
    public sealed class FactoryPairEventReferenceListener : AtomEventReferenceListener<
        FactoryPair,
        FactoryPairEvent,
        FactoryPairEventReference,
        FactoryPairUnityEvent>
    { }
}
