using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace MemoryProfilerWindow.ConfigTool
{
    public class ExcelToJsonConverterWindow : EditorWindow
    {
        
        public static string excel2JsonExePath;
        public static string excelDir;
        public static string jsonOutPutDir;
        
        // EditorPrefs 的键名
        private const string ExePathKey = "ExcelToJsonConverter_ExePath";
        private const string ExcelDirKey = "ExcelToJsonConverter_ExcelDir";
        private const string JsonDirKey = "ExcelToJsonConverter_JsonDir";
        // 初始化时加载保存的路径
        private void OnEnable()
        {
            excel2JsonExePath = EditorPrefs.GetString(ExePathKey, "");
            excelDir = EditorPrefs.GetString(ExcelDirKey, "");
            jsonOutPutDir = EditorPrefs.GetString(JsonDirKey, "");
        }

        private void OnDisable()
        {
            EditorPrefs.SetString(ExePathKey, excel2JsonExePath);
            EditorPrefs.SetString(ExcelDirKey, excelDir);
            EditorPrefs.SetString(JsonDirKey, jsonOutPutDir);
        }

        // 添加菜单项
        [MenuItem("Config/编辑路径")]
        public static void ShowWindow()
        {
            // 显示窗口
            GetWindow<ExcelToJsonConverterWindow>("编辑路径");
        }

        // 绘制窗口内容
        private void OnGUI()
        {
            GUILayout.Label("路径配置", EditorStyles.boldLabel);

            // 输入框和浏览按钮：excel2json.exe 路径
            GUILayout.BeginHorizontal();
            excel2JsonExePath = EditorGUILayout.TextField("excel2json.exe 路径", excel2JsonExePath);
            if (GUILayout.Button("浏览", GUILayout.Width(60)))
            {
                excel2JsonExePath = EditorUtility.OpenFilePanel("选择 excel2json.exe", "", "exe");
            }

            GUILayout.EndHorizontal();

            // 输入框和浏览按钮：Excel 文件目录
            GUILayout.BeginHorizontal();
            excelDir = EditorGUILayout.TextField("Excel 文件目录", excelDir);
            if (GUILayout.Button("浏览", GUILayout.Width(60)))
            {
                excelDir = EditorUtility.OpenFolderPanel("选择 Excel 文件目录", "", "");
            }

            GUILayout.EndHorizontal();

            // 输入框和浏览按钮：JSON 输出目录
            GUILayout.BeginHorizontal();
            jsonOutPutDir = EditorGUILayout.TextField("JSON 输出目录", jsonOutPutDir);
            if (GUILayout.Button("浏览", GUILayout.Width(60)))
            {
                jsonOutPutDir = EditorUtility.OpenFolderPanel("选择 JSON 输出目录", "", "");
            }

            GUILayout.EndHorizontal();
        }
        
        
        [MenuItem("Config/转表")]
        public static void GenConfigJson()
        {

            
            excel2JsonExePath = EditorPrefs.GetString(ExePathKey, "");
            excelDir = EditorPrefs.GetString(ExcelDirKey, "");
            jsonOutPutDir = EditorPrefs.GetString(JsonDirKey, "");
            // 检查路径是否存在
            if (!File.Exists(excel2JsonExePath))
            {
                Debug.LogError($"{excel2JsonExePath}路径不存在");
                return;
            }

            if (!Directory.Exists(excelDir))
            {
                Debug.LogError($"{excelDir}路径不存在");
                return;
            }

            if (!Directory.Exists(jsonOutPutDir))
            {
                Debug.LogError($"{jsonOutPutDir}路径不存在");
                return;
            }

            // 获取所有 .xlsx 文件
            string[] excelFiles = Directory.GetFiles(excelDir, "*.xlsx");

            foreach (string excelFile in excelFiles)
            {
                // 获取文件名（不带扩展名）
                string fileName = Path.GetFileNameWithoutExtension(excelFile);

                // 构建 JSON 文件路径
                string jsonFile = Path.Combine(jsonOutPutDir, fileName + ".json");

                // 构建参数
                string arguments = $"-n -a -e \"{excelFile}\" -j \"{jsonFile}\"";// 创建进程启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = excel2JsonExePath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.GetEncoding("GBK")
                };

                // 启动进程
                using (Process process = Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    Debug.Log(output);
                }
            }
            AssetDatabase.Refresh();

        }
    }

}