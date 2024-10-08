using UnityEngine;
using UnityAtoms.BaseAtoms;
using F4B1.Core;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type F4B1.Core.Factory to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Factory Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncFactoryVariableInstancerToCollection : SyncVariableInstancerToCollection<F4B1.Core.Factory, FactoryVariable, FactoryVariableInstancer> { }
}
