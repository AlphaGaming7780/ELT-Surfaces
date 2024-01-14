using System.IO;
using System.Reflection;
using Game.Prefabs;
using ELT_Surfaces.Patches;
using UnityEngine;
using ExtraLandscapingTools;
namespace ELT_Surfaces
{
	public class Surfaces
	{

		internal static Stream GetEmbedded(string embeddedPath) {
			return Assembly.GetExecutingAssembly().GetManifestResourceStream("ELT_Surfaces.embedded."+embeddedPath);
		}

		internal static void LoadCustomSurfaces(Material material) {
			CustomSurfaces.LoadCustomSurfaces(material, GameSystemBase_OnCreate.resources+"\\CustomSurfaces", GameManager_InitializeThumbnails.COUIBaseLocation);
		}

		internal static string GetIcon(PrefabBase prefab) {

			
			if(prefab is PathwayPrefab) {
				return prefab.name switch
				{   
					_ => "Media/Game/Icons/Pathways.svg",
				};
			} else if (prefab is TrackPrefab trackPrefab) {
				if(trackPrefab.m_TrackType == Game.Net.TrackTypes.Train) {
				return prefab.name switch
					{   
						_ => "Media/Game/Icons/DoubleTrainTrack.svg",
					};
				}
				else if(trackPrefab.m_TrackType == Game.Net.TrackTypes.Subway) {
				return prefab.name switch
					{   
						_ => "Media/Game/Icons/DoubleTrainTrack.svg",  //Media/Game/Icons/DoubleTrainTrack.svg //Media/Game/Icons/OnewayTrainTrack.svg
					};
				}
				else if(trackPrefab.m_TrackType == Game.Net.TrackTypes.Tram) {
				return prefab.name switch
					{   
						_ => "Media/Game/Icons/OnewayTramTrack.svg",
					};
				}
			} else if(prefab is UIAssetMenuPrefab) {

				return prefab.name switch
				{   
					"PlopIt" => $"{GameManager_InitializeThumbnails.COUIBaseLocation}/resources/Icons/UIAssetMenuPrefab/{prefab.name}.svg",
					_ => "Media/Game/Icons/LotTool.svg",
				};
			}

			return "Media/Game/Icons/LotTool.svg";
		}

	}
}