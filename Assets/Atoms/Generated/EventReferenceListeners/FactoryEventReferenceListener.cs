using UnityEngine;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference Listener of type `F4B1.Core.Factory`. Inherits from `AtomEventReferenceListener&lt;F4B1.Core.Factory, FactoryEvent, FactoryEventReference, FactoryUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Factory Event Reference Listener")]
    public sealed class FactoryEventReferenceListener : AtomEventReferenceListener<
        F4B1.Core.Factory,
        FactoryEvent,
        FactoryEventReference,
        FactoryUnityEvent>
    { }
}
