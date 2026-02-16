using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using MonsterAutomation.Tests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAutomation.Tests.Base
{
    public abstract class BaseTest : PageTest
    {

        protected IPlaywright playwright;
        protected IBrowser browser;
        protected IBrowserContext context;
        protected IPage page;

        protected const string BaseUrl = "http://localhost:3000/";
        protected CreateMonstersPage monstersPage;



        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Headless = false
            });
        }


        [SetUp]
        public async Task Setup()
        {
            context = await browser.NewContextAsync();
            page = await browser.NewPageAsync();

            await page.GotoAsync(BaseUrl);
            monstersPage = new CreateMonstersPage(page);
            
        }


        [TearDown]
        public async Task TearDown()
        {
            await context.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task GlobalTearDown()
        {
            await browser.CloseAsync();
            playwright.Dispose();
        }
    }
}
