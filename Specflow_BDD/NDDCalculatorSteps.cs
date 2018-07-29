using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

// ReSharper disable InconsistentNaming

namespace Specflow_BDD
{
    [Binding]
    public class NDDCalculatorSteps
    {
	    public Patient patient { get; set; }

        [Given(@"I have a patient")]
        public void GivenIHaveAPatient()
        {
	        patient = new Patient();
        }
        
        [Given(@"The patient status is ""(.*)""")]
        public void GivenThePatientStatusIs(string status)
        {
	        patient.Status = status;
        }
        
        [When(@"I recalculate the NDD")]
        public void WhenIRecalculateTheNDD()
        {
            var nddCalc = new NddCalculator();
	        patient = nddCalc.Calculate(patient);
        }
        
        [Then(@"The patient NDD should be null")]
        public void ThenThePatientNDDShouldBeNull()
        {
            Assert.IsNull(patient.NDD, "NDD expected to be null");
        }

	    [Given(@"The patient NDD is set to today")]
	    public void GivenThePatientNDDIsSetToToday()
	    {
		    patient.NDD = DateTime.Today;
	    }

	    [Given(@"Their prescription is empty")]
	    public void GivenTheirPrescriptionIsEmpty()
	    {
		    patient.Prescriptions.Clear();
	    }

	    [Given(@"The patient NDD is null")]
	    public void GivenThePatientNDDIsNull()
	    {
		    patient.NDD = null;
	    }

	    [Given(@"Their prescription has items")]
	    public void GivenTheirPrescriptionHasItems(Table table)
	    {
		    foreach (var tableRow in table.Rows)
		    {
			    patient.Prescriptions.Add(new Prescription
			    {
					Name = tableRow["title"],
					Frequency = int.Parse(tableRow["frequency"])
			    });
		    }
	    }

	    [Given(@"The patient has no previous orders")]
	    public void GivenThePatientHasNoPreviousOrders()
	    {
		    patient.Orders.Clear();
	    }

	    [Then(@"The patient NDD should be today")]
	    public void ThenThePatientNDDShouldBeToday()
	    {
		    Assert.AreEqual(DateTime.Today, patient.NDD, "NDD is expected to equal today");
	    }

	    [Given(@"The patient has previous orders")]
	    public void GivenThePatientHasPreviousOrders(Table table)
	    {
			foreach (var tableRow in table.Rows)
			{
				patient.Orders.Add(new Order
				{
					ProductId = tableRow["product_id"],
					Date = DateTime.Parse((tableRow["date"]))
				});
			}
		}
	}
}
