using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.SmartPlatform.Data;
using SmartData.Abstract;
using SmartData.Interfaces;
using Sigtrap.Relays;

namespace SmartData.SmartPlatform.Data {
	/// <summary>
	/// Dynamic collection of PlatformVar assets.
	/// </summary>
	[CreateAssetMenu(menuName="SmartData/Jumper.Entities.Platforms.Platform/Jumper.Entities.Platforms.Platform Multi", order=1)]
	public class PlatformMulti: SmartMulti<Jumper.Entities.Platforms.Platform, PlatformVar>, ISmartMulti<Jumper.Entities.Platforms.Platform, PlatformVar> {
		#if UNITY_EDITOR
		const string VALUETYPE = "Jumper.Entities.Platforms.Platform";
		const string DISPLAYTYPE = "Jumper.Entities.Platforms.Platform Multi";
		#endif
	}
}

namespace SmartData.SmartPlatform {
	/// <summary>
	/// Indexed reference into a PlatformMulti (read-only access).
	/// For write access make a reference to PlatformMultiRefWriter.
	/// </summary>
	[System.Serializable]
	public class PlatformMultiReader : SmartDataMultiRef<PlatformMulti, Jumper.Entities.Platforms.Platform, PlatformVar>  {
		public static implicit operator Jumper.Entities.Platforms.Platform(PlatformMultiReader r){
            return r.value;
		}
		
		[SerializeField]
		Data.PlatformVar.PlatformEvent _onUpdate;
		
		protected override System.Action<Jumper.Entities.Platforms.Platform> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
	}
	/// <summary>
	/// Indexed reference into a PlatformMulti, with a built-in UnityEvent.
	/// For read-only access make a reference to PlatformMultiRef.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class PlatformMultiWriter : SmartDataMultiRefWriter<PlatformMulti, Jumper.Entities.Platforms.Platform, PlatformVar> {
		public static implicit operator Jumper.Entities.Platforms.Platform(PlatformMultiWriter r){
            return r.value;
		}
		
		[SerializeField]
		Data.PlatformVar.PlatformEvent _onUpdate;
		
		protected override System.Action<Jumper.Entities.Platforms.Platform> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
		protected sealed override void InvokeUnityEvent(Jumper.Entities.Platforms.Platform value){
			_onUpdate.Invoke(value);
		}
	}
}