using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using todolist;
using Android.OS;
using System.Collections.Generic;
using System.Collections;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        MockDatabase md = new MockDatabase();


        [TestMethod]
        public void AddTest()
        {
            //setup
            var actv = new Activity();
            actv.ID = 2000;
            actv.Title = "Teste";
            actv.Content = "Morbi a metus. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit. Nullam sapien sem, ornare ac, nonummy non, lobortis a, enim. Nunc tincidunt ante vitae massa. Duis ante orci, molestie vitae, vehicula venenatis, tincidunt ac, pede. Nulla accumsan, elit sit amet varius semper, nulla mauris mollis quam, tempor suscipit diam nulla vel leo. Etiam commodo dui eget wisi. Donec iaculis gravida nulla. Donec quis nibh at felis congue commodo. Etiam bibendum elit eget erat.";
            actv.Date = new DateTime().ToString();
            actv.Hour = "18:00:00";
            actv.posSituation = 2;


            md.InsertActivity(actv);

            //result
            Assert.IsTrue(md.GetActivity(actv.ID)!=null, "funcionou!");
        }

        [TestMethod]
        public void ListTest()
        {
            List<Activity> lista = md.GetActivities();

            Assert.IsTrue(lista != null, "Lista contém itens: " + lista.Count);
           // Assert.AreNotEqual(lista != null, "Lista contém itens: " + lista.Count);
        }

        [TestMethod]
        public void ResetListTest() {
            md.DeleteAllActivities();
            Assert.IsTrue(md.GetActivities().Count == 0, "Lista Resetada");
        }

        [TestMethod]
        public void FindTest()
        {
            IEnumerable a = md.GetActivity(2000);
            Assert.IsTrue(a != null, "Funcionou");
        }
    }
}
