﻿using System.Windows.Forms;
using Vixen.Module.Property;
using Vixen.Sys;

namespace VixenModules.Property.Order {
	public class OrderModule : PropertyModuleInstanceBase {

		private OrderData _data;

		public OrderModule() {
			_data = new OrderData();
		}

		public override void SetDefaultValues() {
			if (_data == null)
			{
				_data = new OrderData();
			}
			_data.Order = 0;
		}

		public int Order
		{
			get { return _data.Order; }
			set { _data.Order = value; }
		}

		public override Vixen.Module.IModuleDataModel ModuleData {
			get {
				return _data;
			}
			set {
				_data = (OrderData)value;
			}
		}

		public static int GetOrderForElement(ElementNode element)
		{
			int p;
			OrderModule module = element.Properties.Get(OrderDescriptor.ModuleId) as OrderModule;
			if (module != null)
			{
				p = module.Order;
			}
			else
			{
				p = 0;
			}
			
			return p;
		}

		#region Overrides of PropertyModuleInstanceBase

		/// <inheritdoc />
		public override bool HasSetup => true;
		
		public override bool Setup() {
			using (SetupForm setupForm = new SetupForm(_data)) {
				if (setupForm.ShowDialog() == DialogResult.OK) {
					_data.Order = setupForm.Order;
					return true;
				}
				return false;
			}
		}

		#endregion

	}
}
