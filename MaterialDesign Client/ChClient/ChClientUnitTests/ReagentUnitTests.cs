using System;
using System.Collections.Generic;
using ChClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChClientUnitTests
{
    [TestClass]
    public class ReagentUnitTests
    {
        //mock-ok
        //feltételezhetjük, hogy nem null listákat kapunk adatelérési oldalról
        //(akkor se, ha 0 darab értéket kapunk DB oldalról)
        private List<SelectedMolecule> molecules = new List<SelectedMolecule>();


        [TestMethod]
        public void EmptyReagentTest()
        {
            Reagent item = new Reagent();

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedReagentTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            Reagent item = new Reagent(molecules[0]);

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndStringRatioInputReagentTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            Reagent item = new Reagent(molecules[0]);
            item.RatioString = "szöveges érték";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        

        [TestMethod]
        public void SelectedAndNumberMInputReagentTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            Reagent item = new Reagent(molecules[0]);
            item.RatioString = "10";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreEqual(0, errors.Count);
        }
    

    }
}

