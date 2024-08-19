using UnityEngine;
using System;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Variable of type `F4B1.Core.Factory`. Inherits from `AtomVariable&lt;F4B1.Core.Factory, FactoryPair, FactoryEvent, FactoryPairEvent, FactoryFactoryFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Factory", fileName = "FactoryVariable")]
    public sealed class FactoryVariable : AtomVariable<F4B1.Core.Factory, FactoryPair, FactoryEvent, FactoryPairEvent, FactoryFactoryFunction>
    {
        protected override bool ValueEquals(F4B1.Core.Factory other)
        {
            return false;
        }
    }
}
