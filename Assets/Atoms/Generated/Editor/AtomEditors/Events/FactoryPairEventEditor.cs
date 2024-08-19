#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `FactoryPair`. Inherits from `AtomEventEditor&lt;FactoryPair, FactoryPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(FactoryPairEvent))]
    public sealed class FactoryPairEventEditor : AtomEventEditor<FactoryPair, FactoryPairEvent> { }
}
#endif
