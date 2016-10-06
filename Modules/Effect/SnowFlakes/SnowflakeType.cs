﻿using System.ComponentModel;

namespace VixenModules.Effect.Snowflakes
{
	public enum SnowflakeType
	{
		[Description("Random")]
		Random,
		[Description("1 Point")]
		Single,
		[Description("3 Point")]
		Three,
		[Description("5 Point")]
		Five,
		[Description("9 Point")]
		Nine,
		[Description("13 Point")]
		Thirteen,
		[Description("45 Point")]
		FortyFive
	}
}