using Battleship2.MVC;
using BattleShip2.BusinessLogic.Enums;
using BattleShip2.BusinessLogic.Intefaces;
using BattleShip2.BusinessLogic.Models;
using BattleShip2.BusinessLogic.Models.Filters;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Battleship2.Tests
{
    class JsonTests
    {
        [Test]
        public void Test_Json_Serializer_On_Item()
        {
            FiltersWithSorting filtersWithSorting = new FiltersWithSorting()
            {
                Filters = new List<IFilter> { new PlayerFilter("John"), new RemainingShipFilter(5) },
                Sorting = new SortingItem() { SortingType = SortingType.DateSorting, SortingDirection = SortingDirection.Descending }
            };
            FiltersWithSorting deserialize = default;
            try
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                string query = JsonConvert.SerializeObject(filtersWithSorting, settings);
                deserialize = JsonConvert.DeserializeObject<FiltersWithSorting>(query, settings);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
            Assert.IsTrue(deserialize?.Sorting.SortingType == SortingType.DateSorting && deserialize.Filters[0].GetType() == typeof(PlayerFilter));
        }
    }
}
