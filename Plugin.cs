﻿using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

#if BEPINEX_V6
	using BepInEx.Unity.Mono;
#endif

namespace ELT_Surfaces
{
	[BepInDependency("ExtraLandscapingTools")]
	[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		internal static new ManualLogSource Logger;
		private void Awake()
		{
			Logger = base.Logger;
			Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

			var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID + "_Cities2Harmony");
			var patchedMethods = harmony.GetPatchedMethods().ToArray();

			Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} made patches! Patched methods: " + patchedMethods.Length);

			foreach (var patchedMethod in patchedMethods) {
				Logger.LogInfo($"Patched method: {patchedMethod.Module.Name}:{patchedMethod.Name}");
			}
		}
	}
}
