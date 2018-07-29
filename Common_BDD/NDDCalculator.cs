using System;
using System.Linq;

namespace Common_BDD
{
	public class NddCalculator
	{
		private readonly string[] _inActiveStatuses = { "stopped", "removed" };

		public Patient Calculate(Patient patient)
		{
			if (_inActiveStatuses.Contains(patient.Status) || !patient.Prescriptions.Any())
			{
				patient.NDD = null;
				return patient;
			}
			patient.NDD = DateTime.Today;
			return patient;
		}
	}
}