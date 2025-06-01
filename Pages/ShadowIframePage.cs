using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class ShadowIframePage{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public ShadowIframePage(IWebDriver driver){
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void FillIframeInsideShadowDom(string fname){
        IWebElement shadowHost = wait.Until(d => d.FindElement(By.CssSelector("nestedshadow-iframe")));
        var js = (IJavaScriptExecutor)driver;

        IWebElement shadowIframe = (IWebElement)js.ExecuteScript(@"
            return arguments[0].shadowRoot.querySelector('iframe#iframeId');
        ", shadowHost);

        driver.SwitchTo().Frame(shadowIframe);
        driver.FindElement(By.Id("fname")).SendKeys(fname);
        driver.SwitchTo().DefaultContent();
    }
}