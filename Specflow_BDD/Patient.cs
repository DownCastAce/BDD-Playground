using System;
using System.Collections.Generic;

namespace Specflow_BDD
{
	public class Patient
	{
		public string Status { get; set; }
		public DateTime? NDD { get; set; }
		public List<Prescription> Prescriptions { get; set; }
		public List<Order> Orders { get; internal set; }

		public Patient()
		{
			Prescriptions = new List<Prescription>();
			Orders = new List<Order>();
		}
	}
}