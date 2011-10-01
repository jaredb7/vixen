﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Sys;
using Vixen.Module.CommandStandardExtension;
using Vixen.Commands.KnownDataTypes;

namespace Vixen.Commands {
	public class Lighting {
		static public readonly byte Value = 0;

		public class Monochrome {
			static public readonly byte Value = 0;

			public class SetLevel : Command {
				private CommandParameterSignature _signature = new CommandParameterSignature(
					new CommandParameterSpecification("Level", typeof(Level)));

				static public readonly byte Value = 0;
				static private CommandIdentifier _identifier = new CommandIdentifier(Lighting.Value, Monochrome.Value, SetLevel.Value);
				
				static public CommandIdentifier CommandIdentifier { get { return _identifier; } }

				public SetLevel() 
					: base() { 
				}

				public SetLevel(Level level)
					: this() {
					Level = level;
				}

				override public CommandIdentifier Identifier { get { return CommandIdentifier; } }

				public override CommandParameterSignature Signature {
					get { return _signature; }
				}

				public override object GetParameterValue(int index) {
					return Level;
				}

				public override void SetParameterValue(int index, object value) {
					if(value is Level) {
						Level = (Level)value;
					}
				}

				public Level Level { get; set; }

				public override Command Combine(Command other) {
					SetLevel otherSetLevelCommand = other as SetLevel;
					double level = Math.Max(Level, otherSetLevelCommand.Level);
					return new SetLevel(level);
				}

				public override Command Clone() {
					return new SetLevel(Level);
				}
			}
		}

		public class Polychrome {
			static public readonly byte Value = 1;

			public class SetColor : Command {
				private CommandParameterSignature _signature = new CommandParameterSignature(
					new CommandParameterSpecification("Color", typeof(Color)));

				static public readonly byte Value = 0;
				static private CommandIdentifier _identifier = new CommandIdentifier(Lighting.Value, Polychrome.Value, SetColor.Value);

				static public CommandIdentifier CommandIdentifier { get { return _identifier; } }

				public SetColor() { }

				public SetColor(Color color) {
					Color = color;
				}

				override public CommandIdentifier Identifier { get { return CommandIdentifier; } }

				public override CommandParameterSignature Signature {
					get { return _signature; }
				}

				public override object GetParameterValue(int index) {
					return Color;
				}

				public override void SetParameterValue(int index, object value) {
					if(value is Color) {
						Color = (Color)value;
					}
				}

				public Color Color { get; set; }

				public override Command Combine(Command other) {
					SetColor otherCommand = other as SetColor;
					int color = Math.Max(Color, otherCommand.Color);
					return new SetColor(color);
				}

				public override Command Clone() {
					return new SetColor(Color);
				}
			}
		}

		public class Custom {
			static public readonly byte Value = CustomCommandBehavior.Value;

			static private ICustomCommandBehavior _behavior = new CustomCommandBehavior();

			static public Command Get(string extensionName) {
				return _behavior.Get(Lighting.Value, extensionName);
			}
		}
	}
}
