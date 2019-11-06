// SMARTTYPE Jumper.Entities.Platforms.Platform
// Do not move or delete the above line

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SmartData.SmartPlatform.Data;
using SmartData.Abstract;
using SmartData.Interfaces;
using Sigtrap.Relays;

namespace SmartData.SmartPlatform.Data {
	/// <summary>
	/// ScriptableObject data which fires a Relay on data change.
	/// <summary>
	[CreateAssetMenu(menuName="SmartData/Jumper.Entities.Platforms.Platform/Jumper.Entities.Platforms.Platform Variable", order=0)]
	public partial class PlatformVar : SmartVar<Jumper.Entities.Platforms.Platform>, ISmartVar<Jumper.Entities.Platforms.Platform> {	// partial to allow overrides that don't get overwritten on regeneration
		#if UNITY_EDITOR
		const string VALUETYPE = "Jumper.Entities.Platforms.Platform";
		const string DISPLAYTYPE = "Jumper.Entities.Platforms.Platform";
		#endif

		[System.Serializable]
		public class PlatformEvent : UnityEvent<Jumper.Entities.Platforms.Platform>{}
	}
}

namespace SmartData.SmartPlatform {
	/// <summary>
	/// Read-only access to SmartPlatform or Jumper.Entities.Platforms.Platform, with built-in UnityEvent.
	/// For write access make a PlatformRefWriter reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class PlatformReader : SmartDataRefBase<Jumper.Entities.Platforms.Platform, PlatformVar, PlatformConst, PlatformMulti> {
		[SerializeField]
		Data.PlatformVar.PlatformEvent _onUpdate;
		
		protected sealed override System.Action<Jumper.Entities.Platforms.Platform> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
	}
	/// <summary>
	/// Write access to SmartPlatformWriter or Jumper.Entities.Platforms.Platform, with built-in UnityEvent.
	/// For read-only access make a PlatformRef reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class PlatformWriter : SmartDataRefWriter<Jumper.Entities.Platforms.Platform, PlatformVar, PlatformConst, PlatformMulti> {
		[SerializeField]
		Data.PlatformVar.PlatformEvent _onUpdate;
		
		protected sealed override System.Action<Jumper.Entities.Platforms.Platform> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
		protected sealed override void InvokeUnityEvent(Jumper.Entities.Platforms.Platform value){
			_onUpdate.Invoke(value);
		}
	}
}