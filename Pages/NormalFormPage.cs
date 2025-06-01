using OpenQA.Selenium;

public class NormalFormPage
{
    private readonly IWebDriver driver;

    public NormalFormPage(IWebDriver driver) => this.driver = driver;

    public void FillForm(string fname, string lname, string email)
    {
        driver.FindElement(By.Id("fname")).SendKeys(fname);
        driver.FindElement(By.Id("lname")).SendKeys(lname);
        driver.FindElement(By.Id("email")).SendKeys(email);
    }
}