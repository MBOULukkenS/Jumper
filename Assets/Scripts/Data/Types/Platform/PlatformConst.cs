using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;
using SmartData.Interfaces;

namespace SmartData.SmartPlatform.Data {
	/// <summary>
	/// ScriptableObject constant Jumper.Entities.Platforms.Platform.
	/// </summary>
	[CreateAssetMenu(menuName="SmartData/Jumper.Entities.Platforms.Platform/Jumper.Entities.Platforms.Platform Const", order=3)]
	public class PlatformConst : SmartConst<Jumper.Entities.Platforms.Platform>, ISmartConst<Jumper.Entities.Platforms.Platform> {
		#if UNITY_EDITOR
		const string VALUETYPE = "Jumper.Entities.Platforms.Platform";
		const string DISPLAYTYPE = "Jumper.Entities.Platforms.Platform Const";
		#endif
	}
}