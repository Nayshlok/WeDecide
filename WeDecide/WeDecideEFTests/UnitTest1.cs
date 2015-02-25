using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeDecide.DAL.Concrete;

namespace WeDecideEFTests
{
    [TestClass]
    public class SQLQuestionDALTests
    {
        [TestMethod]
        public void UpdateTest()
        {
            var questionDal = new SqlQuestionDAL();
            Console.WriteLine("There is a thing here: {0}", questionDal.Get(0));

            Assert.IsNotNull(questionDal.GetAll(x => true));
        }
    }
}
