using UnityEngine;
using UnityAtoms.BaseAtoms;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Set variable value Action of type `F4B1.Core.Factory`. Inherits from `SetVariableValue&lt;F4B1.Core.Factory, FactoryPair, FactoryVariable, FactoryConstant, FactoryReference, FactoryEvent, FactoryPairEvent, FactoryVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Factory", fileName = "SetFactoryVariableValue")]
    public sealed class SetFactoryVariableValue : SetVariableValue<
        F4B1.Core.Factory,
        FactoryPair,
        FactoryVariable,
        FactoryConstant,
        FactoryReference,
        FactoryEvent,
        FactoryPairEvent,
        FactoryFactoryFunction,
        FactoryVariableInstancer>
    { }
}
