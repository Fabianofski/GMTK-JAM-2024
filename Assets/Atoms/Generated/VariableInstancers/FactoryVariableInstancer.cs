using UnityEngine;
using UnityAtoms.BaseAtoms;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Variable Instancer of type `F4B1.Core.Factory`. Inherits from `AtomVariableInstancer&lt;FactoryVariable, FactoryPair, F4B1.Core.Factory, FactoryEvent, FactoryPairEvent, FactoryFactoryFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Factory Variable Instancer")]
    public class FactoryVariableInstancer : AtomVariableInstancer<
        FactoryVariable,
        FactoryPair,
        F4B1.Core.Factory,
        FactoryEvent,
        FactoryPairEvent,
        FactoryFactoryFunction>
    { }
}
