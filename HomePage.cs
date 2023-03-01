using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumWebDriver
{
    [TestFixture]
    public class HomePage : BaseClass
    {

        [Test]
        public void TestLogo()
        {
            Driver.Navigate().GoToUrl("http://18.156.17.83:9095/how-it-works");

            IWebElement logo = Driver.FindElement(By.ClassName("logo-img"));
            logo.Click();

            string homePageUrl = "http://18.156.17.83:9095/";

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(homePageUrl));

            Assert.AreEqual(homePageUrl, Driver.Url);

        }


        [Test]
        public void TestHome()
        {
            IWebElement tableBody = Driver.FindElement(By.ClassName("table-body"));

            List<IWebElement> listOfRows = tableBody.FindElements(By.TagName("tr")).ToList();

            IWebElement firstRow = listOfRows[0];

            List<IWebElement> listOfColumn = firstRow.FindElements(By.TagName("td")).ToList();

            string categoryName = "Skopje";

            List<IWebElement> listOfCategoryNames = listOfColumn.Where(el => el.Text == categoryName).ToList();

           int numberMK = listOfCategoryNames.Count();
            Console.WriteLine(numberMK);

            //int countBaranja = listOfRows.Count;

            //Assert.AreEqual(7, countBaranja);
            /*od Frosina
            public void TableExercise()
            {
            IWebElement tableBody = Driver.FindElement(By.ClassName("table-body"));

            List<IWebElement> listOfRows = tableBody.FindElements(By.TagName("tr")).ToList();

            //IWebElement firstRow = listOfRows[0];
            IWebElement firstRow = listOfRows.First();

            IWebElement fifthColumn = firstRow.FindElement(By.CssSelector("td[class='table-body__cell column5']"));

            string text = fifthColumn.Text;

            Assert.AreEqual("Комбе", text);

            List<IWebElement> listOfColumns = tableBody.FindElements(By.TagName("td")).ToList();

            string categoryName = "Комбе";

            List<IWebElement> listOfCategoryNames = listOfColumns.Where(el => el.Text == categoryName).ToList();

            int broj = listOfCategoryNames.Count();
            Assert.AreEqual(1, broj);

            int countBaranja = listOfRows.Count;

            Assert.AreEqual(2, countBaranja);
            }*/
        }
    }
}
