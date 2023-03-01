using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expect = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System.Net;

namespace SeleniumWebDriver
{
    public class BaseClass
    {
        public IWebDriver Driver;

        public WebDriverWait wait;  
        
        public Random random = new Random();

        public string homePageUrl = "http://18.156.17.83:9095/";

        public string expectedRegisterTitle = "Регистрација";
        public string welcomeMesasage = "ДОБРЕДОЈДОВТЕ!";
        public string expectedLoginMessage = "Добредојдовте на cargodom.com";
        public string profileName = "Александар Петковски\r\nDfTreluz";
        public string expectedTextMessage = "Вашиот обид е неуспешен! Ве молиме проверете го вашето корисничко име и лозинка и обидете се повторно.";
        
        public const string loginSubmitButtonLocatorCss = "button[translate = 'login.form.button']";
        public const string loginMessagelocatorCss = "h2[translate = 'provider.welcomeMessage']";

        public const string myProfileLocatorCss = "a[ui-sref='client-account']";

        public const string changePasSubmitButtonCss = "button[type='submit']";
        public const string succesfulMessageCss = "div[ng-show='vm.success']";
        public const string neuspesenObidWarrningLocator = "div[ng-show='vm.authenticationError']";

        public const string firstNameFieldCss = "input[ng-model = 'vm.providerPerson.user.firstName']";
        public const string lastNameFieldCss = "input[ng-model = 'vm.providerPerson.user.lastName']";
        public const string companyNameFieldCss = "input[ng-model = 'vm.providerPerson.providerCompany.name']";
        public const string addressFieldCss = "input[ng-model = 'vm.providerPerson.providerCompany.address.address']";
        public const string cityFieldCss = "input[ng-model = 'vm.providerPerson.providerCompany.address.city']";
        public const string postalCodeFieldCss = "input[ng-model = 'vm.providerPerson.providerCompany.address.postalCode']";
        public const string taxNumberFieldCss = "input[ng-model = 'vm.providerPerson.providerCompany.taxNumber']";
        public const string phoneNumberFieldCss = "input[ng-model = 'vm.providerPerson.phoneNumber']";
        public const string emailFieldCss = "input[ng-model = 'vm.providerPerson.user.email']";
        private const string countryListCssRegistration = "div[ng-attr-id='ui-select-choices-row-{{ $select.generatedId }}-{{$index}}']";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40); //da se pojavat elementite vo DOM
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(35);  // da se gledaat elementite na stranata
            Driver.Manage().Window.Maximize();

            Driver.Navigate().GoToUrl("http://18.156.17.83:9095/");

            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }
        [TearDown]
        public void TearDown()
        {
            Driver.Close();
            Driver.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        public void LogMeIn(string username, string password)
        {
            IWebElement signInButton = Driver.FindElement(By.Id("login"));
            signInButton.Click();

            IWebElement usernameInput = Driver.FindElement(By.Id("username"));
            usernameInput.Clear();
            usernameInput.SendKeys(username);

            IWebElement passwordInput = Driver.FindElement(By.Id("password"));
            passwordInput.Clear();
            passwordInput.SendKeys(password);

            IWebElement rememberMeCheckBox = Driver.FindElement(By.Id("rememberMe"));
            rememberMeCheckBox.Click();

            IWebElement loginSubmitButton = Driver.FindElement(By.CssSelector(loginSubmitButtonLocatorCss));
            loginSubmitButton.Click();
        }

        public void LogMeOut()
        {
            IWebElement logOutButton = Driver.FindElement(By.Id("logout2"));

            Assert.IsTrue(logOutButton.Displayed);

            IWebElement signOutButton = Driver.FindElement(By.Id("logout2"));
            signOutButton.Click();

            wait.Until(Expect.UrlToBe(homePageUrl));

            Assert.AreEqual(homePageUrl, Driver.Url);
        }

        /// <summary>
        /// Ovaa metoda ne nosi do formata za registracija
        /// </summary>
        public void LoadAndCheckRegistrationForm()
        {
            wait.Until(Expect.ElementToBeClickable(By.CssSelector("a[ui-sref='register']")));

            IWebElement registrationButton = Driver.FindElement(By.CssSelector("a[ui-sref='register']"));
            Assert.IsTrue(registrationButton.Displayed);
            registrationButton.Click();

            wait.Until(Expect.ElementToBeClickable(By.CssSelector("button[ui-sref='register-provider']")));

            IWebElement iAmTransporterButton = Driver.FindElement(By.CssSelector("button[ui-sref='register-provider']"));
            Assert.IsTrue(iAmTransporterButton.Displayed);
            iAmTransporterButton.Click();


            IWebElement registerTitle = Driver.FindElement(By.CssSelector("h1[translate='register.title']"));
            Assert.AreEqual(expectedRegisterTitle, registerTitle.Text);//go proveruva naslovot

            IWebElement registerForm = Driver.FindElement(By.CssSelector("form[ng-show= '!vm.success']"));
            Assert.IsTrue(registerForm.Displayed); //proveruva dali sme vo formata registracija
        }

        public void EnterDataAndSubmitRegistrationForm(string firstName, string lastName, string companyName, string address, string city, string postalCode, string taxNumber, string phoneNumber, string email, string password, string confirmPassword)
        {
            IWebElement firstNameField = Driver.FindElement(By.CssSelector(firstNameFieldCss));
            firstNameField.Clear();
            firstNameField.SendKeys(firstName);

            IWebElement lastNameField = Driver.FindElement(By.CssSelector(lastNameFieldCss));
            lastNameField.Clear();
            lastNameField.SendKeys(lastName);

            IWebElement companyNameField = Driver.FindElement(By.CssSelector(companyNameFieldCss));
            companyNameField.Clear();
            companyNameField.SendKeys(companyName);

            IWebElement addressField = Driver.FindElement(By.CssSelector(addressFieldCss));
            addressField.Clear();
            addressField.SendKeys(address);

            IWebElement cityField = Driver.FindElement(By.CssSelector(cityFieldCss));
            cityField.Clear();
            cityField.SendKeys(city);

            IWebElement postalCodeField = Driver.FindElement(By.CssSelector(postalCodeFieldCss));
            postalCodeField.Clear();
            postalCodeField.SendKeys(postalCode);

            //IWebElement countryField = Driver.FindElement(By.ClassName("ui-select-match"));
            //countryField.Click();

            SearchRandomCountry();

            IWebElement taxNumberField = Driver.FindElement(By.CssSelector(taxNumberFieldCss));
            taxNumberField.Clear();
            taxNumberField.SendKeys(taxNumber);

            IWebElement phoneNumberField = Driver.FindElement(By.CssSelector(phoneNumberFieldCss));
            phoneNumberField.Clear();
            phoneNumberField.SendKeys(phoneNumber);

            IWebElement emailField = Driver.FindElement(By.CssSelector(emailFieldCss));
            emailField.Clear();
            emailField.SendKeys(email);

            IWebElement passwordField = Driver.FindElement(By.Id("password"));
            passwordField.Clear();
            passwordField.SendKeys(password);

            IWebElement confirmPasswordField = Driver.FindElement(By.Id("confirmPassword"));
            confirmPasswordField.Clear();
            confirmPasswordField.SendKeys(confirmPassword);

            IWebElement acceptTermsCheck = Driver.FindElement(By.Id("acceptTerms"));
            acceptTermsCheck.Click();

            IWebElement submitButton = Driver.FindElement(By.CssSelector("input[ng-click = 'form.$valid && vm.register()']"));
            submitButton.Click();

        }

        public void SearchRandomCountry()
        {
            List<IWebElement> countryList = Driver.FindElements(By.CssSelector(countryListCssRegistration)).ToList();

            int n = countryList.Count();

            countryList[random.Next(0, n)].Click();
        }
    }

}
