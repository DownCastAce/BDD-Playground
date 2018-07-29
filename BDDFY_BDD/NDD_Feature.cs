using System;
using Common_BDD;
using TestStack.BDDfy;
using Xunit;
using Assert = NUnit.Framework.Assert;

// ReSharper disable InconsistentNaming

namespace BDDFY_BDD
{
	[Story(
		AsA = "As a Client",
		IWant = "To send orders at the right time",
		SoThat = "I can calculate the next delivery date for a patient"
		)]
	public class NDD_Feature
	{
		private Patient _patient;
		public void GivenIHaveAPatient()
		{
			_patient = new Patient();
		}
		public void GivenThePatientStatusIs(string status)
		{
			_patient.Status = status;
		}
		public void GivenThePatientNDDIsSetToToday()
		{
			_patient.NDD = DateTime.Today;
		}
		public void WhenIRecalculateTheNDD()
		{
			var nddCalc = new NddCalculator();
			_patient = nddCalc.Calculate(_patient);
		}

		public void GivenTheirPrescriptionIsEmpty()
		{
			_patient.Prescriptions.Clear();
		}

		public void GivenThePatientNDDIsNull()
		{
			_patient.NDD = null;
		}


		private void GivenTheirPrescriptionHasItems()
		{
			_patient.Prescriptions.Add(new Prescription
			{
				Name = "pad1",
				Frequency = 2
			});
		}

		public void GivenThePatientHasNoPreviousOrders()
		{
			_patient.Orders.Clear();
		}

		public void GivenThePatientHasPreviousOrders()
		{
			_patient.Orders.Add(new Order()
			{
				ProductId = "1",
				Date = DateTime.Parse("01/01/2018")
			});
		}

		public void ThenThePatientNDDShouldBeToday()
		{
			Assert.AreEqual(DateTime.Today, _patient.NDD, "NDD is expected to equal today");
		}


		public void ThenThePatientNDDShouldBeNull()
		{
			Assert.IsNull(_patient.NDD, "NDD expected to be null");
		}

		[Fact]
		public void PatientIsStopped()
		{
			this.Given(s => s.GivenIHaveAPatient())
				.And(s => s.GivenThePatientStatusIs("stopped"))
				.And(s => s.GivenThePatientNDDIsSetToToday(), "Given the patient NDD is set to today")
				.When(s => s.WhenIRecalculateTheNDD(), "When I recalculate the NDD")
				.Then(s => s.ThenThePatientNDDShouldBeNull(), "Then the patient NDD should be null")
				.BDDfy();

		}

		[Fact]
		public void PatientIsRemoved()
		{
			this.Given(s => s.GivenIHaveAPatient())
				.And(s => s.GivenThePatientStatusIs("removed"))
				.And(s => s.GivenThePatientNDDIsSetToToday(), "Given the patient NDD is set to today")
				.When(s => s.WhenIRecalculateTheNDD(), "When I recalculate the NDD")
				.Then(s => s.ThenThePatientNDDShouldBeNull(), "Then the patient NDD should be null")
				.BDDfy();
		}

		[Fact]
		public void PatientIsActiveWithNoPrescriptionItems()
		{
			this.Given(s => s.GivenIHaveAPatient())
				.And(s => s.GivenThePatientNDDIsSetToToday(), "Given the patient NDD is set to today")
				.And(s => s.GivenThePatientStatusIs("active"))
				.And(s => s.GivenTheirPrescriptionIsEmpty())
				.When(s => s.WhenIRecalculateTheNDD(), "When I recalculate the NDD")
				.Then(s => s.ThenThePatientNDDShouldBeNull(), "Then the patient NDD should be null")
				.BDDfy();
		}

		[Fact]
		public void PatientIsActiveWithOnePrescriptionItemAndNoOrderHistory()
		{
			this.Given(s => s.GivenIHaveAPatient())
				.And(s => s.GivenThePatientNDDIsNull(), "Given the patient NDD is null")
				.And(s => s.GivenThePatientStatusIs("active"))
				.And(s => s.GivenTheirPrescriptionHasItems())
				.And(s => s.GivenThePatientHasNoPreviousOrders())
				.When(s => s.WhenIRecalculateTheNDD(), "When I recalculate the NDD")
				.Then(s => s.ThenThePatientNDDShouldBeToday(), "Then The patient NDD should be today")
				.BDDfy();
		}

		[Fact]
		public void PatientIsActiveWithOnePrescriptionItemAndAnOrderHistory()
		{
			this.Given(s => s.GivenIHaveAPatient())
				.And(s => s.GivenThePatientNDDIsNull(), "Given the patient NDD is null")
				.And(s => s.GivenThePatientStatusIs("active"))
				.And(s => s.GivenTheirPrescriptionHasItems())
				.And(s => s.GivenThePatientHasPreviousOrders())
				.When(s => s.WhenIRecalculateTheNDD(), "When I recalculate the NDD")
				.Then(s => s.ThenThePatientNDDShouldBeToday(), "Then The patient NDD should be today")
				.BDDfy();
		}
	}
}
