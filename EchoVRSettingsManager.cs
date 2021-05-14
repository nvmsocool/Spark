﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spark.Properties;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using static Logger;

namespace Spark
{
	class EchoVRSettingsManager : INotifyPropertyChanged
	{
		private static JToken settings;
		private static JToken settingsSpec;

		public event PropertyChangedEventHandler PropertyChanged;
		private static EchoVRSettingsManager instance;

		public EchoVRSettingsManager()
		{
			PropertyChanged += (o, e) =>
			{
				//RefreshProperties();
				//PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
			};
		}

		private void RefreshProperties()
		{
			if (PropertyChanged != null)
			{
				//PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
			}
		}

		public static void Reload()
		{
			if (instance == null) instance = new EchoVRSettingsManager();
			settings = ReadEchoVRSettings();
		}

		public static void ReloadSpec()
		{
			if (instance == null) instance = new EchoVRSettingsManager();
			settingsSpec = ReadEchoVRSettingsSpec();
		}

		#region settings_mp_v2
		public static JToken ReadEchoVRSettings()
		{
			try
			{
				string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "rad",
				"loneecho", "settings_mp_v2.json");
				if (!File.Exists(file))
				{
					LogRow(LogType.Error, "Can't find the EchoVR settings file");
					return null;
				}
				return JsonConvert.DeserializeObject<JToken>(File.ReadAllText(file));
			}
			catch (Exception e)
			{
				LogRow(LogType.Error, "Error when reading Arena settings.\n" + e.ToString());
			}
			return null;
		}

		public static void WriteEchoVRSettings(JToken settings)
		{
			try
			{
				string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "rad",
					"loneecho", "settings_mp_v2.json");
				if (!File.Exists(file))
				{
					throw new NullReferenceException("Can't find the EchoVR settings file");
				}

				var settingsString = JsonConvert.SerializeObject(settings, Formatting.Indented);
				File.WriteAllText(file, settingsString);
			}
			catch (Exception e)
			{
				LogRow(LogType.Error, "Error when writing Arena settings.\n" + e.ToString());
			}
		}

		public static bool GetBool(params string[] settingsPath)
		{
			if (settings == null) Reload();
			JToken localSettings = settings;

			foreach (string path in settingsPath)
			{
				if (localSettings[path] != null)
				{
					localSettings = localSettings[path];
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return false;
				}
			}
			return (bool)localSettings;
		}
		public static float GetFloat(params string[] settingsPath)
		{
			if (settings == null) Reload();
			JToken localSettings = settings;

			foreach (string path in settingsPath)
			{
				if (localSettings[path] != null)
				{
					localSettings = localSettings[path];
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return 0;
				}
			}
			return (float)localSettings;
		}
		public static int GetInt(params string[] settingsPath)
		{
			if (settings == null) Reload();
			JToken localSettings = settings;

			foreach (string path in settingsPath)
			{
				if (localSettings[path] != null)
				{
					localSettings = localSettings[path];
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return 0;
				}
			}
			return (int)localSettings;
		}
		public static void SetBool(bool value, params string[] settingsPath)
		{
			if (settings == null) Reload();
			JToken localSettings = settings;

			for (int i = 0; i < settingsPath.Length; i++)
			{
				string path = settingsPath[i];
				if (localSettings[path] != null)
				{
					// skip the last one
					if (i < settingsPath.Length - 1)
					{
						localSettings = localSettings[path];
					}
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return;
				}
			}
			localSettings[settingsPath.Last()] = value;
			WriteEchoVRSettings(settings);
		}
		public static void SetFloat(float value, params string[] settingsPath)
		{
			if (settings == null) Reload();
			JToken localSettings = settings;

			for (int i = 0; i < settingsPath.Length; i++)
			{
				string path = settingsPath[i];
				if (localSettings[path] != null)
				{
					// skip the last one
					if (i < settingsPath.Length - 1)
					{
						localSettings = localSettings[path];
					}
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return;
				}
			}
			localSettings[settingsPath.Last()] = value;
			WriteEchoVRSettings(settings);
		}
		public static void SetInt(int value, params string[] settingsPath)
		{
			if (settings == null) Reload();
			JToken localSettings = settings;

			for (int i = 0; i < settingsPath.Length; i++)
			{
				string path = settingsPath[i];
				if (localSettings[path] != null)
				{
					// skip the last one
					if (i < settingsPath.Length - 1)
					{
						localSettings = localSettings[path];
					}
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return;
				}
			}
			localSettings[settingsPath.Last()] = value;
			WriteEchoVRSettings(settings);
		}
		#endregion

		#region mp_spectator_settings.json

		public static JToken ReadEchoVRSettingsSpec()
		{
			try
			{
				string file = Path.Combine(Path.GetDirectoryName(SparkSettings.instance.echoVRPath), "..", "..", "sourcedb", "rad15", "json", "r14", "config", "mp_spectator_settings.json");
				if (!File.Exists(file))
				{
					LogRow(LogType.Error, "Can't find the EchoVR settings file");
					return null;
				}
				return JsonConvert.DeserializeObject<JToken>(File.ReadAllText(file));
			}
			catch (Exception e)
			{
				LogRow(LogType.Error, "Error when reading Arena settings.\n" + e.ToString());
			}
			return null;
		}

		public static void WriteEchoVRSettingsSpec(JToken settings)
		{
			try
			{
				string file = Path.Combine(Path.GetDirectoryName(SparkSettings.instance.echoVRPath), "..", "..", "sourcedb", "rad15", "json", "r14", "config", "mp_spectator_settings.json");
				if (!File.Exists(file))
				{
					throw new NullReferenceException("Can't find the EchoVR settings file");
				}

				var settingsString = JsonConvert.SerializeObject(settings, Formatting.Indented);
				File.WriteAllText(file, settingsString);
			}
			catch (Exception e)
			{
				LogRow(LogType.Error, "Error when writing Arena settings.\n" + e.ToString());
			}
		}

		public static bool GetBoolSpec(params string[] settingsPath)
		{
			if (settingsSpec == null) ReloadSpec();
			JToken localSettings = settingsSpec;

			foreach (string path in settingsPath)
			{
				if (localSettings[path] != null)
				{
					localSettings = localSettings[path];
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return false;
				}
			}
			return (bool)localSettings;
		}
		public static float GetFloatSpec(params string[] settingsPath)
		{
			if (settingsSpec == null) ReloadSpec();
			JToken localSettings = settingsSpec;

			foreach (string path in settingsPath)
			{
				if (localSettings[path] != null)
				{
					localSettings = localSettings[path];
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return 0;
				}
			}
			return (float)localSettings;
		}
		public static int GetIntSpec(params string[] settingsPath)
		{
			if (settingsSpec == null) ReloadSpec();
			JToken localSettings = settingsSpec;

			foreach (string path in settingsPath)
			{
				if (localSettings[path] != null)
				{
					localSettings = localSettings[path];
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return 0;
				}
			}
			return (int)localSettings;
		}
		public static void SetBoolSpec(bool value, params string[] settingsPath)
		{
			if (settingsSpec == null) ReloadSpec();
			JToken localSettings = settingsSpec;

			for (int i = 0; i < settingsPath.Length; i++)
			{
				string path = settingsPath[i];
				if (localSettings[path] != null)
				{
					// skip the last one
					if (i < settingsPath.Length - 1)
					{
						localSettings = localSettings[path];
					}
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return;
				}
			}
			localSettings[settingsPath.Last()] = value;
			WriteEchoVRSettingsSpec(settingsSpec);
		}
		public static void SetFloatSpec(float value, params string[] settingsPath)
		{
			if (settingsSpec == null) ReloadSpec();
			JToken localSettings = settingsSpec;

			for (int i = 0; i < settingsPath.Length; i++)
			{
				string path = settingsPath[i];
				if (localSettings[path] != null)
				{
					// skip the last one
					if (i < settingsPath.Length - 1)
					{
						localSettings = localSettings[path];
					}
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return;
				}
			}
			localSettings[settingsPath.Last()] = value;
			WriteEchoVRSettingsSpec(settingsSpec);
		}
		public static void SetIntSpec(int value, params string[] settingsPath)
		{
			if (settingsSpec == null) ReloadSpec();
			JToken localSettings = settingsSpec;

			for (int i = 0; i < settingsPath.Length; i++)
			{
				string path = settingsPath[i];
				if (localSettings[path] != null)
				{
					// skip the last one
					if (i < settingsPath.Length - 1)
					{
						localSettings = localSettings[path];
					}
				}
				else
				{
					LogRow(LogType.Error, "Error finding game setting");
					return;
				}
			}
			localSettings[settingsPath.Last()] = value;
			WriteEchoVRSettingsSpec(settingsSpec);
		}
		#endregion

		#region Settings 

		public static bool EnableAPIAccess {
			get => GetBool("game", "EnableAPIAccess");
			set => SetBool(value, "game", "EnableAPIAccess");
		}
		public static bool Fullscreen {
			get => GetBool(CaptureVP2String, "fullscreen");
			set => SetBool(value, CaptureVP2String, "fullscreen");
		}
		public static bool MultiResShading {
			get => GetBool(CaptureVP2String, "multires");
			set => SetBool(value, CaptureVP2String, "multires");
		}
		public static bool AutoRes {
			get => GetBool(CaptureVP2String, "adaptiveresolution");
			set => SetBool(value, CaptureVP2String, "adaptiveresolution");
		}
		public static bool TemporalAA {
			get => GetBool(CaptureVP2String, "temporalaa");
			set => SetBool(value, CaptureVP2String, "temporalaa");
		}
		public static bool Volumetrics {
			get => GetBool(CaptureVP2String, "quality", "volumetrics");
			set => SetBool(value, CaptureVP2String, "quality", "volumetrics");
		}
		public static bool Bloom {
			get => GetBool(CaptureVP2String, "quality", "bloom");
			set => SetBool(value, CaptureVP2String, "quality", "bloom");
		}
		public static int Monitor {
			get => GetInt(CaptureVP2String, "display");
			set => SetInt(value, CaptureVP2String, "display");
		}
		public static float Resolution {
			get => GetFloat(CaptureVP2String, "resolutionscale");
			set => SetFloat(value, CaptureVP2String, "resolutionscale");
		}
		public static float FoV {
			get => GetFloat(CaptureVP2String, "capturefov");
			set => SetFloat(value, CaptureVP2String, "capturefov");
		}
		public static float Sharpening {
			get => GetFloat(CaptureVP2String, "sharpening");
			set => SetFloat(value, CaptureVP2String, "sharpening");
		}
		public static int AA {
			get => GetInt(CaptureVP2String, "msaa");
			set => SetInt(value, CaptureVP2String, "msaa");
		}
		public static int ShadowQuality {
			get => GetInt(CaptureVP2String, "quality", "shadows");
			set => SetInt(value, CaptureVP2String, "quality", "shadows");
		}
		public static int MeshQuality {
			get => GetInt(CaptureVP2String, "quality", "meshes");
			set => SetInt(value, CaptureVP2String, "quality", "meshes");
		}
		public static int FXQuality {
			get => GetInt(CaptureVP2String, "quality", "fx");
			set => SetInt(value, CaptureVP2String, "quality", "fx");
		}
		public static int TextureQuality {
			get => GetInt(CaptureVP2String, "quality", "textures");
			set => SetInt(value, CaptureVP2String, "quality", "textures");
		}
		public static int LightingQuality {
			get => GetInt(CaptureVP2String, "quality", "lights");
			set => SetInt(value, CaptureVP2String, "quality", "lights");
		}



		public static string SpecFov {
			get => GetFloatSpec("fov").ToString();
			set => SetFloatSpec(float.Parse(value), "fov");
		}
		public static float SpecFovSlider {
			get => GetFloatSpec("fov");
			set => SetFloatSpec(value, "fov");
		}
		public static string POVFov {
			get => GetFloatSpec("pov_fov").ToString();
			set => SetFloatSpec(float.Parse(value), "pov_fov");
		}
		public static string FreeCamFov {
			get => GetFloatSpec("free_cam_settings", "fov").ToString();
			set => SetFloatSpec(float.Parse(value), "free_cam_settings", "fov");
		}
		public static string FollowCamOffsetX {
			get => GetFloatSpec("follow_cam_offset", "x").ToString();
			set => SetFloatSpec(float.Parse(value), "follow_cam_offset", "x");
		}
		public static string FollowCamOffsetY {
			get => GetFloatSpec("follow_cam_offset", "y").ToString();
			set => SetFloatSpec(float.Parse(value), "follow_cam_offset", "y");
		}
		public static string FollowCamOffsetZ {
			get => GetFloatSpec("follow_cam_offset", "z").ToString();
			set => SetFloatSpec(float.Parse(value), "follow_cam_offset", "z");
		}
		public static string PovSmoothing {
			get => GetFloatSpec("pov_smoothing").ToString();
			set => SetFloatSpec(float.Parse(value), "pov_smoothing");
		}


		#endregion


		private static int capturevp2;

		public static int CaptureVP2 {
			get => capturevp2;
			set {
				capturevp2 = value; instance.RefreshProperties();
			}
		}
		private static string CaptureVP2String {
			get {
				return capturevp2 switch
				{
					0 => "graphics",
					1 => "graphics_capture",
					_ => "",
				};
			}
		}
	}
}
