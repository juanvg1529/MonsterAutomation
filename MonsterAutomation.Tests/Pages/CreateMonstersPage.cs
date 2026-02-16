using Microsoft.Playwright;
using MonsterAutomation.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAutomation.Tests.Pages
{
    public class CreateMonstersPage
    {
        private readonly IPage _page;

        public CreateMonstersPage(IPage page)
        {
            _page = page;
        }

        public Task ClickMonster(int index) => _page.GetByTestId($"monster-{index}").ClickAsync();

        public Task SetMonsterName (string name)=> _page.GetByTestId(LocatorsPage.MonsterNameInput).Locator("input").FillAsync(name);
        public Task SetMonsterHP (int value)=> _page.GetByTestId(LocatorsPage.HPValue).Locator("input").FillAsync(value.ToString());
        public Task SetMonsterDefense (int value)=> _page.GetByTestId(LocatorsPage.DefenseValue).Locator("input").FillAsync(value.ToString());
        public Task SetMonsterAttack (int value)=> _page.GetByTestId(LocatorsPage.AttackValue).Locator("input").FillAsync(value.ToString());
        public Task SetMonsterSpeed(int value)=> _page.GetByTestId(LocatorsPage.SpeedValue).Locator("input").FillAsync(value.ToString());

        public Task ClickCreateMonster()=> _page.GetByTestId(LocatorsPage.CreateMonsterBtn).ClickAsync();

        public Task<string> GetDynamicTitle()=> _page.GetByTestId(LocatorsPage.DynamicTitle).InnerTextAsync();

        public Task <int> GetMonsterCardCount()=> _page.GetByTestId(LocatorsPage.MonsterCard).CountAsync();


        public async Task CreateMonster(int index, MonsterModel monster)
        {
            await ClickMonster(index);
            await SetMonsterName(monster.Name);
            await SetMonsterHP(monster.Hp);
            await SetMonsterDefense(monster.Defense);
            await SetMonsterAttack(monster.Attack);
            await SetMonsterSpeed(monster.Speed);

            await ClickCreateMonster();
        }

        public ILocator GetMonsterCardByName(string name)
        {
            var nameLocator = _page.GetByTestId(LocatorsPage.MonsterCardName).Filter(new() { HasTextString = name });
             return _page.GetByTestId(LocatorsPage.MonsterCard).Filter(new() { Has=nameLocator });
        }
        public Task ScrollToMonstersCard (string name)=> GetMonsterCardByName(name).ScrollIntoViewIfNeededAsync();
        public Task<bool> IsMonsterCardVisible(string name)=> GetMonsterCardByName(name).IsVisibleAsync();

        public Task<bool> IsAlertPresent()=> _page.GetByTestId(LocatorsPage.AlertRequiredFields).IsVisibleAsync();

        public Task ClickDeleteBtn(string name) => GetMonsterCardByName(name).GetByTestId(LocatorsPage.CardDeleteBtn).ClickAsync();
        public Task ClickFavoriteBtn(string name) => GetMonsterCardByName(name).GetByTestId(LocatorsPage.FavoriteBtn).ClickAsync();
        public Task<string?> GetFavoriteStateCardMonster(string name) => GetMonsterCardByName(name).GetByTestId(LocatorsPage.FavoriteBtn).GetAttributeAsync("style");


        public Task<string?> GetMonsterCardHp(string name) => GetMonsterCardByName(name).GetByTestId(LocatorsPage.MonsterCardHpValue).GetAttributeAsync("aria-valuenow");
        public Task<string?> GetMonsterCardDefense(string name) => GetMonsterCardByName(name).GetByTestId(LocatorsPage.MonsterCardDefenseValue).GetAttributeAsync("aria-valuenow");
        public Task<string?> GetMonsterCardAttack(string name) => GetMonsterCardByName(name).GetByTestId(LocatorsPage.MonsterCardAttackValue).GetAttributeAsync("aria-valuenow");
        public Task<string?> GetMonsterCardSpeed(string name) => GetMonsterCardByName(name).GetByTestId(LocatorsPage.MonsterCardSpeedValue).GetAttributeAsync("aria-valuenow");

        private static class LocatorsPage
        {
            public const string MonsterNameInput = "monster-name";
            public const string HPValue = "hp-value";
            public const string DefenseValue = "defense-value";
            public const string AttackValue = "attack-value";
            public const string SpeedValue = "speed-value";

            public const string CreateMonsterBtn = "btn-create-monster";
            public const string DynamicTitle = "dynamic-title";

            public const string MonsterCard = "monster-card";
            public const string MonsterCardName = "card-monster-name";
            public const string MonsterCardHpValue = "card-monster-hp";
            public const string MonsterCardDefenseValue = "card-monster-defense";
            public const string MonsterCardAttackValue = "card-monster-attack";
            public const string MonsterCardSpeedValue = "card-monster-speed";


            public const string AlertRequiredFields = "alert-required-fields";

            public const string CardDeleteBtn = "btn-delete";
            public const string FavoriteBtn = "favorite-btn";



        }
    }
}
