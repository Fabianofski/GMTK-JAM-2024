#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `F4B1.Core.Factory`. Inherits from `AtomDrawer&lt;FactoryConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(FactoryConstant))]
    public class FactoryConstantDrawer : VariableDrawer<FactoryConstant> { }
}
#endif
