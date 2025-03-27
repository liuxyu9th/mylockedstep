
using Synchronize.Game.Lockstep.MapEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace MapEditor.Editor
{
    [UnityEditor.CustomEditor(typeof(StarGalaxy))]
    class StarGalaxyEditor:UnityEditor.Editor
    {
   
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            StarGalaxy galaxy = target as StarGalaxy;
            if(GUILayout.Button("AddGamePrefab"))
            {
                galaxy.AddGamePrefab();
            }
            if(GUILayout.Button("ClearGamePrefab"))
            {
                galaxy.ClearGamePrefab();
            }
            if (GUILayout.Button("Export"))
            {
                ExportStarInfos(galaxy);
            }
            if (GUILayout.Button("Import"))
            {
                ImportMapSteam(galaxy);
            }
            if(GUILayout.Button("Clear"))
            {
                ClearElements(galaxy);
            }
            if(GUILayout.Button("Add"))
            {
                Add(galaxy);
            }
            if(GUILayout.Button("SuperAdd"))
            {
                superAdd(galaxy);
            }
        }

        private static void superAdd(StarGalaxy galaxy)
        {
            uint startId = 10000;
            StarObject start = galaxy.AddComponent<StarObject>();
            start.m_Info = new StarObjectInfo();
            start.m_Info.m_ConfigId = 10;
            start.m_Info.m_EntityId = startId;
            start.m_Info.m_Visable = true;
            start.m_Info.m_RevolutionSpeed = "0";
            start.m_Info.m_RotationSpeed = "0";
            int redius = 10;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var tem = new StarObjectInfo();
                    tem.m_ConfigId = 6;
                    tem.m_EntityId = ++startId;
                    tem.m_RevolutionRedius = redius;
                    tem.m_InitRevolutionDegree = 36 * y;
                    tem.m_ParentEntityId = start.m_Info.m_EntityId;
                    tem.m_RevolutionSpeed = "0";
                    tem.m_RotationSpeed = "0";
                    galaxy.AddComponent<StarObject>().m_Info = tem;
                }
                redius += 4;
            }
        }

        private static void Add(StarGalaxy galaxy)
        {
            var list = galaxy.GetComponents<StarObject>();

            StarObjectInfo tem;
            if (list != null && list.Length > 0)
            {
                tem = StarObjectInfo.Read(StarObjectInfo.Write(list[list.Length - 1].m_Info));
                tem.m_EntityId = tem.m_EntityId + 1;
            }
            else
            {
                tem = new StarObjectInfo();
                tem.m_EntityId = 10000;
            }

            StarObject start = galaxy.AddComponent<StarObject>();
            start.m_Info = tem;
        }

        private void ClearElements(StarGalaxy galaxy)
        {          
            var list = galaxy.GetComponents<StarObject>();
            if (list != null && list.Length > 0)
            {
                for (int i = 0; i < list.Length; ++i)
                {
                    DestroyImmediate(list[i]);
                }
            }

            var asteroids = galaxy.GetComponents<AsteroidBelt>();
            if(asteroids!=null&& asteroids.Length>0)
            {
                for (int i = 0; i < asteroids.Length; ++i)
                {
                    DestroyImmediate(asteroids[i]);
                }
            }
        }

        private void ImportMapSteam(StarGalaxy galaxy)
        {
            if (null == galaxy.m_ImportMapStream || null == galaxy.m_ImportMapStream.text)
            {
                Debug.LogError("map stream null");
                return;
            }
            ClearElements(galaxy);
            galaxy.AddImportMaySteam();
        }

        private void ExportStarInfos(StarGalaxy galaxy)
        {
            if (string.IsNullOrEmpty(galaxy.m_GalaxyName))
            {
                Debug.LogError("Set GalaxyName");
                return;
            }

            StarGalaxyInfo sgInfo = new StarGalaxyInfo();
            sgInfo.GalaxyName = galaxy.m_GalaxyName;
            sgInfo.MapWidth = galaxy.m_MapWidth;
            sgInfo.MapHeight = galaxy.m_MapHeight;
            sgInfo.Stars = new List<StarObjectInfo>();
            sgInfo.Belts = new List<AsteroidBeltInfo>();
            var stars = galaxy.GetComponents<StarObject>();
            for (int i = 0; i < stars.Length; ++i)
            {
                sgInfo.Stars.Add(stars[i].m_Info);
            }

            var asteroids = galaxy.GetComponents<AsteroidBelt>();
            for(int i=0;i<asteroids.Length;++i)
            {
                sgInfo.Belts.Add(asteroids[i].m_Info);
            }

            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Configs/Maps/" + galaxy.m_GalaxyName + ".json",LitJson.JsonMapper.ToJson(sgInfo));
            AssetDatabase.Refresh();
        }
    }
}
