using HarmonyLib;
using System.IO;
using System.Reflection;
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
			CustomSurfaces.AddCustomSurfacesFolder(GameSystemBase_OnCreate.resources+"\\CustomSurfaces");
			// CustomSurfaces.CallOnCustomSurfaces += Surfaces.LoadCustomSurfaces;
		}
	}
}
