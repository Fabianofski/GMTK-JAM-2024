#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `F4B1.Core.Factory`. Inherits from `AtomEventEditor&lt;F4B1.Core.Factory, FactoryEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(FactoryEvent))]
    public sealed class FactoryEventEditor : AtomEventEditor<F4B1.Core.Factory, FactoryEvent> { }
}
#endif
