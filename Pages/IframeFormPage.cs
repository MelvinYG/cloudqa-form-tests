using OpenQA.Selenium;

public class IframeFormPage{
    private readonly IWebDriver driver;
    public IframeFormPage(IWebDriver driver) => this.driver = driver;

    public void FillFirstIframe(string fname, string lname, string email){
        driver.SwitchTo().Frame(0);
        FillInputs(fname, lname, email);
        driver.SwitchTo().DefaultContent();
    }

    public void FillNamedIframe(string frameId, string fname, string lname, string email){
        driver.SwitchTo().Frame(frameId);
        FillInputs(fname, lname, email);
        driver.SwitchTo().DefaultContent();
    }

    private void FillInputs(string fname, string lname, string email){
        driver.FindElement(By.Id("fname")).SendKeys(fname);
        driver.FindElement(By.Id("lname")).SendKeys(lname);
        driver.FindElement(By.Id("email")).SendKeys(email);
    }
}