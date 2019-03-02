using System;
using System.Collections.Generic;
using ChClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChClientUnitTests
{
    [TestClass]
    public class ReactionInfoUnitTests
    {
        //mock-ok
        //feltételezhetjük, hogy nem null listákat kapunk adatelérési oldalról
        //(akkor se, ha 0 darab értéket kapunk DB oldalról)
        private List<string> usersmock = new List<string>();
        private List<string> projectsmock = new List<string>();
        private List<string> reactioncodesmock = new List<string>();


        [TestMethod]
        public void EmptyReactionTest()
        {
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();
            ReactionInfo item = new ReactionInfo();

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);

            //elvárt: üres reakcióban lesz validálásnál hiba
            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void OnlyHeadersReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ReactionImgPath = "test.jpg";

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);

            Assert.AreNotEqual(0, errors.Count);
            
        }

        [TestMethod]
        public void CompleteReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(0, errorcount);
        }

        [TestMethod]
        public void SketchReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.IsSketch = true;

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(0, errorcount);
        }

        [TestMethod]
        public void NoProductReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(1, errorcount);
        }

        [TestMethod]
        public void NoStartingMaterialReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(1, errorcount);
        }

        [TestMethod]
        public void NoReagentReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(0, errorcount);
        }

        [TestMethod]
        public void NoSolventReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(0, errorcount);
        }

        [TestMethod]
        public void NoReactionImgReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "-";
            item.Literature = "-";
            

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(1, errorcount);
        }

        [TestMethod]
        public void InvalidUsersReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser2";
            item.Chiefchemist = "TestUser3";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(2, errorcount);
        }

        [TestMethod]
        public void InvalidProjectReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject2";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(1, errorcount);
        }

        [TestMethod]
        public void InvalidPreviousStepReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");
            
            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = DateTime.Now;
            item.PreviousStep = "PreviousTestReaction";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(1, errorcount);
        }

        [TestMethod]
        public void ClosureDateEarlierReactionTest()
        {
            ReactionInfo item = new ReactionInfo();
            usersmock = new List<string>();
            projectsmock = new List<string>();
            reactioncodesmock = new List<string>();

            usersmock.Add("TestUser");
            projectsmock.Add("TestProject");

            item.Code = "NewTestCode";
            item.Chemist = "TestUser";
            item.Chiefchemist = "TestUser";
            item.Project = "TestProject";
            item.StartDate = DateTime.Now;
            item.ClosureDate = new DateTime(1996, 7, 23);
            item.PreviousStep = "-";
            item.Literature = "-";
            item.ReactionImgPath = "test.jpg";

            item.StartingMaterial = new StartingMaterial();
            item.Reagents.Add(new Reagent());
            item.Solvents.Add(new Solvent());
            item.Products.Add(new Product());

            item.Procedure = "TestProcedureText";
            item.Observation = "TestObservationText";
            item.Yield = "TestYieldText";

            item.ObservationImgPaths.Add("testobsimg.jpg");

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock, projectsmock, reactioncodesmock);
            int errorcount = 0;
            foreach (var erroritem in errors)
            {
                if (erroritem.Level == "error") errorcount++;
            }
            Assert.AreEqual(1, errorcount);
        }
    }
}
