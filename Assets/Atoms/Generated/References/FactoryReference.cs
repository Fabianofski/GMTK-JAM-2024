using System;
using UnityAtoms.BaseAtoms;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Reference of type `F4B1.Core.Factory`. Inherits from `AtomReference&lt;F4B1.Core.Factory, FactoryPair, FactoryConstant, FactoryVariable, FactoryEvent, FactoryPairEvent, FactoryFactoryFunction, FactoryVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class FactoryReference : AtomReference<
        F4B1.Core.Factory,
        FactoryPair,
        FactoryConstant,
        FactoryVariable,
        FactoryEvent,
        FactoryPairEvent,
        FactoryFactoryFunction,
        FactoryVariableInstancer>, IEquatable<FactoryReference>
    {
        public FactoryReference() : base() { }
        public FactoryReference(F4B1.Core.Factory value) : base(value) { }
        public bool Equals(FactoryReference other) { return base.Equals(other); }
        protected override bool ValueEquals(F4B1.Core.Factory other)
        {
            throw new NotImplementedException();
        }
    }
}
