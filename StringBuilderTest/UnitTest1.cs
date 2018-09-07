using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace StringBuilderTest
{

    //StringBuilder sb1 = new StringBuilder("");
    //for (int i = 0; i < Int32.MaxValue; i++) 
    //{
    //    sb1.Append(s);
    //}


    //нужно протестировать stringbuilder на любые входные данные, из любых символов

    // act


    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Создадим экземпляр объекта StringBuilder из строки, преобразуем его обратно к string и убедимся, что данное преобразование не меняет строку
        /// Таким образом мы убедимся в возможности использования этого констурктора в дальнейших тестах
        /// Также проверим метод на корректность метод Append
        /// </summary>
        /// 
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                // arrange
                string s = "Hello world";
                StringBuilder sb0;
                StringBuilder sb1;

                //action
                sb0 = new StringBuilder(s); //создадим объект stringbuilder из строки s
                sb1 = new StringBuilder(); //создадим пустой объект stringbuilder и добавим к нему строку s
                sb1.Append(s);
                string result0 = sb0.ToString();
                string result1 = sb1.ToString();

                // assert
                Assert.AreEqual(s, result0); //совпадение говорит о том, что преобрвазование string->stringbuilder->string не изменяет входную строку
                Assert.AreEqual(s, result1); //совпадение говорит о том, что добавление объекта string к stringbuilder производится корректно
            }
            catch
            {
                //здесь нужно записать исключение в лог или вывести сообщение, но в юнит-тестах вывод на экран недо
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            string s_hello = "Привет, мир";
            StringBuilder sb = new StringBuilder(s);
        }

        /// <summary>
        /// Данный метод тестирует конструкторы класса StringBuilder, метод преобразования к String, добавление к объекту string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestMethod3()
        {
            // arrange
            string s0 = "";
            string s_hello = "Hello world";
            string s_long = "Hello world, good luck, be happy, my loved world";

            StringBuilder sb0 = new StringBuilder(s0);
            StringBuilder sb1 = new StringBuilder(16);
            StringBuilder sb2 = new StringBuilder(s_hello);
            StringBuilder sb3 = new StringBuilder(16, 32); //Проверить реальный размер созданной строки, строку больше 32 символов
            StringBuilder sb4 = new StringBuilder(s_hello, 16); //Предлагаемый размер больше размера входной строки
            StringBuilder sb5ok = new StringBuilder(s_hello, 0, 3, 16); //корректная строка, подстрока с 0-го символа, 3 символа
            StringBuilder sb5err1 = new StringBuilder(s_hello, -1, 3, 16);//возьмём подстроку меньшего размера, чем входящая, но с отрицательного индекса. Ожидаем исключение.
            StringBuilder sb5err2 = new StringBuilder(s_hello, 0, 16, 16);//подстрока с нулевого символа, но объёмом больше, чем входящая. Ожидаем исключение.

            // act
            string rez0 = sb0.ToString();
            string rez1 = sb1.ToString();
            string rez2 = sb2.ToString();
            sb3.Append(s_long); //добавим к строке, ограниченной 32-мя символами строку, содержащую 48 символов. Ожидаем расширения строки до 64 символов.
            string rez3 = sb3.ToString();
            sb4.Append(s_long); //добавим к строке, ограниченной 16-мя символами строку, содержащую 48 символов. Ожидаем расширения строки до 64 символов. Ситуация аналогична предыдущей, однако проверять надов всё
            string rez4 = sb3.ToString();
            string rez5ok = sb5ok.ToString(); //Проверим корректную работу конструктора



            // assert
            Assert.AreEqual(s0, rez0); //строка должна остаться пустой
            Assert.AreEqual(0, sb0.Capacity); //память под пустую строку не должна выделяться

            Assert.AreEqual(s_hello, rez1); //строка должна остаться пустой
            Assert.AreEqual(16, sb1.Capacity); //в конструкторе задана память под 16 символов

            Assert.AreEqual(s_hello, rez2); //проверка на неизменяемость строки после преобразования
            Assert.AreEqual(16, sb2.Capacity); //Длина строки "Hello world"  - 11 символов. Данный экземпляр StringBuilder должен вмещать 16 символов.

            Assert.AreEqual(s_long, rez3); //Проверка на то, что строку не "урезало"
            Assert.AreEqual(64, sb3.Capacity); //Проверка реального объёма строки

            Assert.AreEqual(s_long, rez4);
            Assert.AreEqual(64, sb3.Capacity); //Здесь также выясним, зависит ли объём, до которого расширяется строка, от предыдущего поведения- расширено от 16 или от 32

            Assert.AreEqual(s_hello, rez5ok);
            Assert.AreEqual(16, sb5ok.Capacity); //строка поместилась, должно быть 16 символов
            

        }


    }
}
