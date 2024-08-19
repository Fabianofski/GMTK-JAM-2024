using System;
using UnityEngine;
using F4B1.Core;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;F4B1.Core.Factory&gt;`. Inherits from `IPair&lt;F4B1.Core.Factory&gt;`.
    /// </summary>
    [Serializable]
    public struct FactoryPair : IPair<F4B1.Core.Factory>
    {
        public F4B1.Core.Factory Item1 { get => _item1; set => _item1 = value; }
        public F4B1.Core.Factory Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private F4B1.Core.Factory _item1;
        [SerializeField]
        private F4B1.Core.Factory _item2;

        public void Deconstruct(out F4B1.Core.Factory item1, out F4B1.Core.Factory item2) { item1 = Item1; item2 = Item2; }
    }
}