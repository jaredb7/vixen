﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Sys;
using Vixen.Module.Effect;
using Vixen.Module.Property;
using Vixen.Commands;
using Vixen.Commands.KnownDataTypes;

// Implementing the behavior as a static class to avoid a copy for every command using this effect.

namespace TestScriptModule {
	static internal class SetLevelBehavior {
		static public ChannelData Render(ChannelNode[] nodes, TimeSpan timeSpan, Level level) {
			ChannelData channelData = new ChannelData();

			foreach(ChannelNode node in nodes) {
				// We work both with and without the property.  We have a reference to the
				// property's assembly, so we need to be tolerant of it being missing.
				IPropertyModuleInstance rgbProperty = null;
				try {
					rgbProperty = _GetRgbProperty(node);
				} catch(System.IO.FileNotFoundException) { }

				//For use as a nested, where the consumer expects a straight set-level effect
				//without regard for the RGB property.
				_RenderWithoutRgb(channelData, node, timeSpan, level);
				//if(rgbProperty != null) {
				//    _RenderWithRgb(rgbProperty, channelData, node, timeSpan, parameterValues);
				//} else {
				//    _RenderWithoutRgb(channelData, node, timeSpan, parameterValues);
				//}
			}

			return channelData;
		}

		static private TestProperty.RGB _GetRgbProperty(ChannelNode node) {
			// This method cannot even be touched if the assembly that defines the
			// TestProperty.RGB type is not loaded.  That's why this is a separate
			// method -- so an exception can be caught by the caller.
			return node.Properties.Get(SetLevelModule._rgbProperty) as TestProperty.RGB;
		}

		static private void _RenderWithoutRgb(ChannelData channelData, ChannelNode node, TimeSpan timeSpan, Level level) {
			// Render all channels the same at the same time.
			foreach(Channel channel in node.GetChannelEnumerator()) {
				CommandNode data = new CommandNode(new Lighting.Monochrome.SetLevel(level), TimeSpan.Zero, timeSpan);
				channelData[channel.Id] = new[] { data };
			}
		}

		static private void _RenderWithRgb(IPropertyModuleInstance property, ChannelData channelData, ChannelNode node, TimeSpan timeSpan, Level level) {
			TestProperty.RGB rgbProperty = property as TestProperty.RGB;
			// Split the time over the three channels in RGB order.
			Channel[] allChannels = node.GetChannelEnumerator().ToArray();
			Channel redChannel = allChannels.First(x => x.Id == rgbProperty.RedChannelId);
			Channel greenChannel = allChannels.First(x => x.Id == rgbProperty.GreenChannelId);
			Channel blueChannel = allChannels.First(x => x.Id == rgbProperty.BlueChannelId);
			Channel[] orderedChannels = {
												  redChannel,
												  greenChannel,
												  blueChannel
											  };
			for(int i = 0; i < orderedChannels.Length; i++) {
				TimeSpan startTime = TimeSpan.FromTicks(timeSpan.Ticks / 3 * i);
				TimeSpan endTime = TimeSpan.FromTicks(timeSpan.Ticks / 3 * (i + 1));
				CommandNode data = new CommandNode(new Lighting.Monochrome.SetLevel(level), startTime, endTime - startTime);
				channelData[orderedChannels[i].Id] = new[] { data };
			}
		}
	}
}
