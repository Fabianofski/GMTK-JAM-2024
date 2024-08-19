using System;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference of type `F4B1.Core.Factory`. Inherits from `AtomEventReference&lt;F4B1.Core.Factory, FactoryVariable, FactoryEvent, FactoryVariableInstancer, FactoryEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class FactoryEventReference : AtomEventReference<
        F4B1.Core.Factory,
        FactoryVariable,
        FactoryEvent,
        FactoryVariableInstancer,
        FactoryEventInstancer>, IGetEvent 
    { }
}
