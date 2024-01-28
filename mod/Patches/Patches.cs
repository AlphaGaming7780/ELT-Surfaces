using HarmonyLib;
using System.IO;
using System.Reflection;
using Game;
using System.IO.Compression;
using Game.SceneFlow;

namespace ELT_Surfaces.Patches
{

	[HarmonyPatch(typeof(GameManager), "Awake")]
	internal class GameManager_Awake
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
			ExtraLandscapingTools.CustomSurfaces.AddCustomSurfacesFolder(resources+"\\CustomSurfaces");
		}
	}
}


