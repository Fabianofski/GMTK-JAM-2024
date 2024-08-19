#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `F4B1.Core.Factory`. Inherits from `AtomDrawer&lt;FactoryEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(FactoryEvent))]
    public class FactoryEventDrawer : AtomDrawer<FactoryEvent> { }
}
#endif
