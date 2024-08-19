using UnityEditor;
using UnityAtoms.Editor;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `F4B1.Core.Factory`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(FactoryVariable))]
    public sealed class FactoryVariableEditor : AtomVariableEditor<F4B1.Core.Factory, FactoryPair> { }
}
