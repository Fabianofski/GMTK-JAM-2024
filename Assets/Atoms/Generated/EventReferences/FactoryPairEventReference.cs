using System;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference of type `FactoryPair`. Inherits from `AtomEventReference&lt;FactoryPair, FactoryVariable, FactoryPairEvent, FactoryVariableInstancer, FactoryPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class FactoryPairEventReference : AtomEventReference<
        FactoryPair,
        FactoryVariable,
        FactoryPairEvent,
        FactoryVariableInstancer,
        FactoryPairEventInstancer>, IGetEvent 
    { }
}
