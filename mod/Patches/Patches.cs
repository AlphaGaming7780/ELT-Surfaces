using HarmonyLib;
using Game.SceneFlow;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Game.UI;
using Game;
using ExtraLandscapingTools;
using System.IO.Compression;
using Game.Prefabs;

namespace ELT_Surfaces.Patches
{

	[HarmonyPatch(typeof(GameSystemBase), "OnCreate")]
	internal class GameSystemBase_OnCreate
	{

		static readonly string pathToZip = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\resources.zip";

		// static private readonly string PathToParent = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName;

		static internal readonly string resources = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "resources");

		static void Prefix(GameSystemBase __instance)
		{		

			if(File.Exists(pathToZip)) {
				if(Directory.Exists(resources)) Directory.Delete(resources, true);
				ZipFile.ExtractToDirectory(pathToZip, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
				File.Delete(pathToZip);
			}
		}
	}

	[HarmonyPatch(typeof(PrefabSystem), "OnCreate")]
	public class PrefabSystem_OnCreate
	{
        public static void Prefix( PrefabSystem __instance)
		{
			// CustomSurfaces.AddCustomSurfacesFolder(GameSystemBase_OnCreate.resources+"\\CustomSurfaces");
			CustomSurfaces.CallOnCustomSurfaces += Surfaces.LoadCustomSurfaces;
		}
	}

	[HarmonyPatch(typeof(GameManager), "InitializeThumbnails")]
	internal class GameManager_InitializeThumbnails
	{	
		static readonly string IconsResourceKey = $"{MyPluginInfo.PLUGIN_NAME.ToLower()}";

		public static readonly string COUIBaseLocation = $"coui://{IconsResourceKey}";

		static void Prefix(GameManager __instance)
		{

			ELT.RegisterELTExtension(Assembly.GetExecutingAssembly().FullName, ELT.ELT_ExtensionType.Surfaces);

			List<string> pathToIconToLoad = [GameSystemBase_OnCreate.resources];

			var gameUIResourceHandler = (GameUIResourceHandler)GameManager.instance.userInterface.view.uiSystem.resourceHandler;
			
			if (gameUIResourceHandler == null)
			{
				UnityEngine.Debug.LogError("Failed retrieving GameManager's GameUIResourceHandler instance, exiting.");
				return;
			}
			
			gameUIResourceHandler.HostLocationsMap.Add(
				IconsResourceKey, pathToIconToLoad
			);
		}
	}
}
