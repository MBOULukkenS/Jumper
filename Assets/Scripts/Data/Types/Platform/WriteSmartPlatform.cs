using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartPlatform.Components {
	/// <summary>
	/// Serialised write access to a SmartPlatform.
	/// </summary>
	[AddComponentMenu("SmartData/Jumper.Entities.Platforms.Platform/Write Smart Jumper.Entities.Platforms.Platform", 1)]
	public class WriteSmartPlatform : WriteSmartBase<Jumper.Entities.Platforms.Platform, PlatformWriter> {}
}