using System;
using System.Collections.Generic;
using ChClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChClientUnitTests
{
    [TestClass]
    public class SolventUnitTests
    {
        //mock-ok
        //feltételezhetjük, hogy nem null listákat kapunk adatelérési oldalról
        //(akkor se, ha 0 darab értéket kapunk DB oldalról)
        private List<SelectedMolecule> molecules = new List<SelectedMolecule>();


        [TestMethod]
        public void EmptySolventTest()
        {
           Solvent item = new Solvent();

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedSolventTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", VAvailable = 25.2 });

           Solvent item = new Solvent(molecules[0]);

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndStringVInputSolventTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", VAvailable = 37 });

           Solvent item = new Solvent(molecules[0]);
            
            item.VValueString = "szöveges érték";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }


        [TestMethod]
        public void SelectedAndNumberVInputSolventTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", VAvailable = 674 });

           Solvent item = new Solvent(molecules[0]);
            item.VValueString = "21";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreEqual(0, errors.Count);
        }


        [TestMethod]
        public void SelectedAndNotEnoughtVSolventTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", VAvailable = 63 });

           Solvent item = new Solvent(molecules[0]);
            
            item.VValueString = "130";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }
    }
}
