using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartPlatform.Components {
	/// <summary>
	/// Serialised write access to a SmartPlatformSet.
	/// </summary>
	[AddComponentMenu("SmartData/Jumper.Entities.Platforms.Platform/Write Smart Jumper.Entities.Platforms.Platform Set", 3)]
	public class WriteSmartPlatformSet : WriteSetBase<Jumper.Entities.Platforms.Platform, PlatformSetWriter> {}
}