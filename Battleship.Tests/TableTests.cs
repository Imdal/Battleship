using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations.Schema;
using Battleship;

namespace Battleship.Tests
{
    [TestClass]
    public class TableTests
    {
        [TestMethod()]
        public void placeShipTest()
        {
            Table table = new Table();
            Assert.IsTrue(table.placeShip(0, 0, 0, 0));
            Assert.IsTrue(table.placeShip(2, 2, 2, 4));
            Assert.IsFalse(table.placeShip(0, 0, 5, 0));
            Assert.IsFalse(table.placeShip(5, 1, 6, 2));
        }

        [TestMethod()]
        public void allShipsPlacedTest()
        {
            Table table = new Table();
            Assert.IsFalse(table.allShipsPlaced());
            table.placeShip(0, 0, 0, 0);
            table.placeShip(0, 2, 1, 2);
            Assert.IsFalse(table.allShipsPlaced());
            table.placeShip(0, 4, 2, 4);
            table.placeShip(0, 6, 3, 6);
            table.placeShip(0, 8, 4, 8);
            Assert.IsTrue(table.allShipsPlaced());
        }

        [TestMethod()]
        public void shootTest()
        {
            Table table = new Table();
            table.placeShip(0, 1, 0, 5);
            Assert.IsTrue(table.shoot(0, 1));
            Assert.IsFalse(table.shoot(0, 1));
            Assert.IsTrue(table.shoot(5, 5));
            Assert.IsFalse(table.shoot(5, 5));
        }

        [TestMethod()]
        public void numberOfShipsShunkenTest()
        {
            Table table = new Table();
            Assert.AreEqual(0, table.numberOfShipsShunken());
            table.placeShip(0, 0, 0, 0);
            table.placeShip(0, 2, 1, 2);
            table.placeShip(0, 4, 2, 4);
            table.placeShip(0, 6, 3, 6);
            table.placeShip(0, 8, 4, 8);
            table.shoot(0, 0);
            Assert.AreEqual(1, table.numberOfShipsShunken());
            table.shoot(0, 2);
            table.shoot(1, 2);
            Assert.AreEqual(2, table.numberOfShipsShunken());
        }

        [TestMethod()]
        public void allShootedTest()
        {
            Table table = new Table();
            table.placeShip(0, 0, 0, 0);
            table.shoot(0, 0);
            table.placeShip(0, 2, 1, 2);
            table.shoot(0, 2);
            table.shoot(1, 2);
            Assert.IsFalse(table.allShooted());
            table.placeShip(0, 4, 2, 4);
            table.shoot(0, 4);
            table.shoot(1, 4);
            table.shoot(2, 4);
            table.placeShip(0, 6, 3, 6);
            table.shoot(0, 6);
            table.shoot(1, 6);
            table.shoot(2, 6);
            table.shoot(3, 6);
            table.placeShip(0, 8, 4, 8);
            table.shoot(0, 8);
            table.shoot(1, 8);
            table.shoot(2, 8);
            table.shoot(3, 8);
            table.shoot(4, 8);
            Assert.IsTrue(table.allShooted());
        
        }
    }
}
