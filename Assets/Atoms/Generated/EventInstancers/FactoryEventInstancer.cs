using UnityEngine;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Instancer of type `F4B1.Core.Factory`. Inherits from `AtomEventInstancer&lt;F4B1.Core.Factory, FactoryEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Factory Event Instancer")]
    public class FactoryEventInstancer : AtomEventInstancer<F4B1.Core.Factory, FactoryEvent> { }
}
