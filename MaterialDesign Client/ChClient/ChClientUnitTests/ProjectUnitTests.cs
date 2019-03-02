using System;
using System.Collections.Generic;
using ChClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChClientUnitTests
{
    [TestClass]
    public class ProjectUnitTests
    {
        //mock-ok
        //feltételezhetjük, hogy nem null listákat kapunk adatelérési oldalról
        //(akkor se, ha 0 darab értéket kapunk DB oldalról)
        private List<string> usersmock = new List<string>();
        
        [TestMethod]
        public void EmptyProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate(usersmock);

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void NoNameProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();
            usersmock.Add("TestUser");

            item.Name = "";
            item.Leader = "TestUser";
            item.Goal = "Pass all unit tests";
            item.Description = "Testing";
            item.CurrentPlan = "plan.jpg";

            errors = item.Validate(usersmock);

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void NoLeaderProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();
            usersmock.Add("TestUser");

            item.Name = "Testing project";
            item.Leader = "";
            item.Goal = "Pass all unit tests";
            item.Description = "Testing";
            item.CurrentPlan = "plan.jpg";

            errors = item.Validate(usersmock);

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void InvalidLeaderProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();
            usersmock.Add("TestUser");

            item.Name = "Testing project";
            item.Leader = "Person";
            item.Goal = "Pass all unit tests";
            item.Description = "Testing";
            item.CurrentPlan = "plan.jpg";

            errors = item.Validate(usersmock);

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void NoGoalProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();
            usersmock.Add("TestUser");

            item.Name = "Testing project";
            item.Leader = "TestUser";
            item.Goal = "";
            item.Description = "Testing";
            item.CurrentPlan = "plan.jpg";

            errors = item.Validate(usersmock);

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void LongGoalProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();
            usersmock.Add("TestUser");

            item.Name = "Testing project";
            item.Leader = "TestUser";
            item.Goal = "Ez egy nagyon hosszú cél leírás, ami már nem férne be az adatbázisba ezért nem leg megfelelni validáció során és így nem fog mentődni az adatbázisba se";
            item.Description = "Testing";
            item.CurrentPlan = "plan.jpg";

            errors = item.Validate(usersmock);

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void NoDescriptionProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();
            usersmock.Add("TestUser");

            item.Name = "Testing project";
            item.Leader = "TestUser";
            item.Goal = "Pass all unit tests";
            item.Description = "";
            item.CurrentPlan = "plan.jpg";

            errors = item.Validate(usersmock);

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void NoPlaneProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();
            usersmock.Add("TestUser");

            item.Name = "Testing project";
            item.Leader = "TestUSer";
            item.Goal = "Pass all unit tests";
            item.Description = "Testing";
            item.CurrentPlan = "";

            errors = item.Validate(usersmock);

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void CompleteProjectTest()
        {
            Project item = new Project();
            List<OutputMessage> errors = new List<OutputMessage>();
            usersmock.Add("TestUser");

            item.Name = "Testing project";
            item.Leader = "TestUser";
            item.Goal = "Pass all unit tests";
            item.Description = "Testing";
            item.CurrentPlan = "plan.jpg";

            errors = item.Validate(usersmock);

            Assert.AreEqual(0, errors.Count);
        }
    }
}
