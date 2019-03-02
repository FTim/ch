using System;
using System.Collections.Generic;
using ChClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChClientUnitTests
{
    [TestClass]
    public class StartingMaterialUnitTests
    {
        //mock-ok
        //feltételezhetjük, hogy nem null listákat kapunk adatelérési oldalról
        //(akkor se, ha 0 darab értéket kapunk DB oldalról)
        private List<SelectedMolecule> molecules = new List<SelectedMolecule>();
        

        [TestMethod]
        public void EmptyStartingMaterialTest()
        {
            StartingMaterial item = new StartingMaterial();

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedStartingMaterialTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            StartingMaterial item = new StartingMaterial(molecules[0]);

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndStringMInputStartingMaterialTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            StartingMaterial item = new StartingMaterial(molecules[0]);
            item.mValueString = "szöveges érték";
            item.VValueString = "";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndStringVInputStartingMaterialTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            StartingMaterial item = new StartingMaterial(molecules[0]);
            item.mValueString = "";
            item.VValueString = "szöveges érték";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndNumberMInputStartingMaterialTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            StartingMaterial item = new StartingMaterial(molecules[0]);
            item.mValueString = "10";
            item.VValueString = "";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();
            
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndNumberVInputStartingMaterialTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            StartingMaterial item = new StartingMaterial(molecules[0]);
            item.mValueString = "";
            item.VValueString = "2";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndBothInputGivenStartingMaterialTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            StartingMaterial item = new StartingMaterial(molecules[0]);
            item.mValueString = "10";
            item.VValueString = "32";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndNotEnoughtMStartingMaterialTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", mAvailable = 25.2 });

            StartingMaterial item = new StartingMaterial(molecules[0]);
            item.mValueString = "100";
            item.VValueString = "";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void SelectedAndNotEnoughtVStartingMaterialTest()
        {
            molecules.Add(new SelectedMolecule() { Name = "MoleculeA", CAS = "AAAAA-AA-A", Location = "Q2", VAvailable = 63 });

            StartingMaterial item = new StartingMaterial(molecules[0]);
            item.mValueString = "";
            item.VValueString = "130";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

    }
    }

