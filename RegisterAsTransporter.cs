using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expect = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SeleniumWebDriver
{
    [TestFixture]
    internal class RegisterAsTransporter : BaseClass
    {        
        [Description("Happy path!")]
        [Order(1)]
        [TestCase("Sanja", "Boceska", "BocaPromet", "Gigo", "Skopje","1000", "1234567891234", "+123456789", "sanabo1528@bocata.pro", "!2#4Sanja", "!2#4Sanja")]
        [Test]
        public void registerValidData(string firstName, string lastName, string companyName, string address, string city, string postalCode, string taxNumber, string phoneNumber, string email, string password, string confirmPassword)
        {
            LoadAndCheckRegistrationForm();

            EnterDataAndSubmitRegistrationForm(firstName, lastName, companyName, address, city, postalCode, taxNumber, phoneNumber, email, password, confirmPassword);

            wait.Until(Expect.ElementIsVisible(By.ClassName("successful-registration__main-title")));
            IWebElement welcome = Driver.FindElement(By.ClassName("successful-registration__main-title"));

            Assert.AreEqual(welcomeMesasage, welcome.Text);
        }

        [Description("Unsuccessful registration")]
        [TestCase("", "", "", "", "", "", "", "", "", "", "")]
        [Test]
        public void emptyFields(string firstName, string lastName, string companyName, string address, string city, string postalCode, string taxNumber, string phoneNumber, string email, string password, string confirmPassword) 
        {
            LoadAndCheckRegistrationForm();

            EnterDataAndSubmitRegistrationForm(firstName, lastName, companyName, address, city, postalCode, taxNumber, phoneNumber, email, password, confirmPassword);
                      
            wait.Until(Expect.ElementIsVisible(By.CssSelector("p[translate = 'entity.validation.fillAllFields']")));
                      
            IWebElement errorMessageSubmit = Driver.FindElement(By.CssSelector("p[translate = 'entity.validation.fillAllFields']"));//poraka posle poleto SUBMIT
            Assert.IsTrue(errorMessageSubmit.Displayed);          
        }

        [Description("Unsuccessful registration")]
        [TestCase("Sanja", "Siloska", "BocaPromet", "Gigo", "Skopje", "1000", "1234567891234", "+123456789", "sanabo1528@bocata.pro", "!2#4Sanja", "!2#4Sanja")]
        [Test]
        public void sameEmail(string firstName, string lastName, string companyName, string address, string city, string postalCode, string taxNumber, string phoneNumber, string email, string password, string confirmPassword)
        {
            LoadAndCheckRegistrationForm();

            EnterDataAndSubmitRegistrationForm(firstName, lastName, companyName, address, city, postalCode, taxNumber, phoneNumber, email, password, confirmPassword);

            wait.Until(Expect.ElementIsVisible(By.CssSelector("p[ng-show='vm.errorEmailExists']")));

            IWebElement alreadyUsedMailMessage = Driver.FindElement(By.CssSelector("p[ng-show='vm.errorEmailExists']"));
            Assert.IsTrue(alreadyUsedMailMessage.Displayed);
        }

        [Description("Unsuccessful registration")]
        [TestCase("Sanja", "Siloska", "BocaPromet", "Gigo", "Skopje", "1000", "1234567891234", "+123456789", "sana35867@bocata.pro", "!2#", "!2#")]
        [Test]
        public void threeCharactersPassword(string firstName, string lastName, string companyName, string address, string city, string postalCode, string taxNumber, string phoneNumber, string email, string password, string confirmPassword)
        {
            LoadAndCheckRegistrationForm();

            EnterDataAndSubmitRegistrationForm(firstName, lastName, companyName, address, city, postalCode, taxNumber, phoneNumber, email, password, confirmPassword);

            wait.Until(Expect.ElementIsVisible(By.CssSelector("p[ng-show='form.password.$error.minlength']")));

            IWebElement shortPasswordMessage = Driver.FindElement(By.CssSelector("p[ng-show='form.password.$error.minlength']"));
            Assert.IsTrue(shortPasswordMessage.Displayed);
        }

        [Description("Unsuccessful registration")]
        [TestCase("Sanja", "Siloska", "BocaPromet", "Gigo", "Skopje", "1000", "1234567891234", "+123456789", "sana12586@bocata.pro", "Sanja", "Goran")]
        [Test]
        public void wrongConfirmPasword(string firstName, string lastName, string companyName, string address, string city, string postalCode, string taxNumber, string phoneNumber, string email, string password, string confirmPassword)
        {
            LoadAndCheckRegistrationForm();

            EnterDataAndSubmitRegistrationForm(firstName, lastName, companyName, address, city, postalCode, taxNumber, phoneNumber, email, password, confirmPassword);

            wait.Until(Expect.ElementIsVisible(By.CssSelector("div[ng-show='vm.doNotMatch']")));

            IWebElement wrongConfirmPasswordMessage = Driver.FindElement(By.CssSelector("div[ng-show='vm.doNotMatch']"));
            Assert.IsTrue(wrongConfirmPasswordMessage.Displayed);
        }

        [Description("The frame of field is coloring red and the registration is unsuccessful")]
        [TestCase("Sanja", "Siloska", "BocaPromet", "Gigo", "Skopje", "1000", "1234567891234", "abcdefghi", "anaB1528B@bocata.pro", "!2#4Sanja", "!2#4Sanja")]
        [TestCase("Sanja", "Siloska", "BocaPromet", "Gigo", "Skopje", "1000", "1234567891234", "-----------", "anaBo152W8@bocata.pro", "!2#4Sanja", "!2#4Sanja")]
        [TestCase("Sanja", "Siloska", "BocaPromet", "Gigo", "Skopje", "1000", "1234567891234", "           ", "anaBoc1528G@bocata.pro", "!2#4Sanja", "!2#4Sanja")]
        [Test]
        public void phoneNumberFillABC(string firstName, string lastName, string companyName, string address, string city, string postalCode, string taxNumber, string phoneNumber, string email, string password, string confirmPassword)
        {
            LoadAndCheckRegistrationForm();

            EnterDataAndSubmitRegistrationForm(firstName, lastName, companyName, address, city, postalCode, taxNumber, phoneNumber, email, password, confirmPassword);

            wait.Until(Expect.ElementIsVisible(By.CssSelector("p[translate = 'entity.validation.fillAllFields']")));

            IWebElement errorMessageSubmit = Driver.FindElement(By.CssSelector("p[translate = 'entity.validation.fillAllFields']"));//poraka posle poleto SUBMIT

            Assert.IsTrue(errorMessageSubmit.Displayed);

                    
        }

        [Description("Bonus Test from Me - Verify that we can choose a country from dropdown menu")]
        [Test]
        public void countryDropDownMenu()
        {
            LoadAndCheckRegistrationForm();

            string selectedCountryName = "Macedonia";

            IWebElement dropDownMenu = Driver.FindElement(By.CssSelector("country-selector[selected-country-code='vm.providerPerson.providerCompany.address.country'] > div.ui-select-container"));
            dropDownMenu.Click();

            Assert.That(dropDownMenu.GetDomAttribute("class").Contains("open"), Is.EqualTo(true).After(100).MilliSeconds);

            IWebElement countryMk = Driver.FindElement(By.CssSelector("span[country='MK']"));
            countryMk.Click();

            Assert.That(dropDownMenu.GetDomAttribute("class").Contains("ng-not-empty"), Is.EqualTo(true).After(100).MilliSeconds);

            IWebElement selectedCountry = Driver.FindElement(By.CssSelector("span[ng-bind='$select.selected.name']"));

            Assert.AreEqual(selectedCountryName, selectedCountry.Text);
        }      
    }
}
