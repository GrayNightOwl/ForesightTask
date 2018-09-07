using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace StringBuilderTest
{


    //нужно протестировать stringbuilder на любые входные данные, из любых символов



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
        public void TestMethod01()
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


        /// <summary>
        /// Данный метод тестирует конструкторы класса StringBuilder, метод преобразования к String, добавление к объекту string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestMethod02()
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


        /// <summary>
        /// Этот и следующие методы тестируют поведение класса на изменение длинны 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestMethod03()
        {
            //arrange
            string s_hello = "Привет, мир";
            StringBuilder sb = new StringBuilder(s_hello);

            //action
            //Из проведённых тестов следует, что сейчас ёмкость объекта stringbuilder составляет 16. Попробуем урезать ёмкость и проверить поведение. Ожидаем исключение
            sb.Capacity = 8;

        }


        /// <summary>
        /// Попытка сделать ёмоксть отрицательной, ожидаем исключение
        /// Попытка сделать то же самое через ссылки не увенчалась успехом
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestMethod04()
        {
            //arrange
            StringBuilder sb = new StringBuilder();

            //action
            //Из проведённых тестов следует, что сейчас ёмкость объекта stringbuilder составляет 16. Попробуем сделать ёмкость отрицательной и проверить поведение. Ожидаем исключение
            //Получить ссылку на свойство невозможно, потому изменить значение по ссылке на отрицательное и посмотреть, что получится, также невозможно

            sb.Capacity = -1;
        }

        /// <summary>
        /// Попытка сделать ёмоксть отрицательной, ожидаем исключение
        /// Попытка сделать то же самое через ссылки не увенчалась успехом
        /// </summary>
        [TestMethod]
        public void TestMethod05()
        {
            //arrange
            StringBuilder sb = new StringBuilder(16);

            //action
            sb.Capacity = 9;

            //assert
            Assert.AreEqual(9, sb.Capacity); //Проверим, изменилась ли ёмкость
        }


        /// <summary>
        /// Попытка записать символ в несуществующую позицию (-1)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestMethod06()
        {
            //arrange
            StringBuilder sb = new StringBuilder();
            sb[-1] = 'a';
        }


        /// <summary>
        /// Попытка записать символ в несуществующую позицию (0)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestMethod07()
        {
            //arrange
            StringBuilder sb = new StringBuilder();
            sb[0] = 'a';
        }



        /// <summary>
        /// Проверка свойства Length
        /// </summary>
        [TestMethod]
        public void TestMethod08()
        {
            //arrange
            string s_hello = "Привет, мир";
            StringBuilder sb = new StringBuilder(s_hello);
            sb.Length = 2;
            Assert.AreEqual(s_hello.Substring(0,sb.Length), sb.ToString()); //Проверим, изменилась ли длина строки, исчезло ли "лишнее" содержимое.
        }


        /// <summary>
        /// Проверка свойства Length на отрицательное значение, ожидаем исключение
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestMethod09()
        {
            //arrange
            string s_hello = "Привет, мир";
            StringBuilder sb = new StringBuilder(s_hello);
            sb.Length = -1;
        }


        /// <summary>
        /// Проверка свойства Length на отрицательное значение, ожидаем исключение
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestMethod10()
        {
            //arrange
            string s_hello = "Привет, мир";
            StringBuilder sb = new StringBuilder(s_hello);
            sb.Length = -1;
        }


        ///// <summary>
        ///// Проверка свойства Length и длины строки на максимальное значение, проверить не представляется возможным, т.к. не хватает памяти на рабочей машине
        ///// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(System.OutOfMemoryException))]
        //public void TestMethod11()
        //{
        //    //arrange
        //    string s_hello = "Привет, мир";
        //    StringBuilder sb = new StringBuilder(s_hello);
        //    for (long i = 0; i < 9147483647; i++)
        //    {
        //        sb.Append("0"); //добавляем к StringBuilder'y символ "0" больше раз, чем максимальное значение int
        //    }
        //}



        /// <summary>
        /// Проверка свойства Length на получение значения
        /// </summary>
        [TestMethod]
        public void TestMethod12()
        {
            //arrange
            string s_hello = "Привет, мир";
            StringBuilder sb = new StringBuilder(s_hello);

            //assert

            Assert.AreEqual(11, sb.Length);
        }



        /// <summary>
        /// Проверяет максимальное значение объёма в StringBuilder
        /// </summary>
        [TestMethod]
        public void TestMethod13()
        {
            //arrange
            string s_hello = "Привет, мир";
            StringBuilder sb = new StringBuilder(s_hello);

            //assert
            Assert.AreEqual(Int32.MaxValue, sb.MaxCapacity);
        }



        /// <summary>
        /// Начинаем тестирование методов Append
        /// </summary>
        [TestMethod]
        public void TestMethod14()
        {
            //arrange
            string s_hello = "Привет,";
            byte b = 255;
            string s_world = "мир";
            char C = '_'; 
            char[] c_mas = "BC".ToCharArray();
            decimal dec_number = 1;
            double doub_number = 2.2;
            float f_number = 1.2f;
            int i_number = 1;
            long l_number = 2;
            object obj = ">Object<";
            sbyte sb_number= -128;
            short sh_number = 1;
            uint ui_number = 2;
            ulong ul_number = 3;
            ushort uh_number = 4;
            StringBuilder sb = new StringBuilder(s_hello);

            //act
            //в sb: "Привет,"
            sb.Append(true); //Привет,True
            sb.Append(b); //Привет,True255
            sb.Append(C); //Привет,True255_
            sb.Append(c_mas); //Привет,True255_BC
            sb.Append(dec_number); //Привет,True255_BC1
            sb.Append(C); //Привет,True255_BC1_
            sb.Append(doub_number); //Привет,True255_BC1_2,2
            sb.Append(C); //Привет,True255_BC1_2,2_
            sb.Append(f_number); //Привет,True255_BC1_2,2_1,2
            sb.Append(C);  //Привет,True255_BC1_2,2_1,2_
            sb.Append(i_number); //Привет,True255_BC1_2,2_1,2_1
            sb.Append(l_number); //Привет,True255_BC1_2,2_1,2_12
            sb.Append(obj); //Привет,True255_BC1_2,2_1,2_12>Object<
            sb.Append(sb_number); //Привет,True255_BC1_2,2_1,2_12>Object<-128
            sb.Append(C); //Привет,True255_BC1_2,2_1,2_12>Object<-128_
            sb.Append(sh_number); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1
            sb.Append(s_world); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир
            sb.Append(ui_number); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир2
            sb.Append(ul_number); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир23
            sb.Append(uh_number); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир234
            sb.Append(C, 2); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир234__
            unsafe
            {
                fixed (char* ref_C = &c_mas[1])
                {
                    sb.Append(ref_C, 1); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир234__C
                }
            }
            sb.Append(c_mas, 0, 1); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир234__CB
            sb.Append(s_hello, 0, 2); //Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир234__CBПр

            string result = "Привет,True255_BC1_2,2_1,2_12>Object<-128_1мир234__CBПр";

            //assert
            Assert.AreEqual(result, sb.ToString());

        }
    }
}
