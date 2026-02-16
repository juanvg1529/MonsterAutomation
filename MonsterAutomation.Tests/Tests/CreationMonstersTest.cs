using MonsterAutomation.Tests.Base;
using MonsterAutomation.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAutomation.Tests.Tests
{
    [TestFixture,NonParallelizable]
    public class CreationMonstersTest : BaseTest
    {
        public string InitialTitle = "There are no monsters";

        [Test, Description("initial smoke test to verify that aren't monsters created")]
        public async Task InitialState_ShouldNotHaveMonstersCreated()
        {
            var dynamicTitle = await monstersPage.GetDynamicTitle();
            var initialCountCardsPresent = await monstersPage.GetMonsterCardCount();

            Assert.That(dynamicTitle.Trim(), Is.EqualTo(InitialTitle),"title has change from the initial value");
            Assert.That(initialCountCardsPresent, Is.EqualTo(0), "smoke test, cards present count > 1");

        }

        [Test, Description("happy path, monster created successfuly")]
        public async Task Should_Create_Monster_Successfuly()
        {
            var monster = new MonsterModel() { Name = "SuccesMonster",Hp=10,Attack=62,Defense=85,Speed=25 };
            await monstersPage.CreateMonster(4, monster);
            await monstersPage.ScrollToMonstersCard(monster.Name);

            var dynamicTitle = await monstersPage.GetDynamicTitle();
            var initialCountCardsPresent = await monstersPage.GetMonsterCardCount();
            var monsterCreated = await monstersPage.IsMonsterCardVisible(monster.Name); 

            Assert.That(dynamicTitle.Trim(), Is.Not.EqualTo(InitialTitle), "initial value of the title hasn't change");
            Assert.That(initialCountCardsPresent, Is.EqualTo(1), "the cards present aren't the expected");
            Assert.That(monsterCreated,Is.True,"Card of monster is not visible");

        }

        [Test, Description("unhappy path, monster is note created successfuly with invalid stats")]
        public async Task Should_Not_Create_Monster_With_Invalid_Stats()
        {
            var monster = new MonsterModel() { Name = "SuccesMonster", Hp = 0, Attack = 0, Defense = 0, Speed = 0 };
            await monstersPage.CreateMonster(1, monster);
          
            var dynamicTitle = await monstersPage.GetDynamicTitle();
            var initialCountCardsPresent = await monstersPage.GetMonsterCardCount();
            var monsterCreated = await monstersPage.IsMonsterCardVisible(monster.Name);
            var isAlertVisible = await monstersPage.IsAlertPresent();

            Assert.That(dynamicTitle.Trim(), Is.EqualTo(InitialTitle), "initial value of the title hasn't change");
            Assert.That(initialCountCardsPresent, Is.EqualTo(0), "the cards present aren't the expected");
            Assert.That(isAlertVisible, Is.True, "Card of monster is not visible");
            Assert.That(monsterCreated, Is.False, "Card of monster is not visible");

        }


        [Test, Description("happy path,delete monster created successfuly")]
        public async Task Should_Delete_Created_Monster_Successfuly()
        {
            var monster = new MonsterModel() { Name = "deletedMonster", Hp = 10, Attack = 62, Defense = 85, Speed = 25 };
            await monstersPage.CreateMonster(4, monster);
            await monstersPage.ScrollToMonstersCard(monster.Name);

            var dynamicTitle = await monstersPage.GetDynamicTitle();
            var initialCountCardsPresent = await monstersPage.GetMonsterCardCount();
            var monsterCreated = await monstersPage.IsMonsterCardVisible(monster.Name);

            Assert.That(dynamicTitle.Trim(), Is.Not.EqualTo(InitialTitle), "initial value of the title hasn't change");
            Assert.That(initialCountCardsPresent, Is.EqualTo(1), "the cards present aren't the expected");
            Assert.That(monsterCreated, Is.True, "Card of monster is not visible");

            await monstersPage.ClickDeleteBtn(monster.Name);

            initialCountCardsPresent = await monstersPage.GetMonsterCardCount();
            dynamicTitle = await monstersPage.GetDynamicTitle();
            monsterCreated = await monstersPage.IsMonsterCardVisible(monster.Name);

            Assert.That(dynamicTitle.Trim(), Is.EqualTo(InitialTitle), "initial value of the title has change");
            Assert.That(initialCountCardsPresent, Is.EqualTo(0), "the cards present aren'count > 1");
            Assert.That(monsterCreated, Is.False, "Card of monster is visible");

        }

        [Test, Description("happy path, monster created successfuly")]
        public async Task Should_Toogle_Favorite_State()
        {
            var monster = new MonsterModel() { Name = "FavsMonster", Hp = 10, Attack = 62, Defense = 85, Speed = 25 };
            await monstersPage.CreateMonster(2, monster);
            await monstersPage.ScrollToMonstersCard(monster.Name);

            var dynamicTitle = await monstersPage.GetDynamicTitle();
            var initialCountCardsPresent = await monstersPage.GetMonsterCardCount();
            var monsterCreated = await monstersPage.IsMonsterCardVisible(monster.Name);

            Assert.That(dynamicTitle.Trim(), Is.Not.EqualTo(InitialTitle), "initial value of the title hasn't change");
            Assert.That(initialCountCardsPresent, Is.EqualTo(1), "the cards present aren't the expected");
            Assert.That(monsterCreated, Is.True, "Card of monster is not visible");

            var valueBeforeToogle = await monstersPage.GetFavoriteStateCardMonster(monster.Name);

            await monstersPage.ClickFavoriteBtn(monster.Name);
            var valueAfterToogleON = await monstersPage.GetFavoriteStateCardMonster(monster.Name);

            await monstersPage.ClickFavoriteBtn(monster.Name);
            var valueAfterToogleOFF = await monstersPage.GetFavoriteStateCardMonster(monster.Name);

            Assert.That(valueBeforeToogle, Is.Not.EqualTo(valueAfterToogleON));
            Assert.That(valueAfterToogleON, Is.Not.EqualTo(valueAfterToogleOFF));

        }


        [Test, Description("happy path, monster created successfuly")]
        public async Task Should_Create_Multiple_Monsters_Successfuly()
        {
            var monsters = new List<MonsterModel> {
                new MonsterModel() { Name = "SuccesMonster1", Hp = 10, Attack = 62, Defense = 85, Speed = 25 },
                new MonsterModel() { Name = "SuccesMonster2", Hp = 10, Attack = 62, Defense = 85, Speed = 25 } ,
                new MonsterModel() { Name = "SuccesMonster3", Hp = 10, Attack = 62, Defense = 85, Speed = 25 }
            };

            for (int i = 0; i < monsters.Count; i++) {

                await monstersPage.CreateMonster(index: i + 1, monsters[i]);
            }
            var monsterCount = monsters[monsters.Count() - 1];
            await monstersPage.ScrollToMonstersCard(monsterCount.Name);

            var dynamicTitle = await monstersPage.GetDynamicTitle();
            var initialCountCardsPresent = await monstersPage.GetMonsterCardCount();
        

            Assert.That(dynamicTitle.Trim(), Is.Not.EqualTo(InitialTitle), "initial value of the title hasn't change");
            Assert.That(initialCountCardsPresent, Is.EqualTo(monsters.Count()), "the cards present aren't the expected");

            foreach( var m in monsters)
            {
                var cardMonster= monstersPage.GetMonsterCardByName(m.Name);

                Assert.That(await monstersPage.IsMonsterCardVisible(m.Name), Is.True);

                Assert.That(await monstersPage.GetMonsterCardHp(m.Name), Is.EqualTo(m.Hp.ToString()), "HP value is not the same createed");
                Assert.That(await monstersPage.GetMonsterCardAttack(m.Name), Is.EqualTo(m.Attack.ToString()), "HP value is not the same createed");
                Assert.That(await monstersPage.GetMonsterCardDefense(m.Name), Is.EqualTo(m.Defense.ToString()), "HP value is not the same createed");
                Assert.That(await monstersPage.GetMonsterCardSpeed(m.Name), Is.EqualTo(m.Speed.ToString()), "HP value is not the same createed");

            }
          

        }

    }
}
