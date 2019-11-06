using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartPlatform.Components {
	/// <summary>
	/// Automatically listens to a <cref>SmartPlatformSet</cref> and fires a <cref>UnityEvent<Jumper.Entities.Platforms.Platform></cref> when data changes.
	/// </summary>
	[AddComponentMenu("SmartData/Jumper.Entities.Platforms.Platform/Read Smart Jumper.Entities.Platforms.Platform Set", 2)]
	public class ReadSmartPlatformSet : ReadSmartBase<PlatformSetReader> {}
}