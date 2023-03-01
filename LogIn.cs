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
using Expect = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SeleniumWebDriver
{
    [TestFixture]
    public class LogIn : BaseClass
    {       
        
        [TestCase("petko.gqa@gmail.com", "Gond0r!15")]
        [Test]
        public void loginValid(string username, string password)
        {
            LogMeIn(username, password);

            IWebElement loginUser = Driver.FindElement(By.ClassName("sidebar-details__name"));
            Assert.IsNotNull(loginUser);
            Assert.AreEqual(profileName, loginUser.Text);

            IWebElement loginMessage = Driver.FindElement(By.CssSelector(loginMessagelocatorCss));
            Assert.AreEqual(expectedLoginMessage, loginMessage.Text);

            wait.Until(Expect.ElementIsVisible(By.Id("logout2")));

            LogMeOut();      
        }

        [TestCase("petko.gqa", "Gond0r!15")]
        [Test]
        public void loginInvalid(string username, string password)
        {
            LogMeIn(username, password);
            
            wait.Until(Expect.ElementIsVisible(By.CssSelector(neuspesenObidWarrningLocator)));

            IWebElement neuspesenObidWarrning = Driver.FindElement(By.CssSelector(neuspesenObidWarrningLocator));

            string actualTextMessage = neuspesenObidWarrning.Text;
            Assert.AreEqual(expectedTextMessage, actualTextMessage);
        }        
    }
}
