using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriver
{
    public class MyRequests : BaseClass
    {
        private const string myRequestsButtonCss = "a[ui-sref='client-my-requests']";
        private const string activeMyRequestsButtonCss = "a[ui-sref='client-my-active-requests']";

        int numberOfActiveRequests;
       
        [TestCase("petko.gqa@gmail.com", "Gond0r!15", 5)]
        [Test]
        public void CheckMyRequests(string email, string password, int expectedNumberOfActiveRequests)
        {
            LogMeIn(email, password);

            IWebElement myRequestsButton = Driver.FindElement(By.CssSelector(myRequestsButtonCss));
            myRequestsButton.Click();

            IWebElement activeMyRequestsButton = Driver.FindElement(By.CssSelector(activeMyRequestsButtonCss));
            Assert.True(activeMyRequestsButton.Enabled);

            IWebElement tableBodyActiveRequests = Driver.FindElement(By.ClassName("table-body"));
            List<IWebElement> listOfRowsActiveRequests = tableBodyActiveRequests.FindElements(By.TagName("tr")).ToList();

            numberOfActiveRequests = listOfRowsActiveRequests.Count;

            Assert.AreEqual(expectedNumberOfActiveRequests, numberOfActiveRequests);

            IWebElement secondRowActiveRequests = listOfRowsActiveRequests[1];

            List<IWebElement> listOfColumnsActiveRequests = secondRowActiveRequests.FindElements(By.TagName("td")).ToList();

            
            IWebElement pickUpColumnActiveRequest = listOfColumnsActiveRequests[1];
            IWebElement deliveryColumnActiveRequest= listOfColumnsActiveRequests[2];
            IWebElement categoryColumnActiveRequest = listOfColumnsActiveRequests[4];

            //IWebElement titleColumnActiveRequest = listOfColumnsActiveRequests[0];
            //Console.WriteLine(titleColumnActiveRequest.Text);
            //Console.WriteLine(pickUpColumnActiveRequest.Text);
            //Console.WriteLine(deliveryColumnActiveRequest.Text);
            //Console.WriteLine(categoryColumnActiveRequest.Text);

            IWebElement mkFlag = pickUpColumnActiveRequest.FindElement(By.CssSelector("span[class='flag mk']"));
            Assert.True(mkFlag.Displayed);           

            IWebElement rsFlag = deliveryColumnActiveRequest.FindElement(By.CssSelector("span[class='flag rs']"));
            Assert.True(rsFlag.Displayed);
            
            Assert.IsTrue(categoryColumnActiveRequest.Text.Contains("Мебел"));

            LogMeOut();
        }
    }
}
