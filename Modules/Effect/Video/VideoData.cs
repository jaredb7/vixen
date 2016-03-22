﻿using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Vixen.Module;
using VixenModules.App.Curves;
using VixenModules.Effect.Pixel;
using ZedGraph;

namespace VixenModules.Effect.Video
{
	[DataContract]
	public class VideoData : ModuleDataModelBase
	{

		public VideoData()
		{
			EffectType = EffectType.RenderPictureNone;
			EffectColorType = EffectColorType.RenderColor;
			ScalePercent = 0;
			ScaleToGrid = true;
			MaintainAspect = false;
			AdvancedSettings = false;
			Speed = 1;
			IncreaseBrightness = 10;
			PlayBackSpeed = 0;
			StartTime = 0;
			RotateVideo = 0;
			CustomFrameRate = true;
			Video_DataPath = string.Empty;
			FileName = String.Empty;
			Orientation = StringOrientation.Vertical;
			XOffset = 0;
			YOffset = 0;
			FramesPerSecond = 25;
			LevelCurve = new Curve(new PointPairList(new[] { 0.0, 100.0 }, new[] { 100.0, 100.0 }));
		}

		[DataMember]
		public EffectType EffectType { get; set; }

		[DataMember]
		public int IncreaseBrightness { get; set; }

		[DataMember]
		public EffectColorType EffectColorType { get; set; }

		[DataMember]
		public int Speed { get; set; }

		[DataMember]
		public int RotateVideo { get; set; }

		[DataMember]
		public int PlayBackSpeed { get; set; }

		[DataMember]
		public double StartTime { get; set; }

		[DataMember]
		public bool FitToTime { get; set; }

		[DataMember]
		public string FileName { get; set; }

		[DataMember]
		public bool ScaleToGrid { get; set; }

		[DataMember]
		public bool MaintainAspect { get; set; }

		[DataMember]
		public int ScalePercent { get; set; }

		[DataMember]
		public int XOffset { get; set; }

		[DataMember]
		public int YOffset { get; set; }

		[DataMember]
		public bool CustomFrameRate { get; set; }

		[DataMember]
		public bool AdvancedSettings { get; set; }

		[DataMember]
		public int FramesPerSecond { get; set; }

		[DataMember]
		public StringOrientation Orientation { get; set; }

		[DataMember]
		public Curve LevelCurve { get; set; }

		[DataMember]
		public int VideoLength { get; set; }

		[DataMember]
		public string Video_DataPath { get; set; }

		public override IModuleDataModel Clone()
		{
			VideoData result = new VideoData
			{
				EffectType = EffectType,
				Video_DataPath = Video_DataPath,
				FitToTime = FitToTime,
				XOffset = XOffset,
				YOffset = YOffset,
				Speed = Speed,
				IncreaseBrightness = IncreaseBrightness,
				VideoLength = VideoLength,
				RotateVideo =RotateVideo,
				AdvancedSettings = AdvancedSettings,
				CustomFrameRate = CustomFrameRate,
				PlayBackSpeed = PlayBackSpeed,
				StartTime = StartTime,
				ScalePercent = ScalePercent,
				ScaleToGrid = ScaleToGrid,
				Orientation = Orientation,
				LevelCurve = new Curve(LevelCurve),
				FileName = FileName,
				EffectColorType = EffectColorType,
				MaintainAspect = MaintainAspect,
				FramesPerSecond = FramesPerSecond
			};
			return result;
		}
	}
}
