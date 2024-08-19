#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `F4B1.Core.Factory`. Inherits from `AtomDrawer&lt;FactoryValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(FactoryValueList))]
    public class FactoryValueListDrawer : AtomDrawer<FactoryValueList> { }
}
#endif
