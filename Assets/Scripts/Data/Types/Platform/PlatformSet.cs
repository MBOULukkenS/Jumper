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
	/// ScriptableObject data set which fires a Relay on data addition/removal.
	/// <summary>
	[CreateAssetMenu(menuName="SmartData/Jumper.Entities.Platforms.Platform/Jumper.Entities.Platforms.Platform Set", order=2)]
	public class PlatformSet : SmartSet<Jumper.Entities.Platforms.Platform>, ISmartDataSet<Jumper.Entities.Platforms.Platform> {
		#if UNITY_EDITOR
		const string VALUETYPE = "Jumper.Entities.Platforms.Platform";
		const string DISPLAYTYPE = "Jumper.Entities.Platforms.Platform Set";
		#endif
	}
}

namespace SmartData.SmartPlatform {
	/// <summary>
	/// Read-only access to PlatformSet or List<0>, with built-in UnityEvent.
	/// For write access make a PlatformSetWriter reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class PlatformSetReader : SmartSetRefBase<Jumper.Entities.Platforms.Platform, PlatformSet>, ISmartSetRefReader<Jumper.Entities.Platforms.Platform> {
		[SerializeField]
		Data.PlatformVar.PlatformEvent _onAdd;
		[SerializeField]
		Data.PlatformVar.PlatformEvent _onRemove;
		
		protected override System.Action<Jumper.Entities.Platforms.Platform, bool> GetUnityEventInvoke(){
			return (e,a)=>{
				if (a){
					_onAdd.Invoke(e);
				} else {
					_onRemove.Invoke(e);
				}
			};
		}
	}
	/// <summary>
	/// Write access to PlatformSet or List<Jumper.Entities.Platforms.Platform>, with built-in UnityEvent.
	/// For read-only access make a PlatformSetRef reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class PlatformSetWriter : SmartSetRefWriterBase<Jumper.Entities.Platforms.Platform, PlatformSet>, ISmartSetRefReader<Jumper.Entities.Platforms.Platform> {
		[SerializeField]
		Data.PlatformVar.PlatformEvent _onAdd;
		[SerializeField]
		Data.PlatformVar.PlatformEvent _onRemove;
		
		protected override System.Action<Jumper.Entities.Platforms.Platform, bool> GetUnityEventInvoke(){
			return (e,a)=>{
				if (a){
					_onAdd.Invoke(e);
				} else {
					_onRemove.Invoke(e);
				}
			};
		}
		
		protected sealed override void InvokeUnityEvent(Jumper.Entities.Platforms.Platform value, bool added){
			if (added){
				_onAdd.Invoke(value);
			} else {
				_onRemove.Invoke(value);
			}
		}
		
	}
}