using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class DeepShadowDomPage{
    private readonly IWebDriver driver;

    public DeepShadowDomPage(IWebDriver driver){
        this.driver = driver;
    }

    public void FillDeepInput(string fname){
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => {
            var js = (IJavaScriptExecutor)d;
            return (bool)js.ExecuteScript(@"
                const el = document.querySelector('nestedshadow-form5');
                return !!el && !!el.shadowRoot;
            ");
        });

        // testing 5 layered shadow dom
        var input = GetDeepShadowInput(
            "nestedshadow-form5",
            "nestedshadow-form4",
            "nestedshadow-form3",
            "nestedshadow-form",
            "shadow-form",
            "section[slot='fname']"
        );
        input.SendKeys(fname);
    }

    private IWebElement GetDeepShadowInput(params string[] selectors){
        string script = @"
            let el = document.querySelector(arguments[0]);
            if (!el) throw 'Selector not found ' + arguments[0];
            for (let i = 1; i < arguments.length - 2; i++) {
                el = el.shadowRoot;
                if (!el) throw 'ShadowRoot not found: ' + arguments[i - 1];
                el = el.querySelector(arguments[i]);
                if (!el) throw 'Selector not found: ' + arguments[i];
            }

            el = el.shadowRoot;
            if (!el) throw 'ShadowRoot not found before last element';
            el = el.querySelector(arguments[arguments.length - 2]);
            if (!el) throw 'Selector not found: ' + arguments[arguments.length - 2];

            let section = el.querySelector(arguments[arguments.length - 1]);
            if (!section) throw 'Section element not found';

            let input = section.querySelector('input');
            if (!input) throw 'Input element not found inside';
            return input;
        ";

        return (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(script, selectors);
    }
}