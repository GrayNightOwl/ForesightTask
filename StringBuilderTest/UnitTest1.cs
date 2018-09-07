using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace StringBuilderTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Данный метод тестирует конструкторы класса StringBuilder.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            StringBuilder sb0 = new StringBuilder();
            StringBuilder sb1 = new StringBuilder(16);
            StringBuilder sb2 = new StringBuilder("Hello world");
            StringBuilder sb3 = new StringBuilder(16, 32);
            StringBuilder sb4 = new StringBuilder("Hello world", 16); //Предлагаемый размер больше размера входной строки
            StringBuilder sb4err = new StringBuilder("Hello world", 1); //Отследим поведение программы с размером строки меньше, чем размер входной строки
            StringBuilder sb5ok = new StringBuilder("Hello world", 0, 3, 16); //корректная строка, подстрока с 0-го символа, 3 символа
            StringBuilder sb5err1 = new StringBuilder("Hello world", -1, 3, 16);//возьмём подстроку меньшего размера, чем входящая, но с отрицательного индекса
            StringBuilder sb5err2 = new StringBuilder("Hello world", 0, 16, 16);//подстрока с нулевого символа, но объёмом больше, чем входящая

            // act






            // assert
        }
    }
}
