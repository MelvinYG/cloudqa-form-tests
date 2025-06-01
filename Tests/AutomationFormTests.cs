using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

class AutomationFormTests{
    public static void RunTests(){
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        using (IWebDriver driver = new ChromeDriver(options)){
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");
            Thread.Sleep(2000);

            try{
                var normalForm = new NormalFormPage(driver);
                normalForm.FillForm("Melvin", "George", "melvingeorge204@gmail.com");
                Console.WriteLine("Normal form filled successfully");

                var iframeForm = new IframeFormPage(driver);
                iframeForm.FillFirstIframe("Melvin", "George", "melvingeorge204@gmail.com");
                Console.WriteLine("iframe without id filled successfully");

                iframeForm.FillNamedIframe("iframeId", "Melvin", "George", "melvingeorge204@gmail.com");
                Console.WriteLine("iframe with id filled successfully");

                var shadowIframe = new ShadowIframePage(driver);
                shadowIframe.FillIframeInsideShadowDom("Melvin");
                Console.WriteLine("iframe + shadow filled successfully");

                var deepShadow = new DeepShadowDomPage(driver);
                deepShadow.FillDeepInput("Melvin");
                Console.WriteLine("Nested shadow DOM filled successfully");
            }catch(Exception ex){
                Console.WriteLine($"Error: {ex.Message}");
            }

            Thread.Sleep(7000);
            driver.Quit();
        }
    }
}
