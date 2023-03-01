using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using Expect = SeleniumExtras.WaitHelpers.ExpectedConditions;
using OpenQA.Selenium.DevTools.V108.Emulation;

namespace SeleniumWebDriver
{
    [TestFixture]
    public class MyProfile : BaseClass
    {                  
        [TestCase("petko.gqa@gmail.com", "Gond0r!15", "Gond0r!15")]
        [Test]
        public void ChangePassword(string username, string password, string newPassword)
        {
            LogMeIn(username, password);

            wait.Until(Expect.ElementIsVisible(By.ClassName("sidebar-details__name")));
            IWebElement loginUser = Driver.FindElement(By.ClassName("sidebar-details__name"));
            Assert.IsNotNull(loginUser);
            Assert.AreEqual(profileName, loginUser.Text);

            IWebElement loginMessage = Driver.FindElement(By.CssSelector(loginMessagelocatorCss));
            Assert.AreEqual(expectedLoginMessage, loginMessage.Text);

            IWebElement myProfile = Driver.FindElement(By.CssSelector(myProfileLocatorCss));
            myProfile.Click();

            IWebElement changePasswordButton = Driver.FindElement(By.ClassName("account-screen__password-update"));
            changePasswordButton.Click();

            IWebElement newPasswordField = Driver.FindElement(By.Id("password"));
            newPasswordField.SendKeys(newPassword);

            IWebElement confirmNewPasswordField = Driver.FindElement(By.Id("confirmPassword"));
            confirmNewPasswordField.SendKeys(newPassword);

            IWebElement changePassSubmitButton = Driver.FindElement(By.CssSelector(changePasSubmitButtonCss));
            changePassSubmitButton.Click();

            wait.Until(Expect.ElementIsVisible(By.CssSelector(succesfulMessageCss)));
            IWebElement successfulMessage = Driver.FindElement(By.CssSelector(succesfulMessageCss));
            Assert.IsTrue(successfulMessage.Displayed);
            
            LogMeOut();
        }

        [TestCase("petko.gqa@gmail.com", "Gond0r!15", "Gigo Mihajlovski", "Prilep", "7500", "SK", "123456789")]
        [Test]
        public void ChangeMyProfileFields(string email, string password,string address, string city, string postalCode, string country, string phoneNumber)
        {
            

            LogMeIn(email, password);

            IWebElement myProfile = Driver.FindElement(By.CssSelector(myProfileLocatorCss));
            myProfile.Click();

            IWebElement personField = Driver.FindElement(By.ClassName("form-input"));
            //Assert.AreEqual("true", personField.GetDomAttribute("ng-disabled"));
            Assert.False(personField.Enabled);

            IWebElement firstNameFieldMyProfile = Driver.FindElement(By.CssSelector("input[ng-model='vm.clientPerson.user.firstName']"));
            Assert.False(firstNameFieldMyProfile.Enabled);
                       
            IWebElement lastNameFieldMyProfile = Driver.FindElement(By.CssSelector("input[ng-model = 'vm.clientPerson.user.lastName']"));
            Assert.False(lastNameFieldMyProfile.Enabled);

            IWebElement addressFieldMyProfile = Driver.FindElement(By.CssSelector("input[ng-model='vm.clientPerson.address.address']"));
            addressFieldMyProfile.Clear();
            addressFieldMyProfile.SendKeys(address);

            
            IWebElement cityFieldMyProfile = Driver.FindElement(By.CssSelector("input[ng-model = 'vm.clientPerson.address.city']"));
            cityFieldMyProfile.Clear();
            cityFieldMyProfile.SendKeys(city);

            
            IWebElement postalCodeFieldMyProfile = Driver.FindElement(By.CssSelector("input[ng-model = 'vm.clientPerson.address.postalCode']"));
            postalCodeFieldMyProfile.Clear();
            postalCodeFieldMyProfile.SendKeys(postalCode);

            IWebElement countryFieldMyProfile = Driver.FindElement(By.CssSelector("input[ng-model = 'vm.clientPerson.address.country']"));
            countryFieldMyProfile.Clear();
            countryFieldMyProfile.SendKeys(country);
                        
            IWebElement phoneNumberFieldMyProfile = Driver.FindElement(By.CssSelector("input[ng-model = 'vm.clientPerson.phoneNumber']"));
            phoneNumberFieldMyProfile.Clear();
            phoneNumberFieldMyProfile.SendKeys(phoneNumber);

            IWebElement updateButton = Driver.FindElement(By.CssSelector("button[translate='provider.update']"));
            updateButton.Click();

            wait.Until(Expect.ElementIsVisible(By.CssSelector("strong[translate='provider.updateSuccess']")));
            IWebElement successfulUpdateMessage = Driver.FindElement(By.CssSelector("strong[translate='provider.updateSuccess']"));
            Assert.True(successfulUpdateMessage.Displayed);
        }
    }
}
